namespace CheckIt.ObjectsFinder
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class FilesObjectsFinder : IObjectsFinder
    {
        private readonly IEnumerable<IFile> files;

        public FilesObjectsFinder(IEnumerable<IFile> files)
        {
            this.files = files;
        }

        public IObjectsFinder Class(string match)
        {
            return new ClassesObjectsFinder(new CheckClasses(this.files.SelectMany(f => f.Class(match))));
        }

        public IObjectsFinder Reference(string pattern)
        {
            throw new NotSupportedException("No references on files");
        }

        public IObjectsFinder Assembly(string pattern)
        {
            throw new NotImplementedException();
        }

        public IObjectsFinder File(string pattern, bool invert)
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
            return this.files.Cast<T>().ToList();
        }
    }
}