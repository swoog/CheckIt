namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    public class CheckMethods : CheckEnumerableBase<IMethod>, IMethods, IMethodMatcher
    {
        private readonly string pattern;

        private readonly IEnumerable<IMethod> methods;

        public CheckMethods(string pattern)
        {
            this.pattern = pattern;
        }

        public CheckMethods(ICompilationInfo compilationInfo, string pattern)
        {
            this.pattern = pattern;
            this.methods = compilationInfo.Get<IMethod>();
        }

        public CheckMethods(IEnumerable<IMethod> methods)
        {
            this.methods = methods;
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
            return new CheckMethods(Check.GetProjects().Assembly(pattern).Method(this.pattern));
        }

        public IPatternContains<IMethodMatcher, ICheckMethodContains> FromClass(string pattern)
        {
            return new CheckMethods(Check.GetProjects().Class(pattern).SelectMany(c => c.Method(this.pattern)));
        }

        public CheckMatch Name()
        {
            var checkValues = from m in this select new CheckMatchValue(m.Name, m.Name, m.Position);

            return new CheckMatch(checkValues.ToList(), "method");
        }
    }
}