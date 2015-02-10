namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;

    public class CheckFiles : CheckEnumerableBase<CheckFile>, IPatternContains<CheckFiles, ICheckFilesContains>
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

        public CheckFiles(CompilationInfo compilationInfo, string pattern)
        {
            this.pattern = pattern;
            this.checkFiles = this.Gets(compilationInfo);
        }

        private IEnumerable<CheckFile> Gets(CompilationInfo compilationInfo)
        {
            foreach (var document in compilationInfo.Project.Documents)
            {
                if (FileUtil.FilenameMatchesPattern(document.Name, this.pattern))
                {
                    yield return new CheckFile(document, compilationInfo.Compile);
                }
            }
        }

        protected override IEnumerable<CheckFile> Gets()
        {
            if (this.checkFiles != null)
            {
                return this.checkFiles;
            }
            
            return this.GetFilesFromProject("*.csproj");
        }

        public ICheckContains<ICheckFilesContains> Contains()
        {
            return new CheckContains<CheckFileContains>(new CheckFileContains(this));
        }

        public CheckFiles Have()
        {
            throw new System.NotImplementedException();
        }

        public CheckClasses Class(string match)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(match)));
        }

        public IPatternContains<CheckFiles, ICheckFilesContains> FromProject(string pattern)
        {
            return this.GetFilesFromProject(pattern);
        }

        private CheckFiles GetFilesFromProject(string pattern)
        {
            return Check.GetProjects(pattern).File(this.pattern);
        }
    }
}