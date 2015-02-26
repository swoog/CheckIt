namespace CheckIt.Compilation.Custom
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Xml;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.CSharp;

    public class CustomCompilationInfoFactory : ICompilationInfoFactory
    {
        public ICompilationInfo GetCompilationInfo(FileInfo file)
        {
            var document = new XmlDocument();

            document.Load(file.FullName);

            var nameTable = new NameTable();
            var xmlNamespaceManager = new XmlNamespaceManager(nameTable);
            xmlNamespaceManager.AddNamespace("c", "http://schemas.microsoft.com/developer/msbuild/2003");
            var assemblyName = document.SelectSingleNode("//c:AssemblyName", xmlNamespaceManager).InnerText;

            var files = document.SelectNodes("//c:Compile", xmlNamespaceManager);

            var q = from f in files.Cast<XmlNode>() select new FileInfo(Path.Combine(file.Directory.FullName, f.Attributes["Include"].Value));

            var documents = (from f in q select new { FileInfo = f, SyntaxTree = this.CreateSyntaxTree(f) }).ToList();

            var compile = CSharpCompilation.Create(assemblyName, documents.Select(d => d.SyntaxTree));

            var compilationDocuments = documents.Select(f => this.CreateDocument(f.FileInfo, f.SyntaxTree, compile)).Cast<ICompilationDocument>().ToList();
            var compilationReferences =
                document.SelectNodes("//c:Reference", xmlNamespaceManager)
                    .Cast<XmlNode>()
                    .Select(n => new CustomReference(n.Attributes["Include"].Value)).Cast<ICompilationReference>().ToList();
            var customProject = new CustomProject(assemblyName, compilationDocuments, compilationReferences);

            return new CustomCompilationInfoBase(customProject);
        }

        private CustomDocument CreateDocument(FileInfo fileInfo, SyntaxTree syntaxTree, CSharpCompilation compile)
        {
            return new CustomDocument(fileInfo, compile, syntaxTree);
        }

        private SyntaxTree CreateSyntaxTree(FileInfo fileInfo)
        {
            var s = File.ReadAllText(fileInfo.FullName);

            return CSharpSyntaxTree.ParseText(s);
        }
    }
}