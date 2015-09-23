namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class Files : CheckEnumerableBase<IFile>, IFiles, IFileMatcher
    {
        private readonly IEnumerable<IFile> checkFiles;

        private readonly string pattern;

        public Files(string pattern)
        {
            this.pattern = pattern;
        }

        public Files(ICompilationInfo compilationInfo)
        {
            this.pattern = "*";
            this.checkFiles = this.Gets(compilationInfo);
        }

        private Files(IEnumerable<IFile> checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public ICheckContains<ICheckFilesContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(new FilesObjectsFinder(this)));
        }

        public IFileMatcher Have()
        {
            return this;
        }

        public IPatternContains<IFileMatcher, ICheckFilesContains> FromProject(string pattern)
        {
            return new Files(this.GetFilesFromProject(pattern).ToList<IFile>());
        }

        protected override IEnumerable<IFile> Gets()
        {
            if (this.checkFiles != null)
            {
                return this.checkFiles;
            }
            
            return new Files(this.GetFilesFromProject("*.csproj").ToList<IFile>());
        }

        private IEnumerable<CheckFile> Gets(ICompilationInfo compilationInfo)
        {
            return from document in compilationInfo.Project.Documents 
                   where FileUtil.FilenameMatchesPattern(document.Name, this.pattern) 
                   select new CheckFile(document, compilationInfo);
        }

        private IObjectsFinder GetFilesFromProject(string pattern)
        {
            return Check.GetProjects(pattern).File(this.pattern, false);
        }

        public CheckMatch Name()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.Name, null)).ToList();

            return new CheckMatch(values, "file");
        }
    }
}