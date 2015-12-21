namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public static class HaveMethods
    {
        public static IClassMatcher Have(this IEnumerable<IClass> classes)
        {
            return new CheckClasses(classes);
        }
    }
}
