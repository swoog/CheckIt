namespace CheckIt
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Microsoft.CodeAnalysis;

    public class CheckTypes<T> : IEnumerable<T>
        where T : CheckType
    {
        private readonly string typeName;

        protected IEnumerable<T> classes;

        protected CheckTypes(Project project, Compilation compile, string pattern, string typeName)
        {
            this.classes = this.Get(project, pattern, compile);
            this.typeName = typeName;
        }

        protected CheckTypes(IEnumerable<T> classes, string typeName)
        {
            this.classes = classes;
            this.typeName = typeName;
        }

        private static SyntaxTree GetSyntaxTreeAsync(Document document)
        {
            var st = document.GetSyntaxTreeAsync();

            st.Wait();

            return st.Result;
        }

        protected IEnumerable<T> Get(Project currentProject, string interfacePattern, Compilation compile)
        {
            foreach (var document in currentProject.Documents)
            {
                var syntaxTreeAsync = GetSyntaxTreeAsync(document);
                var checkClasses = this.Visit(syntaxTreeAsync, compile);

                foreach (var checkClass in checkClasses)
                {
                    if (Regex.Match(checkClass.Name, interfacePattern).Success)
                    {
                        yield return checkClass;
                    }
                }
            }
        }

        private IEnumerable<T> Visit(SyntaxTree syntaxTreeAsync, Compilation compile)
        {
            var semanticModel = compile.GetSemanticModel(syntaxTreeAsync);

            var visitor = new CheckClassVisitor(semanticModel);

            visitor.Visit(syntaxTreeAsync.GetRoot());

            return visitor.Get<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var checkClass in this.classes)
            {
                yield return checkClass;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public void HasAny()
        {
            if (this.Count() == 0)
            {
                throw new MatchException("No class found");
            }
        }

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
    }
}