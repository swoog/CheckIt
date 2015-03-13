namespace CheckIt
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using CheckIt.Syntax;

    internal class CheckProjects : CheckEnumerableBase<CheckProject>, IProjects, IObjectsFinder
    {
        private readonly string basePath;

        private readonly string projectfilePattern;

        public CheckProjects(string basePath, string projectfilePattern)
        {
            this.basePath = basePath;
            this.projectfilePattern = projectfilePattern;
        }

        public IEnumerable<IClass> Class(string pattern)
        {
            return new CheckClasses(this.SelectMany(s => s.Class(pattern)));
        }

        public CheckAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(this.Select(s => s.Assembly()), matchAssemblies);
        }

        public Files File(string matchFiles)
        {
            return new Files(this.SelectMany(p => p.File(matchFiles)));
        }

        public CheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(this.GetInterfaces(pattern));
        }

        public ICheckContains<ICheckProjectContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(this));
        }

        public IProjects Have()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IReference> Reference(string pattern)
        {
            return new CheckReferences(this.SelectMany(p => p.Reference(pattern)));
        }

        protected override IEnumerable<CheckProject> Gets()
        {
            foreach (var file in this.GetFiles())
            {
                yield return new CheckProject(file);
            }
        }

        private IEnumerable<FileInfo> GetFiles()
        {
            var hasFiles = false;
            foreach (var file in this.GetFiles(this.basePath))
            {
                hasFiles = true;
                yield return file;
            }

            if (!hasFiles)
            {
                throw new MatchException(string.Format("No project found that match '{0}'.", this.projectfilePattern));
            }
        }

        private IEnumerable<IInterface> GetInterfaces(string pattern)
        {
            return this.SelectMany(c => c.Interface(pattern));
        }

        private IEnumerable<FileInfo> GetFiles(string path)
        {
            foreach (var file in Directory.GetFiles(path, this.projectfilePattern))
            {
                yield return new FileInfo(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                foreach (var fileInfo in this.GetFiles(directory))
                {
                    yield return fileInfo;
                }
            }
        }
    }
}