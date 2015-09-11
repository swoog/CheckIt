namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class Files : CheckEnumerableBase<IFile>, IObjectsFinder, IFiles
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

        private Files(IObjectsFinder getFilesFromProject)
        {
            this.checkFiles = getFilesFromProject as IEnumerable<IFile>;
        }

        public ICheckContains<ICheckFilesContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(this));
        }

        public IFiles Have()
        {
            return this;
        }

        public IObjectsFinder Class(string match)
        {
            return new ClassesObjectsFinder(new CheckClasses(this.SelectMany(f => f.Class(match))));
        }

        public IObjectsFinder Reference(string pattern)
        {
            throw new NotSupportedException("No references on files");
        }

        public IObjectsFinder Assembly(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder File(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder Method(string pattern)
        {
            throw new NotImplementedException();
        }

        public List<T> ToList<T>()
        {
            throw new NotImplementedException();
        }

        public IPatternContains<IFiles, ICheckFilesContains> FromProject(string pattern)
        {
            return new Files(this.GetFilesFromProject(pattern));
        }

        protected override IEnumerable<IFile> Gets()
        {
            if (this.checkFiles != null)
            {
                return this.checkFiles;
            }
            
            return new Files(this.GetFilesFromProject("*.csproj"));
        }

        private IEnumerable<CheckFile> Gets(ICompilationInfo compilationInfo)
        {
            return from document in compilationInfo.Project.Documents 
                   where FileUtil.FilenameMatchesPattern(document.Name, this.pattern) 
                   select new CheckFile(document, compilationInfo);
        }

        private IObjectsFinder GetFilesFromProject(string pattern)
        {
            return Check.GetProjects(pattern).File(this.pattern);
        }
    }
}