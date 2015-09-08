namespace CheckIt.Tests.CheckSources
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckClassNameTests
    {
        public CheckClassNameTests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\CheckIt.Tests.Data\");
        }

        [Fact]
        public void Should_check_class_name_start_upper_case_when_call_sources()
        {
            Check.Class().FromProject(@"CheckIt.Tests.Data.csproj").Have().Name().Match("^[A-Z]");
        }

        [Fact]
        public void Should_throw_error_when_class_name_not_match()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class("Class1").FromProject("CheckIt.Tests.Data.csproj").Have().Name().Match("^[a-z]");
                });

            Assert.Equal("The folowing class doesn't respect pattern '^[a-z]' :\nClass1 on line 8 from file Class1.cs", ex.Message);
        }

        [Fact]
        public void Should_throw_error_when_class_name_not_match2()
        {
            Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class().FromProject("CheckIt.Tests.Data.csproj").Have().Name().Match("^[a-z]");
                });
        }

        [Fact]
        public void Should_throw_error_when_check_class_and_use_from_file()
        {
            var e = Assert.Throws<MatchException>(
                 () =>
                 {
                     Check.Class().FromFile("Class1.cs").Have().Name().Match("Class2");
                 });

            Assert.Equal("The folowing class doesn't respect pattern 'Class2' :\nClass1 on line 8 from file Class1.cs", e.Message);
        }
    }
}
