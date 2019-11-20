using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    static class Globals
    {
        public const uint NUM_REGISTERS = 32;
        public static int[] Registers = new int[NUM_REGISTERS];

        public const uint MEM_SIZE = 10000;
        public const uint CACHE_SIZE = 256;
        public static int[] Memory = new int[MEM_SIZE];
        // Item1 = dirty status
        // Item2 = address in memory
        // Item3 = value
        public static (bool?, int, int)[] Cache = new (bool?, int, int)[CACHE_SIZE];

        public static int PC;
        public static uint Cycles;
        public static uint Misses;

        public static Dictionary<string, int> LabelAddresses = new Dictionary<string, int>();

    }
}
