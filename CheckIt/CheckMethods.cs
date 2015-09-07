namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckMethods : CheckEnumerableBase<IMethod>, IMethods, IMethodMatcher
    {
        private readonly string pattern;

        private readonly IEnumerable<IMethod> methods;

        public CheckMethods(ICompilationInfo compilationInfo, string pattern)
        {
            this.pattern = pattern;
            this.methods = compilationInfo.Get<IMethod>();
        }

        public CheckMethods(IEnumerable<IMethod> methods, string pattern)
        {
            this.methods = methods;
            this.pattern = pattern;
        }

        public ICheckContains<ICheckMethodContains> Contains()
        {
            throw new NotImplementedException();
        }

        public IMethodMatcher Have()
        {
            return this;
        }

        public IPatternContains<IMethodMatcher, ICheckMethodContains> FromAssembly(string pattern)
        {
            return new CheckMethods(Check.GetProjects().Assembly(pattern).Method(this.pattern), this.pattern);
        }

        public IPatternContains<IMethodMatcher, ICheckMethodContains> FromClass(string pattern)
        {
            return new CheckMethods(Check.GetProjects().Class(pattern).SelectMany(c => c.Method(this.pattern)), this.pattern);
        }

        public CheckMatch Name()
        {
            var checkValues = from m in this select new CheckMatchValue(m.Name, m.Name, m.Position);

            return new CheckMatch(checkValues.ToList(), "method");
        }

        public CheckMatch GenericType()
        {
            var checkMethods = from m in this where m.GenericType != null select m;
            var checkValues = from m in checkMethods.SelectMany(g => g.GenericType)
                              select new CheckMatchValue(m.Name, m.Name, m.Position);

            var checkMatchValues = checkValues.ToList();
            return new CheckMatch(checkMatchValues, "generic type");
        }

        protected override IEnumerable<IMethod> Gets()
        {
            foreach (var method in this.methods)
            {
                if (this.pattern == null || FileUtil.FilenameMatchesPattern(method.Name, this.pattern))
                {
                    yield return method;
                }
            }
        }
    }
}