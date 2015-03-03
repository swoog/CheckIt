namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IMethods : IEnumerable<IMethod>, IPatternContains<IMethodMatcher, ICheckMethodContains>
    {
        IPatternContains<IMethodMatcher, ICheckMethodContains> FromAssembly(string pattern);
    }
}