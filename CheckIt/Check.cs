namespace CheckIt
{
    using System;
    using System.IO;

    public class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return Project().Assembly(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static CheckProjects Project(string projectfilePattern)
        {
            return new CheckProjects(basePath, projectfilePattern);
        }

        private static CheckProjects Project()
        {
            return Project("*.csproj");
        }

        public static void SetBasePathSearch(string newBasePath)
        {
            basePath = Path.Combine(Environment.CurrentDirectory, newBasePath);
        }
    }
}