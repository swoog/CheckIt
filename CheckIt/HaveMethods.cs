namespace CheckIt
{
    using System;
    using System.Collections.Generic;

    public static class HaveMethods
    {
        public static CheckClasses Have(this IEnumerable<CheckClass> classes)
        {
            return new CheckClasses(classes);
        }
    }
}
