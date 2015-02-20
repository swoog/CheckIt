namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public static class HaveMethods
    {
        public static CheckClasses Have(this IEnumerable<IClass> classes)
        {
            return new CheckClasses(classes);
        }
    }
}
