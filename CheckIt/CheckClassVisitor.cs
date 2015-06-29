namespace CheckIt
{
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using System.Linq;

    using CheckIt.Syntax;

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

            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node);

            var immutableArray = namedTypeSymbol.TypeArguments;
            this.types.Add(new CheckMethod(node.Identifier.ValueText, position, this.currentType, GetTypes(immutableArray, position).ToList()));

            base.VisitMethodDeclaration(node);
        }

        public override void VisitAccessorDeclaration(AccessorDeclarationSyntax node)
        {
            base.VisitAccessorDeclaration(node);
        }

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var position = GetPosition(node);

            var namedTypeSymbol = this.semanticModel.GetDeclaredSymbol(node.Expression);
            SimpleNameSyntax identifier;
            var memberAccessExpressionSyntax = node.Expression as MemberAccessExpressionSyntax;
            if (memberAccessExpressionSyntax != null)
            {
                identifier = memberAccessExpressionSyntax.Name;
            }
            //else if (node.Expression is GenericNameSyntax)
            //{
            //    identifier = node.Expression as GenericNameSyntax;
            //}
            else
            {
                identifier = node.Expression as SimpleNameSyntax;
            }

            var e = memberAccessExpressionSyntax;
            this.types.Add(new CheckMethod(identifier.Identifier.ValueText, position, this.currentType, null));
            base.VisitInvocationExpression(node);
        }

        private static IEnumerable<IType> GetTypes(IEnumerable<ITypeSymbol> immutableArray, Position position)
        {
            return immutableArray.Select(t => new IntenalType(t.Name, t.ContainingNamespace.ToDisplayString(), position));
        }

        public IEnumerable<T> Get<T>()
        {
            return this.types.OfType<T>();
        }
    }

    internal class IntenalType : IType
    {
        public IntenalType(string name, string nameSpace, Position position)
        {
            this.Name = name;
            this.NameSpace = nameSpace;
            this.Position = position;
        }

        public string Name { get; private set; }

        public string NameSpace { get; private set; }

        public Position Position { get; private set; }

        public IEnumerable<IMethod> Method(string name)
        {
            throw new System.NotImplementedException();
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