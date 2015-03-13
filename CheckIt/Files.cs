namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    public class Files : CheckEnumerableBase<IFile>, IObjectsFinder, IFiles
    {
        private readonly IEnumerable<IFile> checkFiles;

        private readonly string pattern;

        public Files(string pattern)
        {
            this.pattern = pattern;
        }

        public Files(IEnumerable<IFile> checkFiles)
        {
            this.checkFiles = checkFiles;
        }

        public Files(ICompilationInfo compilationInfo, string pattern)
        {
            this.pattern = pattern;
            this.checkFiles = this.Gets(compilationInfo);
        }

        public ICheckContains<ICheckFilesContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(this));
        }

        public IFiles Have()
        {
            return this;
        }

        public IEnumerable<IClass> Class(string match)
        {
            return new CheckClasses(this.SelectMany(f => f.Class(match)));
        }

        public IEnumerable<IReference> Reference(string pattern)
        {
            throw new NotSupportedException("No references on files");
        }

        public IPatternContains<IFiles, ICheckFilesContains> FromProject(string pattern)
        {
            return this.GetFilesFromProject(pattern);
        }

        protected override IEnumerable<IFile> Gets()
        {
            if (this.checkFiles != null)
            {
                return this.checkFiles;
            }
            
            return this.GetFilesFromProject("*.csproj");
        }

        private IEnumerable<CheckFile> Gets(ICompilationInfo compilationInfo)
        {
            return from document in compilationInfo.Project.Documents 
                   where FileUtil.FilenameMatchesPattern(document.Name, this.pattern) 
                   select new CheckFile(document, compilationInfo);
        }

        private Files GetFilesFromProject(string pattern)
        {
            return Check.GetProjects(pattern).File(this.pattern);
        }
    }
}