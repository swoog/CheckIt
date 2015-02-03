namespace CheckIt.Tests.Data
{
    using System;

    public class Class2
    {
        public Class2()
        {
            var i = 0;
            Func<int> fakeLambdaCodeToTestClassName = () => i;
        }
    }
}
