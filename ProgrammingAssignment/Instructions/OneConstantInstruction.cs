using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class OneConstantInstruction : AbstractInstruction
    {
        protected int Op { get; set; }

        public OneConstantInstruction(string[] tokens)
        {
            try
            {
                Op = Globals.LabelAddresses[tokens[1]];
            }
            catch
            {
                Console.WriteLine($"Error when parsing label: {tokens[1]}");
                Environment.Exit(1);
            }
        }

        public AbstractInstruction GetInstruction(string[] tokens)
        {
            if (tokens[0] == "JMP")
                return new Jmp(tokens);
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

        public virtual void ApplyOperation()
        {
            throw new NotImplementedException();
        }
    }  
}
