using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ProgrammingAssignment
{
    class PrintExtractor
    {
        private string text;

        public PrintExtractor(string input)
        {
            text = input;
        }

        public (List<int>, List<int>) ExtractPrints()
        {
            List<int> registers = new List<int>();
            List<int> memory = new List<int>();

            var r1 = new Regex(@"^PRINT\s*R(\d+)\s*$");
            var r2 = new Regex(@"^PRINT\s*R(\d+)\-R(\d+)\s*$");
            var m1 = new Regex(@"^PRINT\s*M\[(\d+)\]\s*$");
            var m2 = new Regex(@"^PRINT\s*M\[(\d+)\-(\d+)\]\s*$");

            var txt = Regex.Split(text, "\r\n|\r|\n");

            foreach (var line in txt)
            {
                Match ma1, ma2, ma3, ma4;
                ma1 = r1.Match(line);
                ma2 = r2.Match(line);
                ma3 = m1.Match(line);
                ma4 = m2.Match(line);

                if (ma1.Success)
                    registers.Add(Int32.Parse(ma1.Groups[1].Value));
                else if (ma2.Success)
                {
                    int low, high;
                    low = Int32.Parse(ma2.Groups[1].Value);
                    high = Int32.Parse(ma2.Groups[2].Value);
                    for (int i = low; i <= high; i++)
                        registers.Add(i);
                }
                else if (ma3.Success)
                    memory.Add(Int32.Parse(ma3.Groups[1].Value));
                else if (ma4.Success)
                {
                    int low, high;
                    low = Int32.Parse(ma4.Groups[1].Value);
                    high = Int32.Parse(ma4.Groups[2].Value);
                    for (int i = low; i <= high; i++)
                        memory.Add(i);
                }
            }
            return (registers, memory);
        }
    }

}