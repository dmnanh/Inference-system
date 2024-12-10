using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm2cs
{
    public class BackwardChaining : Algorithm
    {
        private List<HornClause> _clauses;
        private List<string> _facts;
        private string _query;

        private List<string> _factList;
        private List<string> _queries;

        public BackwardChaining()
        {
            SetCode("BC");
        }

        public BackwardChaining(KnowledgeBase kb, string query) : base(kb, query)
        {
            _clauses = kb.GetClauses();
            _facts = kb.GetFacts();
            _query = query;

            _queries = new List<string>();
            _factList = new List<string>();

            SetCode("BC");
        }

        public override string CheckQuery()
        {
            string outputS = "";

            if (CheckFact())
            {
                outputS = "YES: ";

               for (int i = _factList.Count - 1; i >= 0; i--)
                {
                    outputS += (_factList[i]);

                    if (i != 0)
                    {
                        outputS += ", ";
                    }
                }
            }
            else
            {
                outputS = "NO: " + _query + " is not entailed.";
            }
            return outputS;
        }

        public override bool CheckFact()
        {
            _queries.Add(_query);

            while (_queries.Count > 0)
            {
                string curQue = _queries[0];
                _queries.RemoveAt(0);

                _factList.Add(curQue);

                if (!FactCheck(curQue))
                {
                    if (!ClauseCheck(curQue))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public bool FactCheck(string query)
        {
            for (int i = 0; i < _facts.Count; i++)
            {
                if (query == _facts[i])
                {
                    return true;
                }
            }
            return false;
        }

        public bool ClauseCheck(string query)
        {
            bool output = false;

            for (int i = 0; i < _clauses.Count; i++)
            {
                if (query == _clauses[i].GetEntailedL())
                {
                    output = true;

                    for (int j = 0; j < _clauses[i].LCount(); j++)
                    {
                        _queries.Add(_clauses[i].GetLiteral(j));
                    }
                }
            }

            for (int i = 0; i < _factList.Count; i++)
            {
                for (int j = 0; j < _queries.Count; j++)
                {
                    if (_factList[i] == _queries[j])
                    {
                        _queries.RemoveAt(j); 
                    }
                }
            }

            HashSet<string> hs = new HashSet<string>();
            hs.UnionWith(_queries);
            _queries.Clear();
            _queries.AddRange(hs);

            return output;
        }
    }
}
