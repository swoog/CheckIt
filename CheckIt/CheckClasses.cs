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

        public CheckClasses(Project project, Compilation compile, string pattern)
            : base(project, compile, pattern, "class")
        {
        }

        public CheckClasses(Document document, Compilation compile, string pattern)
            : base(document, compile, pattern, "class")
        {
        }

        public CheckClasses(string pattern)
            : base(pattern, "class")
        {
        }

        protected override CheckClasses GetFromProject(string pattern)
        {
            return new CheckProjects(Check.basePath, pattern).Class(this.pattern);
        }

        public ICheckContains<ICheckClassesContains> Contains()
        {
            return new CheckContains<CheckClassContains>(new CheckClassContains());
        }


        public IClasses Have()
        {
            return this;
        }

        public IPatternContains<IClasses, ICheckClassesContains> FromAssembly(string pattern)
        {
            return new CheckProjects(Check.basePath, "*.csproj").Assembly(pattern).Class(this.pattern);
        }
    }
}