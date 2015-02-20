namespace CheckIt
{
    using System;
    using System.IO;

    using CheckIt.Syntax;

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

        public static IProjects Project()
        {
            return GetProjects();
        }

        public static IFiles File(string pattern)
        {
            return new Files(pattern);
        }

        public static IFiles File()
        {
            return File(string.Empty);
        }

        public static IClasses Class()
        {
            return Class(string.Empty);
        }

        public static IClasses Class(string pattern)
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

        internal static CheckProjects GetProjects(string projectfilePattern = "*.csproj")
        {
            return new CheckProjects(basePath, projectfilePattern);
        }
    }
}