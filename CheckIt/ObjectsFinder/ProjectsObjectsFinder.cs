namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class ProjectsObjectsFinder : IObjectsFinder
    {
        private readonly CheckProjects checkProjects;

        public ProjectsObjectsFinder(CheckProjects checkProjects)
        {
            this.checkProjects = checkProjects;
        }

        public IObjectsFinder Class(string pattern)
        {
            return new ClassesObjectsFinder(new CheckClasses(this.checkProjects.SelectMany(s => s.Class(pattern))));
        }

        public IObjectsFinder Assembly(string matchAssemblies)
        {
            return new AssembliesObjectsFinder(this.checkProjects.Select(s => s.Assembly()));
        }

        public IObjectsFinder File(string matchFiles)
        {
            return new FilesObjectsFinder(this.checkProjects.SelectMany(p => p.File(matchFiles)));
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

        public IObjectsFinder Reference(string pattern)
        {
            return new ReferencesObjectsFinder(new CheckReferences(this.checkProjects.SelectMany(p => p.Reference(pattern))));
        }
    }
}