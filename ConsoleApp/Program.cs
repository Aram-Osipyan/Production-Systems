using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp
{

    public static class Algorithms
    {
        public static void ReverseMethod()
        {         
            KnowledgeBase knowledgeBase = new KnowledgeBase();
            //knowledgeBase.FactsFileParse("");
            //knowledgeBase.RulesFileParse("");



        }
        public static void DirectMethod()
        {
            HashSet<Fact> workBase = new HashSet<Fact>();

            // уже активированные правила
            HashSet<Rule> activatedRules = new HashSet<Rule>();
            ////добавить факты в рабочую базу

            ///
            KnowledgeBase knowledgeBase = new KnowledgeBase();
            //knowledgeBase.FactsFileParse("");
            //knowledgeBase.RulesFileParse("");
            int i;
            for (i = 0; i < knowledgeBase.Rules.Count; i++)
            {
                if (activatedRules.Contains(knowledgeBase.Rules[i]))
                {
                    continue;
                }
                if (knowledgeBase.Rules[i].IsActive(workBase))
                {
                    i = 0;
                    workBase.Add(knowledgeBase.Rules[i].RightHS);
                    activatedRules.Add(knowledgeBase.Rules[i]);
                }

            }
            List<Fact> targets = workBase.Where(x => x.IsTarget).ToList();

            Console.WriteLine("target facts :");
            foreach (var item in targets)
            {
                Console.WriteLine(item.FactID + " " + item.Text);
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            //DirectMethod();

        }

        
    }
}
