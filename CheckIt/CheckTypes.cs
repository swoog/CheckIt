namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public abstract class CheckTypes<T, T2, T3, T4> : CheckEnumerableBase<T>
        where T : CheckType
        where T2 : IEnumerable<T>, IPatternContains<T3, T4>
    {
        protected readonly string pattern;

        private readonly string typeName;

        private readonly IEnumerable<T> classes;

        protected CheckTypes(ICompilationInfo compile, string pattern, string typeName)
            : this(compile.Get<T>(), pattern, typeName)
        {
        }

        protected CheckTypes(Document document, ICompilationInfo compile, string pattern, string typeName)
            : this(compile.Get<T>(document), pattern, typeName)
        {
        }

        private CheckTypes(IEnumerable<T> classes, string pattern, string typeName)
            : this(classes.Where(c => Regex.Match(c.Name, pattern).Success), typeName)
        {
        }

        protected CheckTypes(IEnumerable<T> classes, string typeName)
        {
            this.classes = classes;
            this.typeName = typeName;
        }

        protected CheckTypes(string pattern, string typeName)
        {
            this.pattern = pattern;
            this.typeName = typeName;
        }

        protected override IEnumerable<T> Gets()
        {
            if (this.classes != null)
            {
                foreach (var checkClass in this.classes)
                {
                    yield return checkClass;
                }
            }
            else
            {
                foreach (var checkType in this.GetFromProject("*.csproj"))
                {
                    yield return checkType;
                }
            }
        }

        protected abstract T2 GetFromProject(string pattern);

        public CheckMatch Name()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.Name)).ToList();

            return new CheckMatch(values, this.typeName);
        }

        public CheckMatch NameSpace()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.NameSpace)).ToList();

            return new CheckMatch(values, this.typeName);
        }

        public IPatternContains<T3, T4> FromProject(string pattern)
        {
            return this.GetFromProject(pattern);
        }
    }
}