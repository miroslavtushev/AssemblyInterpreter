using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class TwoOperandInstruction : AbstractInstruction
    {
        protected int R2 { get; set; }

        public TwoOperandInstruction(string[] tokens)
        {
            // check if valid registers or memory have been provided
            if (!tokens[1].StartsWith("R") || !tokens[2].StartsWith("R"))
            { 
               throw new Exception($"Error when decoding registers: {tokens[1]}, {tokens[2]}");
            }
            else
            {
                try
                {
                    R1 = Int32.Parse(tokens[1].Substring(1));
                    R2 = Int32.Parse(tokens[2].Substring(1));
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Error when decoding registers: {tokens[1]}, {tokens[2]}, {tokens[3]}");
                    Environment.Exit(1);
                }
            }
        }

        public AbstractInstruction GetInstruction(string[] tokens)
        {
            if (tokens[0] == "NOT")
                return new Not(tokens);
            else if (tokens[0] == "LD")
                return new Ld(tokens);
            else if (tokens[0] == "ST")
                return new St(tokens);
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
