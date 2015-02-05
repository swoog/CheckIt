namespace CheckIt
{
    using System;
    using System.IO;

    public class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return Sources("*.csproj").Assembly(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static CheckSources Sources(string projectfilePattern)
        {
            return new CheckSources(basePath, projectfilePattern);
        }

        public static void SetBasePathSearch(string newBasePath)
        {
            basePath = Path.Combine(Environment.CurrentDirectory, newBasePath);
        }
    }
}