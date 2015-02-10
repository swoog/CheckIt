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

        protected CheckTypes(Project project, Compilation compile, string pattern, string typeName)
            : this(Get(project, compile), pattern, typeName)
        {
        }

        protected CheckTypes(Document document, Compilation compile, string pattern, string typeName)
            : this(Get(document, compile), pattern, typeName)
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

        private static SyntaxTree GetSyntaxTreeAsync(Document document)
        {
            var st = document.GetSyntaxTreeAsync();

            st.Wait();

            return st.Result;
        }

        private static IEnumerable<T> Get(Document document, Compilation compile)
        {
            var syntaxTreeAsync = GetSyntaxTreeAsync(document);
            var checkClasses = Visit(syntaxTreeAsync, compile);

            foreach (var checkClass in checkClasses)
            {
                yield return checkClass;
            }
        }

        protected static IEnumerable<T> Get(Project currentProject, Compilation compile)
        {
            foreach (var document in currentProject.Documents)
            {
                foreach (var checkClass in Get(document, compile))
                {
                    yield return checkClass;
                }
            }
        }

        private static IEnumerable<T> Visit(SyntaxTree syntaxTreeAsync, Compilation compile)
        {
            var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);

            var visitor = new CheckClassVisitor(semanticModel);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.Get<T>();
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