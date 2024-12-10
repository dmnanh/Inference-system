using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm2cs
{
    public class ForwardChaining : Algorithm
    {
        private List<HornClause> _clauses;
        private List<string> _facts;
        private string _query;

        private List<string> _factList;

        public ForwardChaining()
        {
            SetCode("FC");
        }

        public ForwardChaining(KnowledgeBase kb, string query) : base(kb, query)
        {
            _clauses = kb.GetClauses();
            _facts = kb.GetFacts();
            _query = query;

            _factList = new List<string>();

            SetCode("FC");
        }

        public override string CheckQuery()
        {
            string outputS = "";

            if (CheckFact())
            {
                outputS = "YES: ";
                
                for (int i = 0; i < _factList.Count; i++)
                {
                    outputS += _factList[i];

                    if (i < _factList.Count - 1)
                    {
                        outputS += ", ";
                    }
                }
            }
            else
            {
                outputS = "NO: " + _query + " is not entailed.  ";
            }
            return outputS;
        }

        public override bool CheckFact()
        {
            while (_facts.Count > 0)
            {
                string fact = _facts[0];
                _facts.RemoveAt(0);
                _factList.Add(fact);
                if (fact == _query)
                {
                    return true;
                }
                for (int i = 0; i < _clauses.Count; i++)
                {
                    for (int j = 0; j < _clauses[i].LCount(); j++)
                    {
                        if (fact == (_clauses[i].GetLiteral(j)))
                        {
                            _clauses[i].RemoveL(fact);
                        }
                    }
                }

                for (int i = 0; i < _clauses.Count; i++)
                {
                    if (_clauses[i].LCount() == 0)
                    {
                        _facts.Add(_clauses[i].GetEntailedL());
                        _clauses.RemoveAt(i);
                    }
                }
            }
            return false;
        }
    }
}
