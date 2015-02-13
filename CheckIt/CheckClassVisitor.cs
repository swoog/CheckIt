namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class CheckClassVisitor : CSharpSyntaxWalker
    {
        private readonly SemanticModel semanticModel;

        private List<CheckType> types = new List<CheckType>();

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

        public IEnumerable<T> Get<T>()
        {
            return this.types.OfType<T>();
        }
    }
}