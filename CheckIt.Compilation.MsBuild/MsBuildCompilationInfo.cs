namespace CheckIt.Compilation.MsBuild
{
    using System.IO;

    using Microsoft.CodeAnalysis;

    public class MsBuildCompilationInfo : ICompilationInfo
    {
        public MsBuildCompilationInfo()
        {
        }

        public CompilationInfo GetCompilationInfo(FileInfo file)
        {
            var project = this.OpenProjectAsync(file.FullName);

            var compile = project.GetCompilationAsync().Result;

            var info = new CompilationInfo { Project = project, Compile = compile };
            return info;
        }

        private Project OpenProjectAsync(string fileName)
        {
#if DEBUG
            var msBuildWorkspace = MSBuildWorkspace.Create();
            var t = msBuildWorkspace.OpenProjectAsync(fileName);

            t.Wait();

            return t.Result;
#else
            return null;
#endif
        }
    }
}