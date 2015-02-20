namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CheckClasses : CheckTypes<CheckClass, CheckClasses, IClasses, ICheckClassesContains>, IClasses, IPatternContains<IClasses, ICheckClassesContains>
    {
        public CheckClasses(IEnumerable<CheckClass> classes)
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

        protected override CheckClasses GetFromProject(string pattern)
        {
            return Check.GetProjects(pattern).Class(this.pattern);
        }

        public ICheckContains<ICheckClassesContains> Contains()
        {
            return new CheckContains<CheckSpecificContains>(new CheckSpecificContains());
        }


        public IClasses Have()
        {
            return this;
        }

        public IPatternContains<IClasses, ICheckClassesContains> FromAssembly(string pattern)
        {
            return Check.GetProjects().Assembly(pattern).Class(this.pattern);
        }
    }
}