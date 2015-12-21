namespace CheckIt.Compilation
{
    using Microsoft.CodeAnalysis;

    public interface ICompilationDocument
    {
        SyntaxTree SyntaxTree { get; }

        string Name { get; }

        string FullName { get; }

        Compilation Compile { get; }
    }
}