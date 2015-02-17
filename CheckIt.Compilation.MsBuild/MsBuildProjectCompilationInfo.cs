namespace CheckIt.Compilation.MsBuild
{
    using System.Collections.Generic;
    using System.Linq;

    using Microsoft.CodeAnalysis;

    public class MsBuildProjectCompilationInfo : ICompilationProject
    {
        private readonly Project project;

        private Compilation compile;

        public MsBuildProjectCompilationInfo(Project project, Compilation compile)
        {
            this.project = project;
            this.compile = compile;
        }

        public string AssemblyName
        {
            get
            {
                return this.project.AssemblyName;
            }
        }

        public List<ICompilationDocument> Documents
        {
            get
            {
                return this.project.Documents.Select(d => new MsBuildCompilationDocument(d, this.compile)).Cast<ICompilationDocument>().ToList();
            }
        }
    }
}