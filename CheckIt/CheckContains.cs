namespace CheckIt
{
    using CheckIt.Syntax;

    using Humanizer;

    public class CheckContains<T3> : ICheckContains<T3>
        where T3 : class, IContains
    {
        private readonly T3 checkFiles;

        public CheckContains(T3 checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public T3 Any()
        {
            this.checkFiles.Predicate = e => e.Count > 0;
            this.checkFiles.MessageFunc = this.AnyMessageFunc;
            return this.checkFiles;
        }

        public T3 One()
        {
            this.checkFiles.Predicate = e => e.Count == 1;
            this.checkFiles.MessageFunc =
                (name, pattern) => "No {0} found that match pattern '{1}'.".FormatWith(name, pattern);
            return this.checkFiles;
        }

        public T3 No()
        {
            this.checkFiles.Predicate = e => e.Count == 0;
            this.checkFiles.MessageFunc =
                (name, pattern) => "{0} found that match pattern '{1}'.".FormatWith(name.Humanize(), pattern);
            return this.checkFiles;
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