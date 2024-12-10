using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm2cs
{
    public class HornClause
    {
        private LinkedList<string> _literals;
        private string _entailedLiteral;
        public HornClause(string sentence)
        {
            _literals = new LinkedList<string>();
            string[] SplitByEntail = sentence.Split("=>");
            string[] SplitByAnd = SplitByEntail[0].Split("&");
            for (int i = 0; i < SplitByAnd.Length; i++)
            {
                _literals.AddLast(SplitByAnd[i]);
            }
            _entailedLiteral = SplitByEntail[1];
        }
        public LinkedList<string> GetLiterals()
        {
            return _literals;
        }
        public string GetLiteral(int index)
        {
            try
            {
                if (index >= 0 && index < _literals.Count())
                {
                    //https://stackoverflow.com/questions/10164355/how-do-i-get-the-n-th-element-in-a-linkedlistt
                    return _literals.ElementAt(index);
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(3);
                return null;
            }
        }
        public int LCount()
        {
            return _literals.Count();
        }
        public void RemoveL(string literal)
        {
            _literals.Remove(literal);
        }
        public string GetEntailedL()
        {
            return _entailedLiteral;
        }
        public string Print()
        {
            string OutputS = "";
            for (int i = 0; i < _literals.Count();i++)
            {
                if (i != 0)
                {
                    OutputS += "^";
                }
                OutputS += _literals.ElementAt(i);
            }
            OutputS += "=>";
            OutputS += _entailedLiteral;
            OutputS = OutputS.Trim();
            OutputS.Replace(" ", string.Empty);
            return OutputS;
        }
    }
}
