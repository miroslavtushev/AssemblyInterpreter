using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class OneOperandConstantInstruction : AbstractInstruction
    {
        protected int Op { get; set; }
        protected bool IsComputed { get; set; }

        public OneOperandConstantInstruction(string[] tokens)
        {
            IsComputed = false;
            // check if valid registers have been provided
            if (!tokens[1].StartsWith("R"))
                throw new Exception($"Error when decoding registers: {tokens[1]}");
            try
            {
                R1 = Int32.Parse(tokens[1].Substring(1));
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error when decoding registers: {tokens[1]}");
                Environment.Exit(1);
            }
            if (tokens[0] == "NOTI")
            {
                try
                {
                    Op = Int32.Parse(tokens[2]);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Error when parsing constant value: {tokens[2]}");
                    Environment.Exit(1);
                }
            }
            else // branch instruction
            {
                try
                {
                    Op = Globals.LabelAddresses[tokens[2]];
                }
                catch
                {
                    Console.WriteLine($"Error when parsing label: {tokens[2]}");
                    Environment.Exit(1);
                }
            }
        }

        public AbstractInstruction GetInstruction(string[] tokens)
        {    
            if (tokens[0] == "NOTI")
                return new Noti(tokens);
            else if (tokens[0] == "BRZ")
                return new Brz(tokens);
            else if (tokens[0] == "BRNZ")
                return new Brnz(tokens);
            else if (tokens[0] == "BRG")
                return new Brg(tokens);
            else if (tokens[0] == "BRL")
                return new Brl(tokens);
            else
                throw new Exception($"Error when decoding instruction: {tokens[0]}");
        }

        public override void Compute()
        {
            if (!IsComputed)
                ApplyOperation();
            else
                IsComputed = false;
        }

        public void ComputeInAdvance(int target)
        {
            ComputeInAdvanceSub(target);
            IsComputed = true;
        }

        public override int Peek()
        {
            throw new NotImplementedException();
        }

        public virtual void ApplyOperation()
        {
            throw new NotImplementedException();
        }

        public virtual void ComputeInAdvanceSub(int target)
        {
            throw new NotImplementedException();
        }
    }  
}
