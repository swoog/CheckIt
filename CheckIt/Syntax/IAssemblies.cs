namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IAssemblies : IEnumerable<CheckAssembly>, IPatternContains<IAssemblyMatcher, ICheckAssemblyContains>
    {
    }
}