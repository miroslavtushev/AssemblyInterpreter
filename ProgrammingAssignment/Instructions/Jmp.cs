using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Jmp : OneConstantInstruction
    {
        public Jmp(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            Globals.PC = Op;
        }

        public override int GetTargetRegister()
        {
            throw new Exception("JMP does not have target register");
        }
    }
}
