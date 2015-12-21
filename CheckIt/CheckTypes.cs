namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal abstract class CheckTypes<T, T2, T3, T4> : CheckEnumerableBase<T>
        where T : IType
        where T3 : IEnumerable<T>, IPatternContains<T2, T4>
    {
        protected readonly string Pattern;

        private readonly string typeName;

        private readonly IEnumerable<T> classes;

        protected CheckTypes(ICompilationInfo compile, string pattern, string typeName)
            : this(compile.Get<T>(), pattern, typeName)
        {
        }

        protected CheckTypes(ICompilationDocument document, ICompilationInfo compile, string pattern, string typeName)
            : this(compile.Get<T>(document), pattern, typeName)
        {
        }

        protected CheckTypes(IEnumerable<T> classes, string typeName)
        {
            this.classes = classes;
            this.typeName = typeName;
        }

        protected CheckTypes(string pattern, string typeName)
        {
            this.Pattern = pattern;
            this.typeName = typeName;
        }

        private CheckTypes(IEnumerable<T> classes, string pattern, string typeName)
            : this(classes.Where(c => FileUtil.FilenameMatchesPattern(c.Name, pattern)), typeName)
        {
        }

        public CheckMatch Name()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.Name, c.Position)).ToList();

            return new CheckMatch(values, this.typeName);
        }

        public CheckMatch NameSpace()
        {
            var values = this.Select(c => new CheckMatchValue(c.Name, c.NameSpace, c.Position)).ToList();

            return new CheckMatch(values, this.typeName);
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
                var foundClass = false;
                foreach (var checkType in this.GetTypes())
                {
                    if (FileUtil.FilenameMatchesPattern(checkType.Name, this.Pattern))
                    {
                        foundClass = true;
                    yield return checkType;
                }
            }

                if (!foundClass)
        {
                    throw new MatchException("No class found that match '{0}'.", this.Pattern);
                }
        }
        }

        protected abstract IEnumerable<T> GetTypes();
    }
}