using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    static class ExtensionMethods
    {
        public static void Add(this Instructions.AbstractInstruction[] ar, Instructions.AbstractInstruction i)
        {
            ar[0] = i;
        }

        public static void Shift(this Instructions.AbstractInstruction[] ar)
        {
            for (int i = 4; i > 0; i--)
                ar[i] = ar[i - 1];
            ar[0] = null;
        }

        public static bool IsFull(this Instructions.AbstractInstruction[] ar)
        {
            return ar[0] == null ? false : true;
        }

        public static bool IsEmpty(this Instructions.AbstractInstruction[] ar)
        {
            return ar[4] == null && ar[3] == null 
                && ar[2] == null && ar[1] == null 
                && ar[0] == null ? true : false;
        }
    }
}
