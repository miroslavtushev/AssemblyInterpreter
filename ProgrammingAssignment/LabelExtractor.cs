using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    class LabelExtractor
    {
        private string text;

        public LabelExtractor(string input)
        {
            text = input;
        }

        public string ExtractLabels()
        {
            // remove empty lines
            text = Regex.Replace(text, @"^\s+$[\r\n]*", string.Empty, RegexOptions.Multiline);
            // find and record all labels
            Regex r = new Regex(@"(\w+):");
            var matches = r.Matches(text);
            foreach (Match m in matches)
            {
                Globals.LabelAddresses.Add(m.Groups[1].Value, LineFromPos(text, m.Index)-1);
            }
            return text = Regex.Replace(text, @"\w+:", "");
        }

        private int LineFromPos(string input, int indexPosition)
        {
            int lineNumber = 1;
            for (int i = 0; i < indexPosition; i++)
            {
                if (input[i] == '\n') lineNumber++;
            }
            return lineNumber;
        }
    }
}
