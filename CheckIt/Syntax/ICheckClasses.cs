namespace CheckIt.Syntax
{
    using System.Collections;

    public interface ICheckClasses : IClasses
    {
        IPatternContains<IClasses, ICheckClassesContains> FromAssembly(string pattern);

        IPatternContains<IClasses, ICheckClassesContains> FromProject(string pattern);
    }
}