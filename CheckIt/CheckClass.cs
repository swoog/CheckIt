namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public class CheckClass : CheckType, IClass
    {
        private readonly ICompilationInfo compilationInfo;

        public CheckClass(string name, string nameSpace, ICompilationInfo compilationInfo, Position position)
            : base(name, nameSpace, position)
        {
            this.compilationInfo = compilationInfo;
        }

        public override IEnumerable<IMethod> Method(string name)
        {
            foreach (var method in this.compilationInfo.Get<IMethod>())
            {
                if (FileUtil.FilenameMatchesPattern(method.Name, name))
                {
                    if (method.Type == this)
                    {
                        yield return method;
                    }
                }
            }
        }
    }
}