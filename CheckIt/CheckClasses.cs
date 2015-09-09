namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckClasses : CheckTypes<IClass, IClassMatcher, ICheckClasses, ICheckClassesContains>, ICheckClasses, IClassMatcher, IObjectsFinder
    {
        private readonly ClassesObjectsFinder classesObjectsFinder;

        public CheckClasses(IEnumerable<IClass> classes)
            : base(classes, "class")
        {
            this.classesObjectsFinder = new ClassesObjectsFinder(this);
        }

        public CheckClasses(ICompilationDocument document, ICompilationInfo compile, string pattern)
            : base(document, compile, pattern, "class")
        {
            this.classesObjectsFinder = new ClassesObjectsFinder(this);
        }

        public CheckClasses(string pattern)
            : base(pattern, "class")
        {
            this.classesObjectsFinder = new ClassesObjectsFinder(this);
        }

        public CheckClasses(ICompilationInfo compilationInfo, string pattern)
            : base(compilationInfo, pattern, "class")
        {
            this.classesObjectsFinder = new ClassesObjectsFinder(this);
        }

        private CheckClasses(IObjectsFinder objectsFinder)
            : this(objectsFinder.ToList<IClass>())
        {
            this.classesObjectsFinder = new ClassesObjectsFinder(this);
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

        public IObjectsFinder Class(string pattern)
        {
            return this.classesObjectsFinder.Class(pattern);
        }

        public IObjectsFinder Reference(string pattern)
        {
            return this.classesObjectsFinder.Reference(pattern);
        }

        public IObjectsFinder Assembly(string pattern)
        {
            return this.classesObjectsFinder.Assembly(pattern);
        }

        public IObjectsFinder File(string pattern)
        {
            return this.classesObjectsFinder.File(pattern);
        }

        public IObjectsFinder Interfaces(string pattern)
        {
            return this.classesObjectsFinder.Interfaces(pattern);
        }

        public IObjectsFinder Method(string pattern)
        {
            return this.classesObjectsFinder.Method(pattern);
        }

        public List<T> ToList<T>()
        {
            return this.classesObjectsFinder.ToList<T>();
        }
    }
}