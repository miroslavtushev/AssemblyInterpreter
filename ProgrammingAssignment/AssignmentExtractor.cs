using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    class AssignmentExtractor
    {
        private string text;

        public AssignmentExtractor(string input)
        {
            text = input;
        }

        public string ExtractAssignment()
        {
            var r1 = new Regex(@"^R(\d+)\s*=\s*(\d+)");
            var r2 = new Regex(@"^R(\d+)\-R(\d+)\s*=\s*(\d+)");
            var m1 = new Regex(@"^M\[(\d+)\]\s*=\s*(\d+)");
            var m2 = new Regex(@"^M\[(\d+)\-(\d+)\]\s*=\s*(\d+)");

            var txt = Regex.Split(text, "\r\n|\r|\n");

            foreach (var line in txt)
            {
                Match ma1, ma2, ma3, ma4;
                ma1 = r1.Match(line);
                ma2 = r2.Match(line);
                ma3 = m1.Match(line);
                ma4 = m2.Match(line);

                if (ma1.Success)
                    Globals.Registers[Int32.Parse(ma1.Groups[1].Value)] = Int32.Parse(ma1.Groups[2].Value);
                else if (ma2.Success)
                {
                    int low, high, val;
                    low = Int32.Parse(ma2.Groups[1].Value);
                    high = Int32.Parse(ma2.Groups[2].Value);
                    val = Int32.Parse(ma2.Groups[3].Value);
                    for (int i = low; i <= high; i++)
                        Globals.Registers[i] = val;
                }
                else if (ma3.Success)
                    Globals.Memory[Int32.Parse(ma3.Groups[1].Value)] = Int32.Parse(ma3.Groups[2].Value);
                else if (ma4.Success)
                {
                    int low, high, val;
                    low = Int32.Parse(ma4.Groups[1].Value);
                    high = Int32.Parse(ma4.Groups[2].Value);
                    val = Int32.Parse(ma4.Groups[3].Value);
                    for (int i = low; i <= high; i++)
                        Globals.Memory[i] = val;
                }
            }
            return text = Regex.Replace(text, @"^(R(\d+)\s*=\s*(\d+))|(R(\d+)\-R(\d+)\s*=\s*(\d+))|(M\[(\d+)\]\s*=\s*(\d+))|(M\[(\d+)\-(\d+)\]\s*=\s*(\d+))",
                "", RegexOptions.Multiline);
        }
    }
}
