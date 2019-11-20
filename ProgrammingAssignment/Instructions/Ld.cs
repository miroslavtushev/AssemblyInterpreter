using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class Ld : TwoOperandInstruction
    {
        public Ld(string[] tokens) : base(tokens)
        {
        }

        public override void ApplyOperation()
        {
            Globals.Registers[R1] = MemoryManager.Load(Globals.Registers[R2]);
        }
    }
}
