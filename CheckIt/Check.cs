namespace CheckIt
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text.RegularExpressions;

    public class Check
    {
        public static CheckAssembly Assembly(string matchAssemblies)
        {
            var assemblies = new List<Assembly>();

            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, matchAssemblies))
            {
                assemblies.Add(System.Reflection.Assembly.LoadFile(file));
            }

            if (assemblies.Count == 0)
            {
                throw new MatchException(string.Format("No assembly found that match '{0}'", matchAssemblies));
            }

            return new CheckAssembly(assemblies);
        }

        public static CheckAssembly Assembly()
        {
            return Assembly("*.dll");
        }
    }
}