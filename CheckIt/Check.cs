namespace CheckIt
{
    using System;
    using System.IO;

    using Microsoft.CodeAnalysis;

    public class Check
    {
        internal static string basePath = Environment.CurrentDirectory;

        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return new CheckProjects(basePath, "*.csproj").Assembly(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static IProjects Project(string projectfilePattern)
        {
            return new CheckProjects(basePath, projectfilePattern);
        }

        private static IProjects Project()
        {
            return Project("*.csproj");
        }

        public static void SetBasePathSearch(string newBasePath)
        {
            basePath = Path.Combine(Environment.CurrentDirectory, newBasePath);
        }

        public static CheckFiles File(string pattern)
        {
            return new CheckFiles(pattern);
        }
    }
}