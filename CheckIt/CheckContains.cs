namespace CheckIt
{
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
            return this.checkFiles;
        }

        public T3 One()
        {
            this.checkFiles.Predicate = e => e.Count == 1;
            return this.checkFiles;
        }
    }
}