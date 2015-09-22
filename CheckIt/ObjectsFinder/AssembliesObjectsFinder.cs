namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class AssembliesObjectsFinder : IObjectsFinder
    {
        private readonly IEnumerable<IAssembly> checkAssemblies;

        public AssembliesObjectsFinder(IEnumerable<IAssembly> checkAssemblies)
        {
            this.checkAssemblies = checkAssemblies;
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            return new InterfacesObjectsFinder(this.checkAssemblies.SelectMany(a => a.Interface(pattern)));
        }

        public IObjectsFinder Method(string pattern)
        {
            return new MethodsObjectsFinder(this.checkAssemblies.SelectMany(a => a.Method(pattern)));
        }

        public List<T> ToList<T>()
        {
            return this.checkAssemblies.Cast<T>().ToList();
        }

        public IObjectsFinder Class(string pattern)
        {
            return new ClassesObjectsFinder(new CheckClasses(this.checkAssemblies.SelectMany(f => f.Class(pattern))));
        }

        public IObjectsFinder Reference(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder Assembly(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder File(string pattern, bool invert)
        {
            throw new System.NotImplementedException();
        }
    }
}