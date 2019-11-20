using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Noti : OneOperandConstantInstruction
    {
        public Noti(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            Globals.Registers[R1] = ~Op;
        }

        public override int Peek()
        {
            return ~Op;
        }
    }
}
