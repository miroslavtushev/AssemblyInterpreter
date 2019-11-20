using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Brz : OneOperandConstantInstruction
    {
        public Brz(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            if (Globals.Registers[R1] == 0) Globals.PC = Op;
        }

        public override void ComputeInAdvanceSub(int target)
        {
            if (target == 0) Globals.PC = Op;
        }
    }
}
