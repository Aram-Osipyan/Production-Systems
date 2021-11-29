using System.Text.RegularExpressions;
namespace ConsoleApp
{
    class Fact
    {
        public string FactID { get; set; }
        public string Text { get; set; }
        public Fact(string factID,string text)
        {
            FactID = factID;
            Text = text;
        }
        public override int GetHashCode()
        {
            return FactID.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return FactID == (obj as Fact)?.FactID;
        }
        public bool IsTarget
        {
            get
            {
                return Regex.IsMatch(Text, @".*Страна:.*");
            }
        }
        public override string ToString()
        {
            return Text;
        }

    }
}
