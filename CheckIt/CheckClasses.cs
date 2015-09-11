namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class CheckClasses : CheckTypes<IClass, IClassMatcher, ICheckClasses, ICheckClassesContains>, ICheckClasses, IClassMatcher
    {
        public CheckClasses(IEnumerable<IClass> classes)
            : base(classes, "class")
        {
        }

        public CheckClasses(ICompilationDocument document, ICompilationInfo compile, string pattern)
            : base(document, compile, pattern, "class")
        {
        }

        public CheckClasses(string pattern)
            : base(pattern, "class")
        {
        }

        public CheckClasses(ICompilationInfo compilationInfo, string pattern)
            : base(compilationInfo, pattern, "class")
        {
        }

        private CheckClasses(IObjectsFinder objectsFinder)
            : this(objectsFinder.ToList<IClass>())
        {
        }

        public ICheckContains<ICheckClassesContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains());
        }

        public IClassMatcher Have()
        {
            return this;
        }

        public IPatternContains<IClassMatcher, ICheckClassesContains> FromAssembly(string pattern)
        {
            return Class(Check.GetProjects().Assembly(pattern), this.Pattern);
        }

        public IPatternContains<IClassMatcher, ICheckClassesContains> FromFile(string pattern)
        {
            return Class(Check.GetProjects().File(pattern), this.Pattern);
        }

        private CheckClasses Class(IObjectsFinder assemblies, string pattern)
        {
            return new CheckClasses(assemblies.Class(pattern));
        }

        protected override ICheckClasses GetFromProject(string pattern)
        {
            return new CheckClasses(Check.GetProjects(pattern).Class(this.Pattern));
        }
    }
}