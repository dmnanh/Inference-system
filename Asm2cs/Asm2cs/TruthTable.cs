using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm2cs
{
    public class TruthTable : Algorithm
    {
        private List<HornClause> _clauses;
        private List<string> _facts;
        private string _query;

        private List<string> _var;
        private int _cols;
        private int _rows;
        private bool[ , ] _grid;
        private bool[ ] _result;
        private int[ , ] _literalI;
        private int[ ] _factI;
        private int[ ] _entailed;
        private bool[ ] _output;
        private int _queryI;
        private int _num;

        public TruthTable()
        {
            SetCode("TT");
        }

        public TruthTable(KnowledgeBase kb, string query) : base(kb, query)
        {
            _clauses = kb.GetClauses();
            _facts = kb.GetFacts();
            _query = query;

            _var = new List<string>();

            GetVar();

            _cols = _var.Count;

            _rows = (int)Math.Pow(2, _var.Count);

            _grid = new bool[_rows, _cols];

            _result = new bool[_rows];

            for(int i = 0; i < _rows; i++)
            {
                _result[i] = true;
            }

            _literalI = new int[_clauses.Count, 2];

            _factI = new int[_facts.Count];

            _entailed = new int[_clauses.Count];

            _output = new bool[_rows];

            _queryI = 0;

            _num = 0;

            CreateGrid();

            FactsColIndex();

            LColIndex();

            SetCode("TT");
        }
        public override string CheckQuery()
        {
            string outputS = "";

            if (CheckFact())
            {
                outputS = "YES: " + _num;
            }
            else
            {
                outputS = "NO: " + _query + " is not entailed.";
            }
            return outputS;
        }
        public override bool CheckFact()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _factI.Length; j++)
                {
                    if (_result[i])
                    {
                        if (!_grid[i, _queryI])
                        {
                            _result[i] = false;
                            _output[i] = false;
                            break;
                        }
                        else
                        {
                            _output[i] = true;
                        }
                        _result[i] = _grid[i, _factI[j]];
                    }
                    else
                    {
                        break;
                    }
                }
            }

            for (int i = 0; i < _rows; i++)
            {
                if (_result[i])
                {
                    for (int j = 0; j < _literalI.GetLength(0); j++)
                    {
                        if (_clauses[j].LCount() == 2)
                        {
                            if ((_grid[i, _literalI[j, 0]] == true) &&
                                (_grid[i, _literalI[j, 1]] == true) &&
                                (_grid[i, _entailed[j]] == false))
                            {
                                _result[i] = false;
                            }
                        }
                        else
                        {
                            if ((_grid[i, _literalI[j, 0]] == true) &&
                                (_grid[i, _entailed[j]] == false))
                            {
                                _result[i] = false;
                            }
                        }
                    }
                }
            }

            for (int i = 0; i < _rows; i++)
            {
                if (_result[i])
                {
                    _num++;
                }

                if (_output[i] == false && _result[i] == true)
                {
                    return false;
                }
            }

            return true;
        }
        public void GetVar()
        {
            for (int i = 0; i < _clauses.Count; i++)
            {
                for (int j = 0; j < _clauses[i].LCount(); j++)
                {
                    _var.Add(_clauses[i].GetLiteral(j));
                }

                _var.Add(_clauses[i].GetEntailedL());
            }

            HashSet<string> hs = new HashSet<string>();
            hs.UnionWith(_var);
            _var.Clear();
            _var.AddRange(hs);
        }
        public void CreateGrid()
        {
            for (int i = 0; i < _rows; i++)
            {
                for (int j = 0; j < _cols; j++)
                {
                    int v = i & 1 << _cols - 1 - j;
                    _grid[i, j] = (v == 0 ? true : false);
                }
            }
        }
        public void FactsColIndex()
        {
            for (int i = 0; i < _facts.Count; i++ )
            {
                for (int j = 0; j < _var.Count; j++)
                {
                    if (_facts[i].Equals(_var[j]))
                    {
                        _factI[i] = j;
                    }

                    if (_query.Equals(_var[j]))
                    {
                        _queryI = j;
                    }
                }
            }
        }
        public void LColIndex()
        {
            for (int i = 0; i < _var.Count; i++)
            {
                for (int j = 0; j < _clauses.Count; j++)
                {
                    for (int x = 0; x < _clauses[j].LCount(); x++)
                    {
                        if (_clauses[j].GetLiteral(x).Equals(_var[i]))
                        {
                            _literalI[j, x] = i;
                        }
                    }

                    if (_clauses[j].GetEntailedL().Equals(_var[i]))
                    {
                        _entailed[j] = i;
                    }
                }
            }
        }
        
        
    }
}
