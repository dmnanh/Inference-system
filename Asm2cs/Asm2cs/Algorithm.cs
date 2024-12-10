using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asm2cs
{
    public abstract class Algorithm
    {
        private string _code;
        private KnowledgeBase _knowledgeBase;
        private string _query;
        public Algorithm()
        {

        }
        public Algorithm(KnowledgeBase knowledgeBase, string query)
        {
            _knowledgeBase = knowledgeBase;
            _query = query;
        }
        public string GetCode() { return _code; }
        public void SetCode(string code) { _code = code; }
        protected KnowledgeBase GetKB() { return _knowledgeBase; }
        public void setKB(KnowledgeBase kb) { _knowledgeBase=kb; }
        protected string GetQuery() { return _query; }
        public void SetQuery(string query) { _query = query; }
        public abstract string CheckQuery();
        public abstract bool CheckFact();
    }
}
