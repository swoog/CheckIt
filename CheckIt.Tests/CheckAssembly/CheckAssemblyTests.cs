﻿namespace CheckIt.Tests.CheckAssembly
{
    using System.Linq;

    using Xunit;

    public class CheckAssemblyTests
    {
        public CheckAssemblyTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_check_assembly_information_when_match_one_assembly()
        {
            var assemblies = Check.Assembly("CheckIt.dll");

            Assert.Equal(1, assemblies.Count());
            Assert.Equal("CheckIt", assemblies.ElementAt(0).Name);
        }
    }
}
