namespace CheckIt.Syntax
{
    using System.Collections;

    public interface ICheckClasses : IClasses
    {
        IPatternContains<IClassMatcher, ICheckClassesContains> FromAssembly(string pattern);

        IPatternContains<IClassMatcher, ICheckClassesContains> FromProject(string pattern);

        IPatternContains<IClassMatcher, ICheckClassesContains> FromFile(string pattern);

        ICheckClasses Not();
    }
}