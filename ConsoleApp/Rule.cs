using System.Collections.Generic;

namespace ConsoleApp
{
    class Rule
    {
        public string RuleID { get; set; }
        public List<Fact> LeftHS { get; set; }
        public Fact RightHS { get; set; }
        public string Text { get; set; }
        public Rule(string ruleID, List<Fact> leftHS,Fact rightHS,string text = "")
        {
            RuleID = ruleID;
            LeftHS = leftHS;
            RightHS = rightHS;
            Text = text;
        }
        public bool IsActive(HashSet<Fact> workBase)
        {
            foreach (var item in LeftHS)
            {
                if (!workBase.Contains(item))
                {
                    return false;
                }

            }
            return true;
        }
        public override int GetHashCode()
        {
            return RuleID.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return RuleID == (obj as Rule)?.RuleID;
        }
    }
}
