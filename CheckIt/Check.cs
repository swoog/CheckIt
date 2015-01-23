namespace CheckIt
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Text.RegularExpressions;

    public class Check
    {
        public static CheckAssembly Assembly(string matchAssemblies)
        {
            foreach (var file in Directory.GetFiles(Environment.CurrentDirectory, "*.dll"))
            {
                System.Reflection.Assembly.LoadFile(file);
            }

            var assemblies = AppDomain.CurrentDomain.GetAssemblies().Where(c => Regex.Match(c.FullName, matchAssemblies).Success).ToList();

            if (assemblies.Count == 0)
            {
                throw new MatchException(string.Format("No assembly found that match '{0}'", matchAssemblies));
            }

            return new CheckAssembly(assemblies);
        }
    }
}