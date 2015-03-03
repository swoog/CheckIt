namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class CheckClassVisitor : CSharpSyntaxWalker
    {
        private readonly SemanticModel semanticModel;

        private List<object> types = new List<object>();

        public CheckClassVisitor(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node).ContainingNamespace;
            this.types.Add(new CheckClass(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString()));
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node).ContainingNamespace;
            this.types.Add(new CheckInterface(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString()));
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            this.types.Add(new CheckMethod(node.Identifier.ValueText));
        }

        public IEnumerable<T> Get<T>()
        {
            return this.types.OfType<T>();
        }
    }

    internal class CheckMethod : IMethod
    {
        public CheckMethod(string name)
        {
            this.Name = name;
        }

        public string Name { get; private set; }
    }
}