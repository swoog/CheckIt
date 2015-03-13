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
                        Check.Method("Method").FromClass("Check").Have().Name().Match("type");
                    });

            Assert.Equal("The folowing method doesn't respect pattern 'type' :\nMethod on line 72 from file Check.cs\nMethod on line 77 from file Check.cs", e.Message);
        }
    }
}
