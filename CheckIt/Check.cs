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
        public static CheckAssemblies Assembly(string matchAssemblies)
        {
            return new CheckAssemblies(matchAssemblies);
        }

        public static CheckAssemblies Assembly()
        {
            return Assembly("*.dll");
        }
    }
}