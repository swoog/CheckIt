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

            Assert.Equal("The folowing method doesn't respect pattern 'type' :\nMethod on line 67 from file Check.cs\nMethod on line 69 from file Check.cs\nMethod on line 72 from file Check.cs\nMethod on line 74 from file Check.cs", e.Message);
        }

        [Fact]
        public void Should_test_generic_ype_from_calling_method()
        {
            Check.Method("CalledMethod").FromClass("ClassCaller").Have().GenericType().Not().EqualTo("T");
        }

        [Fact]
        public void Should_throw_error_when_test_wrong_generic_type_from_calling_method()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Method("CalledMethod").FromClass("ClassCaller").Have().GenericType().Not().Match("int");
                });

            Assert.Equal("The folowing generic type match pattern 'int' :\nint on line 12 from file ClassCaller.cs", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_test_wrong_generic_type_from_called_method()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Method("CalledMethod").FromClass("ClassCalled").Have().GenericType().Not().Match("T");
                });

            Assert.Equal("The folowing generic type match pattern 'T' :\nT on line 9 from file ClassCalled.cs", e.Message);
        }
    }
}
