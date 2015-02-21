namespace CheckIt.Syntax
{
    public interface ICheckInterfaces : IInterfaces
    {
        IPatternContains<IInterfaces, ICheckInterfacesContains> FromAssembly(string pattern);
    }
}