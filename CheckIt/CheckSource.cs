namespace CheckIt
{
    using System.IO;

    using Microsoft.CodeAnalysis;
    using Microsoft.CodeAnalysis.MSBuild;

    public class CheckSource
    {
        private readonly FileInfo file;

        public CheckSource(FileInfo file)
        {
            this.file = file;
        }

        public CheckClasses Class(string classPattern)
        {
            var project = this.OpenProjectAsync();

            var compile = project.GetCompilationAsync().Result;

            return new CheckClasses(project, compile, classPattern);
        }

        private Project OpenProjectAsync()
        {
            var msBuildWorkspace = MSBuildWorkspace.Create();
            var t = msBuildWorkspace.OpenProjectAsync(this.file.FullName);

            t.Wait();

            return t.Result;
        }

        public CheckAssembly Assembly()
        {
            var project = this.OpenProjectAsync();

            var compile = project.GetCompilationAsync().Result;

            return new CheckAssembly(project, compile);
        }
    }
}