using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Div : ThreeOperandInstruction
    {
        public Div(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            try
            {
                Globals.Registers[R1] = Globals.Registers[R2] / Globals.Registers[R3];
            }
            catch (DivideByZeroException)
            {
                throw new DivideByZeroException();
            }
        }

        public override int Peek()
        {
            return Globals.Registers[R2] / Globals.Registers[R3];
        }
    }
}

