namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class CheckMatch
    {
        private readonly List<CheckMatchValue> values;

        public CheckMatch(List<CheckMatchValue> values)
        {
            this.values = values;
        }

        public void Matche(string regex)
        {
            this.Test(
                regex,
                v => !Regex.Match(v.Value, regex).Success,
                "The folowing class doesn't respect pattern '{0}' :\n{1}");
        }

        public void NotMatche(string regex)
        {
            this.Test(
                regex,
                v => Regex.Match(v.Value, regex).Success,
                "The folowing class match pattern '{0}' :\n{1}");
        }

        private void Test(string regex, Func<CheckMatchValue, bool> predicate, string message)
        {
            var noMatchedValues = this.values.Where(predicate).ToList();
            if (noMatchedValues.Count > 0)
            {
                var classNames = string.Join("\n", noMatchedValues.Select(t => t.Type.Name));
                throw new MatchException(string.Format(message, regex, classNames));
            }
        }
    }
}