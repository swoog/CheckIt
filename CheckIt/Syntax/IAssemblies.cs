namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IAssemblies : IEnumerable<IAssembly>, IPatternContains<IAssemblyMatcher, ICheckAssemblyContains>
    {
    }
}