namespace CheckIt
{
    using CheckIt.Syntax;

    using Humanizer;

    public class CheckContains : ICheckContains<CheckSpecificContains>
    {
        private readonly CheckSpecificContains checkSpecificContains;

        public CheckContains(CheckSpecificContains checkSpecificContains)
        {
            this.checkSpecificContains = checkSpecificContains;
        }

        public CheckSpecificContains Any()
        {
            this.checkSpecificContains.Predicate = e => e.Count > 0;
            this.checkSpecificContains.MessageFunc = this.AnyMessageFunc;
            return this.checkSpecificContains;
        }

        public CheckSpecificContains One()
        {
            this.checkSpecificContains.Predicate = e => e.Count == 1;
            this.checkSpecificContains.MessageFunc =
                (name, pattern) => "No {0} found that match pattern '{1}'.".FormatWith(name, pattern);
            return this.checkSpecificContains;
        }

        public CheckSpecificContains No()
        {
            this.checkSpecificContains.Predicate = e => e.Count == 0;
            this.checkSpecificContains.MessageFunc =
                (name, pattern) => "{0} found that match pattern '{1}'.".FormatWith(name.Humanize(), pattern);
            return this.checkSpecificContains;
        }

        private string AnyMessageFunc(string name, string pattern)
        {
            if (string.IsNullOrEmpty(pattern) || pattern == "*")
            {
                return "No {0} found.".FormatWith(name);
            }

            return "No {0} found that match pattern '{1}'.".FormatWith(name, pattern);
        }
    }
}