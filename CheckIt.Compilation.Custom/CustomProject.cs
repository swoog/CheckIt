namespace CheckIt.Compilation.Custom
{
    using System.Collections.Generic;

    public class CustomProject : ICompilationProject
    {
        public CustomProject(string assemblyName, List<ICompilationDocument> documents, List<ICompilationReference> references, string name)
        {
            this.References = references;
            this.AssemblyName = assemblyName;
            this.Documents = documents;
            this.Name = name;
        }

        public string AssemblyName { get; private set; }

        public List<ICompilationDocument> Documents { get; private set; }

        public List<ICompilationReference> References { get; private set; }

        public string Name { get; private set; }
    }
}