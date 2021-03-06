namespace CheckIt.ObjectsFinder
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class ClassesObjectsFinder : IObjectsFinder
    {
        private readonly IEnumerable<IClass> checkClasses;

        public ClassesObjectsFinder(IEnumerable<IClass> checkClasses)
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

        public IObjectsFinder File(string pattern, bool invert)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            throw new System.NotImplementedException();
        }

        public IObjectsFinder Method(string pattern)
        {
            return new MethodsObjectsFinder(this.checkClasses.SelectMany(c => c.Method(pattern)));
        }

        public List<T> ToList<T>()
        {
            return this.checkClasses.Cast<T>().ToList();
        }

        public IObjectsFinder Project(string pattern, bool invert)
        {
            throw new System.NotImplementedException();
        }
    }
}