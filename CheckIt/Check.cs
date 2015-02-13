namespace CheckIt
{
    using System;
    using System.IO;

    public class Check
    {
        internal static string basePath = Environment.CurrentDirectory;

        public static IAssemblies Assembly(string matchAssemblies)
        {
            return GetProjects().Assembly(matchAssemblies);
        }

        public static IAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static IProjects Project(string projectfilePattern)
        {
            return GetProjects(projectfilePattern);
        }

        internal static CheckProjects GetProjects(string projectfilePattern = "*.csproj")
        {
            return new CheckProjects(basePath, projectfilePattern);
        }

        private static IProjects Project()
        {
            return GetProjects();
        }

        public static CheckFiles File(string pattern)
        {
            return new CheckFiles(pattern);
        }

        public static CheckFiles File()
        {
            return File(string.Empty);
        }

        public static CheckClasses Class()
        {
            return Class(string.Empty);
        }

        public static CheckClasses Class(string pattern)
        {
            return new CheckClasses(pattern);
        }

        public static CheckInterfaces Interfaces()
        {
            return Interfaces(string.Empty);
        }

        public static CheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(pattern);
        }

        public static void SetBasePathSearch(string newBasePath)
        {
            basePath = Path.Combine(Environment.CurrentDirectory, newBasePath);
        }

        public static CheckSources Source(string p)
        {
            throw new NotImplementedException();
        }
    }
}