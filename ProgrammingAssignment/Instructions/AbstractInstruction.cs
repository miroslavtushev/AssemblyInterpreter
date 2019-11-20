using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    abstract class AbstractInstruction
    {
        protected virtual int R1 { get; set; }
        public virtual int GetTargetRegister()
        {
            return R1;
        }
        public abstract void Compute();
        public abstract int Peek();
        public virtual void GetOperands(ref int?[] arr) { }
    }
}
