using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{
    public static class ExtensionMethod
    {
        public static void Add<T>(this HashSet<T> set,IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                set.Add(item);
            }
        }
        public static void Enqueue<T>(this Queue<T> set, IEnumerable<T> list)
        {
            foreach (var item in list)
            {
                set.Enqueue(item);
            }
        }
    }
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
        public Dictionary<Fact, List<Rule>> ReverseRules
        {
            get;
            private set;
        }


        public KnowledgeBase()
        {
            Facts = new Dictionary<string, Fact>();
            Rules = new List<Rule>();
            ReverseRules = new Dictionary<Fact, List<Rule>>();
        }
        public void FactsFileParse(string filename)
        {
            var file = File.ReadAllLines(filename);
            int i = 1;
            foreach (var item in file)
            {
                string[] splitted = item.Split(';');
                if (splitted.Length != 2)
                {
                    throw new Exception("Facts file parse error");
                }
                Facts.Add(splitted[0],new Fact(splitted[0], splitted[1]));
                i++;
            }
        }
        public void RulesFileParse(string filename)
        {
            var file = File.ReadAllLines(filename);
            int i = 1;
            foreach (var item in file)
            {
                string[] splitted = item.Split(';');
                if (splitted.Length != 4)
                {
                    throw new Exception($"Rules file parse error. line number {i}");
                }
                
                if (!Facts.ContainsKey(splitted[2]))
                {
                    throw new Exception($"Fact {splitted[2]} is not found");
                }
                List<Fact> lfacts = splitted[1].Split(',').Select(x=>Facts[x]).ToList();
                Fact rfact = Facts[splitted[2]];
                Rule rule = new Rule(splitted[0], lfacts, rfact, splitted[3]);
                Rules.Add(rule);
                if (ReverseRules.ContainsKey(rfact))
                {
                    ReverseRules[rfact].Add(rule);
                }
                else
                {
                    ReverseRules[rfact] = new List<Rule>();
                }
                
                i++;
            }
        }
    }
}
