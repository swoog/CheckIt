namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    internal class AssembliesObjectsFinder : IObjectsFinder
    {
        private CheckAssemblies checkAssemblies;

        public AssembliesObjectsFinder(CheckAssemblies checkAssemblies)
        {
            this.checkAssemblies = checkAssemblies;
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            return new CheckInterfaces(this.checkAssemblies.SelectMany(a => a.Interface(pattern)));
        }

        public IObjectsFinder Method(string pattern)
        {
            return new CheckMethods(this.checkAssemblies.SelectMany(a => a.Method(pattern)), pattern);
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

        public IObjectsFinder File(string pattern)
        {
            throw new System.NotImplementedException();
        }
    }
}