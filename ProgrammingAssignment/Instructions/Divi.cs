using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Divi : TwoOperandConstantInstruction
    {
        public Divi(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            try
            {
                Globals.Registers[R1] = Globals.Registers[R2] / Op;
            }
            catch (DivideByZeroException)
            {
                throw new DivideByZeroException();
            }
        }

        public override int Peek()
        {
            
            return Globals.Registers[R2] / Op;
        }
    }
}
