namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CheckClasses : CheckTypes<CheckClass, CheckClasses, IClasses, ICheckContains>, IPatternContains<IClasses, ICheckContains>, IClasses
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

        public ICheckContains Contains()
        {
            return new CheckContains<CheckClasses, CheckClass>(this);
        }

        public IClasses Have()
        {
            return this;
        }

        public IPatternContains<IClasses, ICheckContains> FromAssembly(string pattern)
        {
            return new CheckProjects(Check.basePath, "*.csproj").Assembly(pattern).Class(this.pattern);
        }
    }

    public interface IClasses
    {
        CheckMatch NameSpace();

        CheckMatch Name();
    }
}