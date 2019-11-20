using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment
{
    class CommentRemoval
    {
        private string text;

        public CommentRemoval(string input)
        {
            text = input;
        }
        public string RemoveComments()
        {
            bool inComment = false;
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                if (inComment && text[i] == '\n')
                {
                    s.Append(text[i]);
                    inComment = false;
                }
                else if (inComment)
                    continue;
                else if (text[i] == '/' && text[i + 1] == '/' && !inComment)
                {
                    i++;
                    inComment = true;
                    continue;
                }
                else
                    s.Append(text[i]);
            }
            return s.ToString();
        }
    }
}
