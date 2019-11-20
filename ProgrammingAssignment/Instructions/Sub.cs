using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Sub : ThreeOperandInstruction
    {
        public Sub(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            Globals.Registers[R1] = Globals.Registers[R2] - Globals.Registers[R3];
        }

        public override int Peek()
        {
            return Globals.Registers[R2] - Globals.Registers[R3];
        }
    }
}
