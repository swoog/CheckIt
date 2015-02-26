namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IInterfaces : IEnumerable<IInterface>, IPatternContains<IInterfaceMatcher, ICheckInterfacesContains>
    {

    }
}