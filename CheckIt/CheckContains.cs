namespace CheckIt
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

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

    public interface IContains
    {
        Predicate<IList> Predicate { get; set; }
    }

    public  class CheckFileContains : ICheckFilesContains, IContains
    {
        private readonly CheckFiles checkFiles;

        private List<CheckClass> classes;

        public Predicate<IList> Predicate { get; set; }

        public CheckFileContains(CheckFiles checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public void Class(string check)
        {
            this.classes = this.checkFiles.Class(check).ToList();
            if (!this.Predicate(this.classes))
            {
                throw new MatchException("No class found that match '{0}'.", check);
            }
        }

        public void Class()
        {
            this.Class(string.Empty);
        }
    }
}