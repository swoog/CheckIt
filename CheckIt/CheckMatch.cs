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

        private readonly bool invert;

        internal CheckMatch(List<CheckMatchValue> values, string type, bool invert = false)
        {
            this.values = values;
            this.type = type;
            this.invert = invert;
        }

        public void Match(string regex)
        {
            this.Test(
                regex,
                v => this.invert ^ !Regex.Match(v.Value, regex).Success,
                "The folowing {0} {3} pattern '{1}' :\n{2}");
        }

        public IMatch Not()
        {
            return new CheckMatch(this.values, this.type, true);
        }

        public void EqualTo(string value)
        {
            this.Test(
                value,
                v => this.invert ^ !v.Name.Equals(value),
                "The folowing {0} {3} '{1}' :\n{2}");
        }

        private void Test(string regex, Func<CheckMatchValue, bool> predicate, string message)
        {
            var noMatchedValues = this.values.Where(predicate).ToList();
            if (noMatchedValues.Count > 0)
            {
                var classNames = string.Join("\n", noMatchedValues.Select(t => t.DisplayName).OrderBy(n => n));
                throw new MatchException(string.Format(message, this.type, regex, classNames, this.invert ? "match" : "doesn't respect"));
            }
        }
    }
}