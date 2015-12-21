namespace CheckIt.Tests.Data
{
    using System;
    using System.Collections.Generic;

    public class Class2
    {
        public Class2()
        {
            var i = 0;
            Func<int> fakeLambdaCodeToTestClassName = () => i;

            var dicoOfFunc = new Dictionary<int, Func<string, int>> { { 1, int.Parse } };
            var testCall = dicoOfFunc[1]("1");
        }
    }
}
