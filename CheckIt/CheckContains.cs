namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckContains<T, T2> : ICheckContains
        where T : IEnumerable<T2>
    {
        protected readonly T checkFiles;

        public CheckContains(T checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public void Any()
        {
            if (!this.checkFiles.Any())
            {
                throw new MatchException("No class found.");
            }
        }
    }

    public  class CheckFileContains : CheckContains<CheckFiles, CheckFile>, ICheckFilesContains
    {
        public CheckFileContains(CheckFiles checkFiles)
            : base(checkFiles)
        {
        }

        public void Class(string check)
        {
            if (!this.checkFiles.Class(check).Any())
            {
                throw new MatchException("No class found that match '{0}'.", check);
            }
        }
    }
}