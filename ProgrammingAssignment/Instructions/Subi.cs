using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Subi : TwoOperandConstantInstruction
    {
        public Subi(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            Globals.Registers[R1] = Globals.Registers[R2] - Op;
        }

        public override int Peek()
        {
            return Globals.Registers[R2] - Op;
        }
    }
}
