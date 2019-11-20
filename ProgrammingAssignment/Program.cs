using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    class Program
    {
        static int Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage: ProgrammingAssignment /folder/file");
                return 1;
            }     
            // init global vars
            Globals.Cycles = 0;
            Globals.PC = 0;
            Globals.Misses = 0;
           
            Array.Clear(Globals.Registers, 0, Globals.Registers.Length);
            var InstructionList = new List<Instructions.AbstractInstruction>();
            Instructions.AbstractInstruction[] Pipeline = new Instructions.AbstractInstruction[5];
            Array.Clear(Pipeline, 0, Pipeline.Length);
            Array.Clear(Globals.Memory, 0, Globals.Memory.Length);
            Array.Clear(Globals.Cache, 0, Globals.Cache.Length);

            uint TotalInstr = 0;
            uint Cycles = 0;

            // preprocessing
            string text = "";
            try
            {
                text = File.ReadAllText(args[0]);
            }
            catch
            {
                Console.WriteLine("Error reading file");
                return 1;
            }
            var cr = new CommentRemoval(text);
            text = cr.RemoveComments();
            var asgn = new AssignmentExtractor(text);
            text = asgn.ExtractAssignment();
            var le = new LabelExtractor(text);
            text = le.ExtractLabels();
            var pr = new PrintExtractor(text);
            var prints = pr.ExtractPrints();

            // reading commands
            using (StringReader reader = new StringReader(text))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("PRINT"))
                        continue;
                    InstructionList.Add(Instructions.InstructionFactory.GetInstruction(line));
                }
            }

            // execute 
            // TODO: 
            // ld x1 x2
            // brl x1 Label
            Pipeline.Add(InstructionList[Globals.PC++]);
            while (!Pipeline.IsEmpty())
            {
                Globals.Cycles++;
                // resolve branch target on stage 2:
                if (Pipeline[1] != null)
                {
                    var t = Pipeline[1].GetType();
                    if (t == typeof(Instructions.Brg) || t == typeof(Instructions.Brl)
                        || t == typeof(Instructions.Brnz) || t == typeof(Instructions.Brz))
                    {
                        int pc = Globals.PC;
                        // data forward from prev. instr.
                        int? res;
                        try
                        {
                            res = null;
                            // find among previous instr. if any os them used the target register
                            int i = 2;
                            while (i < 5 && Pipeline[i] != null)
                            {
                                if (Pipeline[i].GetTargetRegister() == Pipeline[1].GetTargetRegister())
                                {
                                    res = Pipeline[i].Peek();
                                    break;
                                }
                                i++;
                            }
                            if (res.HasValue) // if a previous instr. used target register of the branch
                            {
                                (Pipeline[1] as Instructions.OneOperandConstantInstruction).ComputeInAdvance(res.Value);
                                if (pc != Globals.PC) // if taken
                                {
                                    Globals.Cycles++;
                                    // replace the next instruction with correct one
                                    Pipeline.Add(InstructionList[Globals.PC++]);
                                }
                            }
                            
                        }
                        catch (NotImplementedException)
                        {
                            // preceeding ld, ldi, st, sti instr.
                            // cannot data forward
                        }
                    } 
                }

                // detect data hazards
                // ld   x1, 0(x2)
                // sub  x4, x1, x5
                if (Pipeline[0] != null && Pipeline[1] != null &&
                   (Pipeline[1].GetType() == typeof(Instructions.Ld) || Pipeline[1].GetType() == typeof(Instructions.Ldi)))
                {
                    int reg = (Pipeline[1] as Instructions.AbstractInstruction).GetTargetRegister();
                    int?[] operands = new int?[2];
                    (Pipeline[0] as Instructions.AbstractInstruction).GetOperands(ref operands);
                    int i = -1;
                    int? op;
                    while ((op = operands[++i]) != null)
                    {
                        if (op.Value == reg) // we have a stall
                        {
                            Globals.Cycles++;
                            break;
                        }
                    }

                }
                if (Pipeline[4] != null)
                {
                    try
                    {
                        Pipeline[4].Compute();
                    }
                    catch (DivideByZeroException)
                    {
                        Console.WriteLine("A division of zero occured. Aborting");
                        break;
                    }
                    TotalInstr++;
                }
                Pipeline.Shift();
                if (Globals.PC <= InstructionList.Count - 1)
                    Pipeline.Add(InstructionList[Globals.PC++]);
            }
            // to prevent overwriting
            Cycles = Globals.Cycles;

            // printing
            // TODO: Do print statements incur any cycle overhead?
            if (prints.Item1.Count != 0)
                Console.WriteLine("REGISTERS:");
            foreach (var elem in prints.Item1)
            {
                Console.WriteLine(Globals.Registers[elem]);
            }
            if (prints.Item2.Count != 0)
                Console.WriteLine("MEMORY:");
            foreach (var elem in prints.Item2)
            {
                Console.WriteLine(MemoryManager.Load(elem));
            }

            Console.WriteLine($"Total # of instructions executed: {TotalInstr}");
            Console.WriteLine($"Total # of cycles: {Cycles}");
            Console.WriteLine("Average # of cycles per instruction: {0:F2}", (float)Cycles / (float)TotalInstr);
            Console.WriteLine($"Total # of misses: {Globals.Misses}");
            Console.WriteLine("Misses per instruction: {0:F2}", (float)Globals.Misses / (float)TotalInstr);

            Console.ReadKey();

            return 0;
        }
    }
}
