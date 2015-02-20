namespace CheckIt.Syntax
{
    using System.Collections;
    using System.Collections.Generic;

    public interface IClasses : IEnumerable<IClass>
    {
        IPatternContains<IClasses, ICheckClassesContains> FromAssembly(string pattern);

        IPatternContains<IClasses, ICheckClassesContains> FromProject(string pattern);

        CheckMatch NameSpace();

        CheckMatch Name();
    }
}