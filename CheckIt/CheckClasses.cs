namespace CheckIt
{
    using System.Collections.Generic;

    using Microsoft.CodeAnalysis;

    public class CheckClasses : CheckTypes<CheckClass>
    {
        public CheckClasses(IEnumerable<CheckClass> classes)
            :base(classes, "class")
        {
            this.classes = classes;
        }

        public CheckClasses(Project project, Compilation compile, string classPattern)
            :base(project, compile, classPattern, "class")
        {
        }
    }
}