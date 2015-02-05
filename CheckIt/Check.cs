namespace CheckIt
{
    using System;
    using System.IO;

    public class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return Project("*.csproj").Assembly(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static CheckProjects Project(string projectfilePattern)
        {
            return new CheckProjects(basePath, projectfilePattern);
        }

        public static void SetBasePathSearch(string newBasePath)
        {
            basePath = Path.Combine(Environment.CurrentDirectory, newBasePath);
        }
    }
}