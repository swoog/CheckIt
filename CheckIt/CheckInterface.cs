namespace CheckIt
{
    using System.Collections.Generic;

    using CheckIt.Syntax;

    public class CheckInterface : CheckType, IInterface
    {
        public CheckInterface(string name, string nameSpace)
            : base(name, nameSpace)
        {
        }

        public override IEnumerable<IMethod> Method(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}