namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class CheckMatch
    {
        private readonly List<string> values;

        public CheckMatch(List<string> values)
        {
            this.values = values;
        }

        public void Matche(string regex)
        {
            var noMatchedValues = this.values.Where(v => !Regex.Match(v, regex).Success).ToList();
            if (noMatchedValues.Count > 0)
            {
                throw new MatchException(
                    string.Format(
                        "The folowing class doesn't respect pattern '{0}' :\n{1}",
                        regex,
                        string.Join("\n", noMatchedValues)));
            }
        }
    }
}