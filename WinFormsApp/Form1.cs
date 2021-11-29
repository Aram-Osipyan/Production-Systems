using ConsoleApp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp
{
    public partial class Form1 : Form
    {
       
        ////добавить факты в рабочую базу

        ///
        KnowledgeBase knowledgeBase = new KnowledgeBase();
        
        public Form1()
        {
            InitializeComponent();
            knowledgeBase.FactsFileParse("facts.txt");
            //Console.WriteLine("facts are parsed");
            knowledgeBase.RulesFileParse("rules.txt");
            //Console.WriteLine("rules are parsed");
            foreach (var item in knowledgeBase.Facts)
            {
                if (!item.Value.IsTarget)
                {
                    checkedListBox1.Items.Add(item.Value);
                }
                else
                {
                    checkedListBox2.Items.Add(item.Value);
                }
                
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            WayListBox.Items.Clear();
            ResultListBox.Items.Clear();
            IEnumerable<Fact> facts = checkedListBox1.CheckedItems.Cast<Fact>();
            HashSet<Fact> workBase = new HashSet<Fact>(facts);
            HashSet<ConsoleApp.Rule> activatedRules = new HashSet<ConsoleApp.Rule>();
            int i = 0;
            for (; i < knowledgeBase.Rules.Count; i++)
            {                
                if (activatedRules.Contains(knowledgeBase.Rules[i]))
                {
                    continue;
                }
                if (knowledgeBase.Rules[i].IsActive(workBase))
                {
                    workBase.Add(knowledgeBase.Rules[i].RightHS);
                    activatedRules.Add(knowledgeBase.Rules[i]);
                    //Console.WriteLine(knowledgeBase.Rules[i].Text);
                    WayListBox.Items.Add(knowledgeBase.Rules[i]);
                    i = 0;
                }


            }
            List<Fact> targets = workBase.Where(x => x.IsTarget).ToList();
            foreach (var item in targets)
            {
                ResultListBox.Items.Add(item);
            }
            ResultListBox.Items.Add("done");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            IEnumerable<ConsoleApp.Rule> facts1 = WayListBox.Items.Cast<ConsoleApp.Rule>();
            WayListBox.Items.Clear();
            ResultListBox.Items.Clear();
            IEnumerable<Fact> facts = checkedListBox1.CheckedItems.Cast<Fact>();
            HashSet<Fact> workBase = new HashSet<Fact>(facts);

            IEnumerable<Fact> targets = checkedListBox2.CheckedItems.Cast<Fact>();
            foreach (var item in targets)
            {
                List<ConsoleApp.Rule> output = new List<ConsoleApp.Rule>();   
                if (IsFound(item, workBase,output))
                {
                    ResultListBox.Items.Add(item);
                    foreach (var rule in output)
                    {
                        WayListBox.Items.Add(rule);
                    }
                }
                
            }
        }

        private bool IsFound(Fact fact, HashSet<Fact> workBase,List<ConsoleApp.Rule> output)
        {
            if (workBase.Contains(fact))
            {
                return true;
            }
            if (IsLeaf(fact))
            {
                return workBase.Contains(fact);
            }
            foreach (ConsoleApp.Rule orfacts in knowledgeBase.ReverseRules[fact])
            {
                bool flag = true;
                foreach (Fact andfacts in orfacts.LeftHS)
                {
                    
                    if (!IsFound(andfacts,workBase,output))
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    output.Add(orfacts);
                    return true;
                }
                
            }
            return false;
        }

        private bool IsLeaf(Fact currentFact)
        {
            if (!knowledgeBase.ReverseRules.ContainsKey(currentFact))
            {
                return true;
            }
            return knowledgeBase.ReverseRules[currentFact].Count == 0;
        }
    }
}
