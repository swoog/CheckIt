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

        private List<object> types = new List<object>();

        private ICompilationInfo compilationInfo;

        public CheckClassVisitor(SemanticModel semanticModel, ICompilationInfo compilationInfo)
        {
            this.semanticModel = semanticModel;
            this.compilationInfo = compilationInfo;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node).ContainingNamespace;
            this.types.Add(new CheckClass(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString(), compilationInfo));
            base.VisitClassDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node).ContainingNamespace;
            this.types.Add(new CheckInterface(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString()));
            base.VisitInterfaceDeclaration(node);
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
}