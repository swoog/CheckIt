namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Threading.Tasks;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;
    using Microsoft.CodeAnalysis.CSharp.Syntax;

    public class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return Sources("*.csproj").Assembly(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static CheckSources Sources(string projectfilePattern)
        {
            return new CheckSources(basePath, projectfilePattern);
        }

        public static void SetBasePathSearch(string basePath)
        {
            Check.basePath = Path.Combine(Environment.CurrentDirectory, basePath);
        }
    }

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