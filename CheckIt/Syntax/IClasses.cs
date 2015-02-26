namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IClasses : IEnumerable<IClass>, IPatternContains<IClassMatcher, ICheckClassesContains>
    {
    }
}