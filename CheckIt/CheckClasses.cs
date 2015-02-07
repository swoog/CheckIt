namespace CheckIt
{
    using System;
    using System.Collections;
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
            return new CheckContains<CheckClassContains>(new CheckClassContains(this));
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

    public class CheckClassContains : IContains, ICheckClassesContains
    {
        public CheckClassContains(CheckClasses checkClasses)
        {
            
        }

        public Predicate<IList> Predicate { get; set; }
    }

    public interface ICheckClassesContains
    {
    }

    public interface IClasses
    {
        CheckMatch NameSpace();

        CheckMatch Name();
    }
}