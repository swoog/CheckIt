namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using CheckIt.ObjectsFinder;
    using CheckIt.Syntax;

    public static class Check
    {
        private static string basePath = Environment.CurrentDirectory;

        public static IAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(GetProjects().Assembly(matchAssemblies).ToList<IAssembly>(), matchAssemblies);
        }

        public static IAssemblies Assembly()
        {
            return Assembly("*.dll");
        }

        public static IProjects Project(string projectfilePattern)
        {
            return new CheckProjects(GetFiles(basePath, projectfilePattern), projectfilePattern);
        }

        public static IProjects Project()
        {
            return Project("*.csproj");
        }

        public static IFiles File(string pattern)
        {
            return new Files(pattern);
        }

        public static IFiles File()
        {
            return File("*");
        }

        public static ICheckClasses Class()
        {
            return Class("*");
        }

        public static ICheckClasses Class(string pattern)
        {
            return new CheckClasses(GetProjects(), pattern);
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
            return new CheckMethods(Check.GetProjects().Class("*").Method(pattern).ToList<IMethod>(), pattern);
        }

        public static IMethods Method()
        {
            return Method("*");
        }

        internal static IObjectsFinder GetProjects()
        {
            return new ProjectsObjectsFinder(Project());
        }

        internal static IObjectsFinder GetProjects(string projectfilePattern)
        {
            return new ProjectsObjectsFinder(Project(projectfilePattern));
        }

        private static IEnumerable<FileInfo> GetFiles(string path, string pattern)
        {
            foreach (var file in Directory.GetFiles(path, pattern))
            {
                yield return new FileInfo(file);
            }

            foreach (var directory in Directory.GetDirectories(path))
            {
                foreach (var fileInfo in GetFiles(directory, pattern))
                {
                    yield return fileInfo;
                }
            }
        }

	    public static Extend Extend()
	    {
		    return new Extend();
	    }
    }
}