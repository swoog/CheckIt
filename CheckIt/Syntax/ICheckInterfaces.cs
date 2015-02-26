namespace CheckIt.Syntax
{
    public interface ICheckInterfaces : IInterfaces
    {
        IPatternContains<IInterfaceMatcher, ICheckInterfacesContains> FromAssembly(string pattern);
    }
}