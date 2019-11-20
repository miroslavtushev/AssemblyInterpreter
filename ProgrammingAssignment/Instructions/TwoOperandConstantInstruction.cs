using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class TwoOperandConstantInstruction : AbstractInstruction
    {
        protected int R2 { get; set; }
        protected int Op { get; set; }

        public TwoOperandConstantInstruction(string[] tokens)
        {
            // check if valid registers have been provided
            if (!tokens[1].StartsWith("R") || !tokens[2].StartsWith("R"))
                throw new Exception($"Error when decoding registers: {tokens[1]}, {tokens[2]}");
            try
            {
                R1 = Int32.Parse(tokens[1].Substring(1));
                R2 = Int32.Parse(tokens[2].Substring(1));
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error when decoding registers: {tokens[1]}, {tokens[2]}");
                Environment.Exit(1);
            }
            try
            {
                Op = Int32.Parse(tokens[3]);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error when parsing constant value: {tokens[3]}");
                Environment.Exit(1);
            }
        }

        public AbstractInstruction GetInstruction(string[] tokens)
        {
            if (tokens[0] == "ADDI")
                return new Addi(tokens);
            else if (tokens[0] == "SUBI")
                return new Subi(tokens);
            else if (tokens[0] == "MULI")
                return new Muli(tokens);
            else if (tokens[0] == "DIVI")
                return new Divi(tokens);
            else if (tokens[0] == "ANDI")
                return new Andi(tokens);
            else if (tokens[0] == "ORI")
                return new Ori(tokens);
            else if (tokens[0] == "LDI")
                return new Ldi(tokens);
            else if (tokens[0] == "STI")
                return new Sti(tokens);
            else
                throw new Exception($"Error when decoding instruction: {tokens[0]}");
        }

        public override void Compute()
        {
            ApplyOperation();
        }

        public override int Peek()
        {
            throw new NotImplementedException();
        }

        public override void GetOperands(ref int?[] arr)
        {
            arr[0] = R2;
        }

        public virtual void ApplyOperation()
        {
            throw new NotImplementedException();
        }
    }  
}
