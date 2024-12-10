using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Asm2cs
{
    public class KnowledgeBase
    {
        private List<HornClause> _clauses;
        private List<string> _facts;
        public KnowledgeBase()
        {
            _clauses = new List<HornClause>();
            _facts = new List<string>();
        }
        public KnowledgeBase(StreamReader reader)
        {
            _clauses = new List<HornClause>();
            _facts = new List<String>();
            ReadInput(reader);
        }
        public void ReadInput(StreamReader reader)
        {
            try
            {
                reader.ReadLine();
                string Tell = reader.ReadLine().Replace(" ", "");
                string[] Sentence = Tell.Split(";");
                for (int i = 0; i < Sentence.Length; i++)
                {
                    if (Sentence[i].Contains("=>"))
                    {
                        _clauses.Add(new HornClause(Sentence[i]));
                    }
                    else
                    {
                        Sentence[i] = Sentence[i].Trim();
                        _facts.Add(Sentence[i]);
                    }
                }
                _facts.RemoveAt(_facts.Count - 1);
            } 
            
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Environment.Exit(2);
            }
        }
        public List<HornClause> GetClauses() { return _clauses; }
        public List<string> GetFacts() {  return _facts; }
        public string Print()
        {
            string outputS = "";
            int count = _clauses.Count;
            outputS += "Knowledge Base: ";
            for (int i = 0;i < count; i++)
            {
                outputS += _clauses[i].Print();
                outputS += "; ";
            }
            count = _facts.Count;
            for (int i = 0; i < count; i++)
            {
                outputS += _facts[i];
                outputS += "; ";
            }
            return outputS;
        }
    }
}
