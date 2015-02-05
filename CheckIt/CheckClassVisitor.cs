namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    internal class CheckClassVisitor : CSharpSyntaxWalker
    {
        private readonly SemanticModel semanticModel;

        private List<CheckClass> classes = new List<CheckClass>();

        private List<CheckInterface> interfaces = new List<CheckInterface>();

        public CheckClassVisitor(SemanticModel semanticModel)
        {
            this.semanticModel = semanticModel;
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            this.classes.Add(new CheckClass(node.Identifier.ValueText, this.semanticModel.GetDeclaredSymbol(node).ToDisplayString()));
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            this.interfaces.Add(new CheckInterface(node.Identifier.ValueText));
        }

        public List<CheckClass> GetClasses()
        {
            return this.classes;
        }

        public IEnumerable<CheckInterface> GetInterfaces()
        {
            return this.interfaces;
        }
    }
}