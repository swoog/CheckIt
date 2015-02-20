namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    public class Files : CheckEnumerableBase<CheckFile>, IObjectsFinder, IFiles
    {
        private readonly IEnumerable<CheckFile> checkFiles;

        private string pattern;

        public Files(string pattern)
        {
            this.pattern = pattern;
        }

        public Files(IEnumerable<CheckFile> checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public Files(ICompilationInfo compilationInfo, string pattern)
        {
            this.pattern = pattern;
            this.checkFiles = this.Gets(compilationInfo);
        }

        private IEnumerable<CheckFile> Gets(ICompilationInfo compilationInfo)
        {
            foreach (var document in compilationInfo.Project.Documents)
            {
                if (FileUtil.FilenameMatchesPattern(document.Name, this.pattern))
                {
                    yield return new CheckFile(document, compilationInfo);
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
            return new CheckContains<CheckSpecificContains>(new CheckSpecificContains(this));
        }

        public IFiles Have()
        {
            throw new System.NotImplementedException();
        }

        public CheckClasses Class(string match)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(match)));
        }

        public CheckReferences Reference(string pattern)
        {
            throw new NotSupportedException("No references on files");
        }

        public IPatternContains<IFiles, ICheckFilesContains> FromProject(string pattern)
        {
            return this.GetFilesFromProject(pattern);
        }

        private Files GetFilesFromProject(string pattern)
        {
            return Check.GetProjects(pattern).File(this.pattern);
        }
    }
}