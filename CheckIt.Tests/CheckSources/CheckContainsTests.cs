namespace CheckIt.Tests.CheckSources
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckContainsTests
    {
        public CheckContainsTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_check_contains_one_class()
        {
            Check.File("Class1.cs").FromProject("CheckIt.tests.Data.csproj").Contains().One().Class("Class1");
        }

        [Fact]
        public void Should_throw_error_when_contains_more_than_one_class()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.File("MultipleClass.cs").FromProject("CheckIt.tests.Data.csproj").Contains().One().Class();
                });

            Assert.Equal("No class found that match pattern '*'.", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_contains_more_than_one_class_per_file()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Each().File("TwoClassInFile.cs").Contains().One().Class();
                });

            Assert.Equal("TwoClassInFile.cs contains two class.", e.Message);
        }
    }
}
