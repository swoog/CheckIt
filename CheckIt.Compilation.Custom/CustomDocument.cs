namespace CheckIt.Compilation.Custom
{
    using System.IO;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CustomDocument : ICompilationDocument
    {
        public CustomDocument(FileInfo fileInfo, Compilation compile, SyntaxTree syntaxTree)
        {
            this.SyntaxTree = syntaxTree;
            this.Compile = compile;
            this.Name = fileInfo.Name;
        }

        public SyntaxTree SyntaxTree { get; private set; }

        public string Name { get; private set; }

        public Compilation Compile { get; private set; }
    }
}