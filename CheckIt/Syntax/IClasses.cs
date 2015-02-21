namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IClasses : IEnumerable<IClass>, IPatternContains<IClasses, ICheckClassesContains>
    {
        CheckMatch NameSpace();

        CheckMatch Name();
    }
}