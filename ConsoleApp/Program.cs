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
            knowledgeBase.FactsFileParse("facts.txt");
            Console.WriteLine("facts are parsed");
            knowledgeBase.RulesFileParse("rules.txt");
            Console.WriteLine("rules are parsed");
            workBase.Add(knowledgeBase.Facts["f-9"]);
            workBase.Add(knowledgeBase.Facts["f-12"]);
            workBase.Add(knowledgeBase.Facts["f-11"]);
            //f-9,f-12,f-11
            //f-24,f-23;f-27
            workBase.Add(knowledgeBase.Facts["f-24"]);
            workBase.Add(knowledgeBase.Facts["f-23"]);
            workBase.Add(knowledgeBase.Facts["f-27"]);
            int i;
            for (i = 0; i < knowledgeBase.Rules.Count; i++)
            {
                if (activatedRules.Contains(knowledgeBase.Rules[i]))
                {
                    continue;
                }
                if (knowledgeBase.Rules[i].IsActive(workBase))
                {                    
                    workBase.Add(knowledgeBase.Rules[i].RightHS);
                    activatedRules.Add(knowledgeBase.Rules[i]);
                    Console.WriteLine(knowledgeBase.Rules[i].Text);
                    i = 0;
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
            Algorithms.DirectMethod();

        }

        
    }
}
