namespace CheckIt.Compilation.MsBuild
{
    using System.IO;

    using Microsoft.CodeAnalysis;

    public class MsBuildCompilationInfoFactory : ICompilationInfoFactory
    {
        public MsBuildCompilationInfoFactory()
        {
        }

        public ICompilationInfo GetCompilationInfo(FileInfo file)
        {
            var project = this.OpenProjectAsync(file.FullName);

            var compile = project.GetCompilationAsync().Result;

            var projectInfo = new MsBuildProjectCompilationInfo(project, compile);

            var info = new CompilationInfo(projectInfo);
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