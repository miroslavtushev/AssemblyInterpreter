using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    class Register
    {
        public int Value { get; set; }

        public Register (string s)
        {
            try
            {
                Value = Int32.Parse(s);
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error when parsing register value: {s}");
                Environment.Exit(1);
            }
        }
    }
}
