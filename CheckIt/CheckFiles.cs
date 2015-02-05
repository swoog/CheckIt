namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;

    public class CheckFiles : CheckEnumerableBase<CheckFile>, IPatternContains<CheckFiles>
    {
        private readonly IEnumerable<CheckFile> checkFiles;

        private string pattern;

        public CheckFiles(string pattern)
        {
            this.pattern = pattern;
        }

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
            if (this.checkFiles != null)
            {
                return this.checkFiles;
            }
            else
            {
                return this.GetFilesFromProject("*.csproj");
            }
        }

        public CheckContains Contains()
        {
            return new CheckContains(this);
        }

        public CheckFiles Have()
        {
            throw new System.NotImplementedException();
        }

        public CheckClasses Class(string match)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(match)));
        }

        public IPatternContains<CheckFiles> FromProject(string pattern)
        {
            return this.GetFilesFromProject(pattern);
        }

        private CheckFiles GetFilesFromProject(string pattern)
        {
            return new CheckProjects(Check.basePath, pattern).File(this.pattern);
        }
    }
}