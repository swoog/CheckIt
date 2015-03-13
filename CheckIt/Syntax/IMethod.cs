namespace CheckIt.Syntax
{
    public interface IMethod
    {
        string Name { get; }

        Position Position { get; }

        IType Type { get; }
    }
}