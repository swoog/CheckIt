namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    internal class ClassesObjectsFinder
    {
        private readonly CheckClasses checkClasses;

        public ClassesObjectsFinder(CheckClasses checkClasses)
        {
            this.checkClasses = checkClasses;
        }

        public IObjectsFinder Class(string pattern)
        {
            throw new System.NotImplementedException();
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

        public IObjectsFinder Interfaces(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder Method(string pattern)
        {
            return new CheckMethods(this.checkClasses.SelectMany(c => c.Method(pattern)), pattern);
        }

        public List<T> ToList<T>()
        {
            return this.checkClasses.Cast<T>().ToList();
        }
    }
}