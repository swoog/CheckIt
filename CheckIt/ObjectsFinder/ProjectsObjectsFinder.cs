namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class ProjectsObjectsFinder : IObjectsFinder
    {
        private readonly IEnumerable<IProject> checkProjects;

        public ProjectsObjectsFinder(IEnumerable<IProject> checkProjects)
        {
            this.checkProjects = checkProjects;
        }

        public IObjectsFinder Class(string pattern)
        {
            return new ClassesObjectsFinder(new CheckClasses(this.checkProjects.SelectMany(s => s.Class(pattern))));
        }

        public IObjectsFinder Assembly(string matchAssemblies)
        {
            return
                new AssembliesObjectsFinder(
                    this.checkProjects.Select(s => s.Assembly())
                        .Where(a => FileUtil.FilenameMatchesPattern(a.FileName, matchAssemblies)));
        }

        public IObjectsFinder File(string matchFiles, bool invert)
        {
            return
                new FilesObjectsFinder(
                    this.checkProjects.SelectMany(p => p.File())
                        .Where(f => invert ^ FileUtil.FilenameMatchesPattern(f.Name, matchFiles)));
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            return new InterfacesObjectsFinder(this.checkProjects.SelectMany(c => c.Interface(pattern)));
        }

        public IObjectsFinder Method(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public List<T> ToList<T>()
        {
            return this.checkProjects.Cast<T>().ToList();
        }

        public IObjectsFinder Project(string pattern, bool invert)
        {
            return
                new ProjectsObjectsFinder(
                    this.checkProjects.Where(f => invert ^ FileUtil.FilenameMatchesPattern(f.Name, pattern)));
        }

        public IObjectsFinder Reference(string pattern)
        {
            return new ReferencesObjectsFinder(new CheckReferences(this.checkProjects.SelectMany(p => p.Reference(pattern))));
        }
    }
}