using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Sti : TwoOperandConstantInstruction
    {
        public Sti(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            MemoryManager.Store(Globals.Registers[R1], Globals.Registers[R2] + Op);
        }
    }
}
