namespace CheckIt
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class CheckProjects : CheckEnumerableBase<IProject>, IProjects, IProjectMatcher
    {
        private readonly IEnumerable<FileInfo> files;

        private readonly string projectfilePattern;

        public CheckProjects(IEnumerable<FileInfo> files, string pattern)
        {
            this.files = files;
            this.projectfilePattern = pattern;
        }

        public ICheckContains<ICheckProjectContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(new ProjectsObjectsFinder(this)));
        }

        public IProjectMatcher Have()
        {
            return this;
        }

        public CheckMatch AssemblyName()
        {
            var assemblies = from p in this
                             select new CheckMatchValue(p.Assembly().Name, p.Assembly().Name, null);

            return new CheckMatch(assemblies.ToList(), "assemblies");
        }

        public CheckMatch Name()
        {
            var assemblies = from p in this
                             select new CheckMatchValue(p.Name, p.Name, null);

            return new CheckMatch(assemblies.ToList(), "projects");
        }

        protected override IEnumerable<IProject> Gets()
        {
            var hasFiles = false;
            foreach (var file in this.files)
            {
                hasFiles = true;
                yield return new CheckProject(file);
            }

            if (!hasFiles)
            {
                throw new MatchException(string.Format("No project found that match '{0}'.", this.projectfilePattern));
            }
        }
    }
}