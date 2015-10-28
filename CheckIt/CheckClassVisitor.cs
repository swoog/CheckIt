namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class CheckClassVisitor : CSharpSyntaxWalker
    {
        private readonly ICompilationDocument document;

        private readonly SemanticModel semanticModel;

        private readonly List<object> types = new List<object>();

        private readonly ICompilationInfo compilationInfo;

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
            this.currentType = new CheckInterface(node.Identifier.ValueText, namedTypeSymbol.ToDisplayString(), this.compilationInfo, position);
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

        public override void VisitInvocationExpression(InvocationExpressionSyntax node)
        {
            var position = GetPosition(node);
            var name = string.Empty;
            SimpleNameSyntax identifier;
            IList<IType> typesList = null;
            if (node.Expression is MemberAccessExpressionSyntax)
            {
                var memberAccessExpressionSyntax = node.Expression as MemberAccessExpressionSyntax;
                if (memberAccessExpressionSyntax.Name is GenericNameSyntax)
                {
                    var genericNameSyntax = memberAccessExpressionSyntax.Name as GenericNameSyntax;
                    typesList = GetTypes(genericNameSyntax.TypeArgumentList).ToList();
                    identifier = genericNameSyntax;
                }
                else
                {
                    identifier = memberAccessExpressionSyntax.Name;
                }
                name = identifier.Identifier.ValueText;
            }
            else if (node.Expression is SimpleNameSyntax)
            {
                identifier = node.Expression as SimpleNameSyntax;
                name = identifier.Identifier.ValueText;
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                this.types.Add(new CheckMethod(name, position, this.currentType, typesList));
            }
            base.VisitInvocationExpression(node);
        }

        private IEnumerable<IType> GetTypes(TypeArgumentListSyntax typeArgumentList)
        {
            foreach (TypeSyntax typeSyntax in typeArgumentList.Arguments)
            {
                if (typeSyntax is IdentifierNameSyntax)
                {
                    var t = typeSyntax as IdentifierNameSyntax;
                    yield return new IntenalType(t.Identifier.Text, t.Identifier.Text,  GetPosition(typeArgumentList));
                }
                else if (typeSyntax is PredefinedTypeSyntax)
                {
                    var t = typeSyntax as PredefinedTypeSyntax;
                    yield return new IntenalType(t.Keyword.Text, t.Keyword.Text,  GetPosition(typeArgumentList));
                }
            }
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
