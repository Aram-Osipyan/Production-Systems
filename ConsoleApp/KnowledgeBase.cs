using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    class KnowledgeBase
    {
        public Dictionary<string,Fact> Facts
        {
            get;
            private set;
        }
        public List<Rule> Rules
        {
            get;
            private set;
        }
        public Dictionary<Fact, Rule> ReverseRules
        {
            get;
            private set;
        }


        public KnowledgeBase()
        {
            Facts = new Dictionary<string, Fact>();
            Rules = new List<Rule>();
        }
        public void FactsFileParse(string filename)
        {
            var file = File.ReadAllLines(filename);
            foreach (var item in file)
            {
                string[] splitted = item.Split(';');
                if (splitted.Length != 2)
                {
                    throw new Exception("Facts file parse error");
                }
                Facts.Add(splitted[0],new Fact(splitted[0], splitted[1]));
            }
        }
        public void RulesFileParse(string filename)
        {
            var file = File.ReadAllLines(filename);
            foreach (var item in file)
            {
                string[] splitted = item.Split(';');
                if (splitted.Length != 4)
                {
                    throw new Exception("Rules file parse error");
                }
                
                if (Facts.ContainsKey(splitted[2]))
                {
                    throw new Exception($"Fact {splitted[2]} is not found");
                }
                List<Fact> lfacts = splitted[1].Split(',').Select(x=>Facts[x]).ToList();
                Fact rfact = Facts[splitted[2]];
                Rule rule = new Rule(splitted[0], lfacts, rfact, splitted[3]);
                Rules.Add(rule);
                ReverseRules.Add(rfact, rule);
            }
        }
    }
}
