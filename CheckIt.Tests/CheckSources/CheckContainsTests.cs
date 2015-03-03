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
            Check.File("Class1.cs").FromAssembly("CheckIt.tests.Data.dll").Contains().One().Class("Class1");
        }

        [Fact]
        public void Should_throw_error_when_contains_more_than_one_class()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.File("MultipleClass.cs").FromAssembly("CheckIt.tests.Data.dll").Contains().One().Class();
                });

            Assert.Equal("No class found that match pattern ''.", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_contains_more_than_one_class_per_file()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Each().File("MultipleClass.cs").Contains().One().Class();
                });

            Assert.Equal("No class found that match ''.", e.Message);
        }
    }
}
