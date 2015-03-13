namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public class CheckClasses : CheckTypes<IClass, IClassMatcher, ICheckClasses, ICheckClassesContains>, ICheckClasses, IClassMatcher
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

        public ICheckContains<ICheckClassesContains> Contains()
        {
            return new CheckContains<CheckSpecificContains>(new CheckSpecificContains());
        }

        public IClassMatcher Have()
        {
            return this;
        }

        public IPatternContains<IClassMatcher, ICheckClassesContains> FromAssembly(string pattern)
        {
            return Check.GetProjects().Assembly(pattern).Class(this.Pattern);
        }

        protected override ICheckClasses GetFromProject(string pattern)
        {
            return Check.GetProjects(pattern).Class(this.Pattern);
        }
    }
}