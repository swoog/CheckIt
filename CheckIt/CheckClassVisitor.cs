namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class CheckClassVisitor : CSharpSyntaxWalker
    {
        private readonly ICompilationDocument document;

        private readonly SemanticModel semanticModel;

        private List<object> types = new List<object>();

        private ICompilationInfo compilationInfo;

        private IType currentType;

        public CheckClassVisitor(ICompilationDocument document, SemanticModel semanticModel, ICompilationInfo compilationInfo)
        {
            this.document = document;
            this.semanticModel = semanticModel;
            this.compilationInfo = compilationInfo;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            var position = GetPosition(node);
            
            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node).ContainingNamespace;
            this.currentType = new CheckClass(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString(), this.compilationInfo, position);
            this.types.Add(this.currentType);
            base.VisitClassDeclaration(node);
        }

        private Position GetPosition(SyntaxNode node)
        {
            var p = node.SyntaxTree.GetLineSpan(node.Span).StartLinePosition;

            return new Position(p.Line, this.document.Name);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            var position = GetPosition(node);

            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node).ContainingNamespace;
            this.currentType = new CheckInterface(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString(), position);
            this.types.Add(this.currentType);
            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var position = GetPosition(node);

            this.types.Add(new CheckMethod(node.Identifier.ValueText, position, this.currentType));
        }

        public IEnumerable<T> Get<T>()
        {
            return this.types.OfType<T>();
        }
    }

    public class Position
    {
        public Position(int line, string name)
        {
            this.Line = line;
            this.Name = name;
        }

        public int Line { get; private set; }

        public string Name { get; private set; }
    }
}