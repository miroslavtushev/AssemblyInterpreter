using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class ThreeOperandInstruction : AbstractInstruction
    {
        protected int R2 { get; set; }
        protected int R3 { get; set; }

        public ThreeOperandInstruction(string[] tokens)
        {
            // check if valid registers have been provided
            if (!tokens[1].StartsWith("R") || !tokens[2].StartsWith("R") || !tokens[3].StartsWith("R"))
                throw new Exception($"Error when decoding registers: {tokens[1]}, {tokens[2]}, {tokens[3]}");
            try
            {
                R1 = Int32.Parse(tokens[1].Substring(1));
                R2 = Int32.Parse(tokens[2].Substring(1));
                R3 = Int32.Parse(tokens[3].Substring(1));
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error when decoding registers: {tokens[1]}, {tokens[2]}, {tokens[3]}");
                Environment.Exit(1);
            }
        }

        public AbstractInstruction GetInstruction(string[] tokens)
        {
            if (tokens[0] == "ADD")
                return new Add(tokens);
            else if (tokens[0] == "SUB")
                return new Sub(tokens);
            else if (tokens[0] == "MUL")
                return new Mul(tokens);
            else if (tokens[0] == "DIV")
                return new Div(tokens);
            else if (tokens[0] == "AND")
                return new And(tokens);
            else if (tokens[0] == "OR")
                return new Or(tokens);
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
            arr[1] = R3;
        }


        public virtual void ApplyOperation()
        {
            throw new NotImplementedException();
        }
    }  
}
