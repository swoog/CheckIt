namespace CheckIt
{
    using System;
    using System.IO;
    using System.Linq;

    using CheckIt.Syntax;

    public static class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static IAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(GetProjects().Assembly(matchAssemblies), matchAssemblies);
        }

        public static IAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static IProjects Project(string projectfilePattern)
        {
            return GetProjects(projectfilePattern) as IProjects;
        }

        public static IProjects Project()
        {
            return GetProjects() as IProjects;
        }

        public static IFiles File(string pattern)
        {
            return new Files(pattern);
        }

        public static IFiles File()
        {
            return File(string.Empty);
        }

        public static ICheckClasses Class()
        {
            return Class("*");
        }

        public static ICheckClasses Class(string pattern)
        {
            return new CheckClasses(pattern);
        }

        public static ICheckInterfaces Interfaces()
        {
            return Interfaces(string.Empty);
        }

        public static ICheckInterfaces Interfaces(string pattern)
        {
            return new CheckInterfaces(pattern);
        }

        public static void SetBasePathSearch(string newBasePath)
        {
            basePath = Path.Combine(Environment.CurrentDirectory, newBasePath);
        }

        public static IMethods Method(string pattern)
        {
            return new CheckMethods(Check.GetProjects().Class("*").Method(pattern), pattern);
        }

        public static IMethods Method()
        {
            return Method("*");
        }

        internal static IObjectsFinder GetProjects(string projectfilePattern = "*.csproj")
        {
            return new CheckProjects(basePath, projectfilePattern);
        }
    }
}