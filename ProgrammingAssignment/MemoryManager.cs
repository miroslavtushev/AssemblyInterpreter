using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    static class MemoryManager
    {
        public static int Load(int addr)
        {
            var loc = addr % Globals.CACHE_SIZE;
            var t = Globals.Cache[loc];
            if (!t.Item1.HasValue) // read - miss
            {
                Globals.Misses++;
                Globals.Cache[loc].Item1 = false;
                Globals.Cache[loc].Item2 = addr;
                Globals.Cache[loc].Item3 = Globals.Memory[addr];
                Globals.Cycles += 10;
            }
            else if (t.Item1.HasValue && t.Item2 != addr)
            {
                if (t.Item1.Value) // dirty block - write back
                {
                    Globals.Misses++;
                    Globals.Memory[t.Item2] = t.Item3;
                    Globals.Cache[loc].Item1 = false;
                    Globals.Cache[loc].Item2 = addr;
                    Globals.Cache[loc].Item3 = Globals.Memory[addr];
                    Globals.Cycles += 10;
                }
                else
                {
                    Globals.Misses++;
                    Globals.Cache[loc].Item1 = false;
                    Globals.Cache[loc].Item2 = addr;
                    Globals.Cache[loc].Item3 = Globals.Memory[addr];
                    Globals.Cycles += 10;
                }
            }
            return Globals.Cache[loc].Item3;
        }

        public static void Store(int i, int addr)
        {
            var loc = addr % Globals.CACHE_SIZE;
            var t = Globals.Cache[loc];
            if (t.Item1.HasValue && t.Item2 == addr) // the block is in cache
            {
                Globals.Cache[loc].Item1 = true;
                Globals.Cache[loc].Item3 = i;
            }
            else if (t.Item1.HasValue && t.Item2 != addr && t.Item1.Value) // different block with dirty bit - write back
            {
                Globals.Memory[t.Item2] = t.Item3;

                Globals.Cache[loc].Item1 = true;
                Globals.Cache[loc].Item2 = addr;
                Globals.Cache[loc].Item3 = i;
            }
            else // block is not in the cache and no write back is needed
            {
                Globals.Cache[loc].Item1 = true;
                Globals.Cache[loc].Item2 = addr;
                Globals.Cache[loc].Item3 = i;
            }
        }
    }
}
