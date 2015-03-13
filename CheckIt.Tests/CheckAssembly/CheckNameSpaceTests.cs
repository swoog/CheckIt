namespace CheckIt.Tests.CheckAssembly
{
    using Xunit;

    public class CheckNameSpaceTests
    {
        public CheckNameSpaceTests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\CheckIt.Tests.Data\");
        }

        [Fact]
        public void Should_check_namespace_of_class()
        {
            Check.Class("Class1").FromAssembly("CheckIt.Tests.Data.dll").Have().NameSpace().Match("CheckIt.Tests.Data");
        }

        [Fact]
        public void Should_throw_error_when_name_no_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class("Class1").FromAssembly("CheckIt.Tests.Data.dll").Have().NameSpace().Match("Toto");
                });

            Assert.Equal("The folowing class doesn't respect pattern 'Toto' :\nClass1 on line 8 from file Class1.cs", e.Message);
        }

        [Fact]
        public void Should_check_namespace_of_interface()
        {
            Check.Interfaces("ErrorInterface").FromAssembly("CheckIt.Tests.Data.dll").Have().NameSpace().Match("^CheckIt.Tests.Data$");
        }
    }
}
