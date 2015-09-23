namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Compilation;
    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    internal class CheckClasses : CheckTypes<IClass, IClassMatcher, ICheckClasses, ICheckClassesContains>, ICheckClasses, IClassMatcher
    {
        private readonly IObjectsFinder objectsFinder;

        private readonly bool invert;

        public CheckClasses(IEnumerable<IClass> classes, bool invert = false)
            : base(classes, "class")
        {
            this.invert = invert;
        }

        public CheckClasses(ICompilationDocument document, ICompilationInfo compile, string pattern)
            : base(document, compile, pattern, "class")
        {
        }

        public CheckClasses(ICompilationInfo compilationInfo, string pattern)
            : base(compilationInfo, pattern, "class")
        {
        }

        public CheckClasses(IObjectsFinder objectsFinder, string pattern, bool invert = false)
            : base(pattern, "class")
        {
            this.objectsFinder = objectsFinder;
            this.invert = invert;
        }

        public ICheckContains<ICheckClassesContains> Contains()
        {
            return new CheckContains(new CheckSpecificContains(new ClassesObjectsFinder(this)));
        }

        public IClassMatcher Have()
        {
            return this;
        }

        public IPatternContains<IClassMatcher, ICheckClassesContains> FromAssembly(string pattern)
        {
            return Class(this.objectsFinder.Assembly(pattern));
        }

        public IPatternContains<IClassMatcher, ICheckClassesContains> FromProject(string pattern)
        {
            return Class(this.objectsFinder.Project(pattern, this.invert));
        }

        public IPatternContains<IClassMatcher, ICheckClassesContains> FromFile(string pattern)
        {
            return Class(this.objectsFinder.File(pattern, this.invert));
        }

        public ICheckClasses Not()
        {
            return new CheckClasses(this.objectsFinder, this.Pattern, !this.invert);
        }

        private CheckClasses Class(IObjectsFinder assemblies)
        {
            return new CheckClasses(assemblies, this.Pattern);
        }

        protected override IEnumerable<IClass> GetTypes()
        {
            return this.objectsFinder.Class(this.Pattern).ToList<IClass>();
        }
    }
}