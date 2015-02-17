namespace CheckIt.Compilation.Custom
{
    using System.Collections.Generic;

    public class CustomProject : ICompilationProject
    {
        public CustomProject(string assemblyName, List<ICompilationDocument> documents)
        {
            this.AssemblyName = assemblyName;
            this.Documents = documents;
        }

        public string AssemblyName { get; private set; }

        public List<ICompilationDocument> Documents { get; private set; }
    }
}