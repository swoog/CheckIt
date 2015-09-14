namespace CheckIt
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class CheckProjects : CheckEnumerableBase<CheckProject>, IProjects, IProjectMatcher
    {
        private readonly string basePath;

        private readonly string projectfilePattern;

        private readonly ProjectsObjectsFinder projectsObjectsFinder;

        private List<CheckProject> projects;

        public CheckProjects(string basePath, string projectfilePattern)
        {
            this.basePath = basePath;
            this.projectfilePattern = projectfilePattern;
            this.projectsObjectsFinder = new ProjectsObjectsFinder(this);
        }

        public CheckProjects(List<CheckProject> toList)
        {
            this.projects = toList;
            this.projectsObjectsFinder = new ProjectsObjectsFinder(this);
        }

        public ICheckContains<ICheckProjectContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(this.projectsObjectsFinder));
        }

        public IProjectMatcher Have()
        {
            return this;
        }

        public CheckMatch AssemblyName()
        {
            var assemblies = from p in this.projects
                             select new CheckMatchValue(p.Assembly().Name, p.Assembly().Name, null);

            return new CheckMatch(assemblies.ToList(), "assemblies");
        }

        protected override IEnumerable<CheckProject> Gets()
        {
            if (this.projects != null)
            {
                foreach (var file in this.projects)
                {
                    yield return file;
                }

                yield break;
            }

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
    }
}