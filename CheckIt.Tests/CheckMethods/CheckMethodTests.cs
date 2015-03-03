using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckIt.Tests.CheckMethods
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckMethodTests
    {
        public CheckMethodTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_check_method_name()
        {
            Check.Method().FromAssembly("CheckIt.dll").Have().Name().Match("^[A-Z]");
        }

        [Fact]
        public void Should_throw_an_error_when_method_name_not_match()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Method("Method").FromAssembly("CheckIt.dll").Have().Name().Match("Class");
                    });

            Assert.Equal("", e.Message);
        }
    }
}
