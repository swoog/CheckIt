namespace CheckIt
{
    using System.IO;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

    public class CheckProject
    {
        private readonly FileInfo file;

        private CompilationInfo compilationInfo;

        public CheckProject(FileInfo file)
        {
            this.file = file;

            var project = this.OpenProjectAsync(this.file.FullName);

            var compile = project.GetCompilationAsync().Result;

            this.compilationInfo = new CompilationInfo { Project = project, Compile = compile };
        }

        public CheckClasses Class(string classPattern)
        {
            return new CheckClasses(this.compilationInfo, classPattern);
        }

        private Project OpenProjectAsync(string fileName)
        {
            var msBuildWorkspace = MSBuildWorkspace.Create();
            var t = msBuildWorkspace.OpenProjectAsync(fileName);

            t.Wait();

            return t.Result;
        }

        public CheckAssembly Assembly()
        {
            return new CheckAssembly(this.compilationInfo);
        }

        public CheckFiles File(string pattern)
        {
            var project = this.OpenProjectAsync(this.file.FullName);

            var compile = project.GetCompilationAsync().Result;

            return new CheckFiles(this.compilationInfo, pattern);
        }

        public CheckInterfaces Interface(string pattern)
        {
            return new CheckInterfaces(this.compilationInfo, pattern);
        }
    }
}