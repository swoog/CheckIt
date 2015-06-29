using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckIt.Tests.CheckMethods
{
    using System.Collections;

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

            Assert.Equal("The folowing method doesn't respect pattern 'type' :\nMethod on line 73 from file Check.cs\nMethod on line 75 from file Check.cs\nMethod on line 78 from file Check.cs\nMethod on line 80 from file Check.cs", e.Message);
        }

        [Fact]
        public void Should_test_generic_ype_from_calling_method()
        {
            Check.Method("CalledMethod").Have().GenericType().Not().EqualTo("T");
        }

        [Fact]
        public void Should_throw_error_when_test_wrong_generic_ype_from_calling_method()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Method("CalledMethod").Have().GenericType().Not().Match("int");
                });

            Assert.Equal("The folowing generic type match 'int' :\nT on line 9 from file ClassCalled.cs", e.Message);
        }
    }
}
