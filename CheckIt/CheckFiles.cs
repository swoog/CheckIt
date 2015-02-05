namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;

    public class CheckFiles : CheckEnumerableBase<CheckFile>
    {
        private readonly IEnumerable<CheckFile> checkFiles;

        public CheckFiles(IEnumerable<CheckFile> checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public CheckFiles(Project project, Compilation compile)
        {
            this.checkFiles = this.Gets(project, compile);
        }

        private IEnumerable<CheckFile> Gets(Project project, Compilation compile)
        {
            foreach (var document in project.Documents)
            {
                yield return new CheckFile(document, compile);
            }
        }

        protected override IEnumerable<CheckFile> Gets()
        {
            return this.checkFiles;
        }

        public CheckContains Contains()
        {
            return new CheckContains(this);
        }

        public CheckClasses Class(string match)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(match)));
        }
    }
}