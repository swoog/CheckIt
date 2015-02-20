namespace CheckIt
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    public class CheckProjects : CheckEnumerableBase<CheckProject>, IProjects
    {
        private readonly string basePath;

        private readonly string projectfilePattern;

        public CheckProjects(string basePath, string projectfilePattern)
        {
            this.basePath = basePath;
            this.projectfilePattern = projectfilePattern;
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

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.GetClassess(classPattern));
        }

        private IEnumerable<CheckClass> GetClassess(string classPattern)
        {
            return this.SelectMany(s => s.Class(classPattern));
        }

        public CheckAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(this.Select(s => s.Assembly()), matchAssemblies);
        }

        public CheckFiles File(string matchFiles)
        {
            return new CheckFiles(this.SelectMany(p => p.File(matchFiles)));
        }

        public CheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(this.GetInterfaces(pattern));
        }

        private IEnumerable<CheckInterface> GetInterfaces(string pattern)
        {
            return this.SelectMany(c => c.Interface(pattern));
        }

        public ICheckContains<ICheckProjectContains> Contains()
        {
            return new CheckContains<CheckProjectContains>(new CheckProjectContains(this, null));
        }

        public IProjects Have()
        {
            throw new System.NotImplementedException();
        }

        public CheckReferences Reference(string pattern)
        {
            return new CheckReferences(this.SelectMany(p => p.Reference(pattern)));
        }
    }
}