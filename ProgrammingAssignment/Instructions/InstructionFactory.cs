using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingAssignment.Instructions
{
    class InstructionFactory
    {
        public static AbstractInstruction GetInstruction(string input)
        {
            // tokenize input
            string[] tokens = input.Split(new char[0], StringSplitOptions.RemoveEmptyEntries);
            string instrIdent = tokens[0];
            // decode
            if (tokens.Length == 4 && instrIdent[instrIdent.Length - 1] != 'I') // ADD, SUB, DIV...
            {
                var a = new ThreeOperandInstruction(tokens);
                return a.GetInstruction(tokens);
            }
            else if (tokens.Length == 4 && instrIdent[instrIdent.Length - 1] == 'I') // SUBI, ANDI, DIVI...
            {
                var a = new TwoOperandConstantInstruction(tokens);
                return a.GetInstruction(tokens);
            }
            else if (tokens.Length == 3 && (instrIdent[instrIdent.Length - 1] == 'I' || tokens[0].StartsWith("B"))) // NOTI, BRZ, BRL...
            {
                var a = new OneOperandConstantInstruction(tokens);
                return a.GetInstruction(tokens);
            }
            else if (tokens.Length == 3 && instrIdent[instrIdent.Length - 1] != 'I') // NOT, LD, ST
            {
                var a = new TwoOperandInstruction(tokens);
                return a.GetInstruction(tokens);
            }
            else if (tokens.Length == 2) // JMP
            {
                var a = new OneConstantInstruction(tokens);
                return a.GetInstruction(tokens);
            }
            else
            {
                throw new Exception($"Error when decoding instruction: {input}");
            }
        }
    }
}
