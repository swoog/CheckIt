namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class CheckContains
    {
        private readonly CheckFiles checkFiles;

        public CheckContains(CheckFiles checkFiles)
        {
            this.checkFiles = checkFiles;
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