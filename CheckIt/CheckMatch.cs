namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using CheckIt.Syntax;

    public class CheckMatch : IMatch
    {
        private readonly List<CheckMatchValue> values;

        private readonly string type;

        internal CheckMatch(List<CheckMatchValue> values, string type)
        {
            this.values = values;
            this.type = type;
        }

        public void Match(string regex)
        {
            this.Test(
                regex,
                v => !Regex.Match(v.Value, regex).Success,
                "The folowing {0} doesn't respect pattern '{1}' :\n{2}");
        }

        public void NotMatch(string regex)
        {
            this.Test(
                regex,
                v => Regex.Match(v.Value, regex).Success,
                "The folowing {0} match pattern '{1}' :\n{2}");
        }

        private void Test(string regex, Func<CheckMatchValue, bool> predicate, string message)
        {
            var noMatchedValues = this.values.Where(predicate).ToList();
            if (noMatchedValues.Count > 0)
            {
                var classNames = string.Join("\n", noMatchedValues.Select(t => t.DisplayName).OrderBy(n => n));
                throw new MatchException(string.Format(message, this.type, regex, classNames));
            }
        }
    }
}