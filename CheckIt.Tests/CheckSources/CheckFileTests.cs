namespace CheckIt.Tests.CheckSources
{
    using Xunit;

    public class CheckFileTests
    {
        public CheckFileTests()
        {
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_contains_class_when_check_source()
        {
            Check.Project("*.csproj").File("Check.cs").Contains().Class("Check");
        }

        [Fact]
        public void Should_contains_class_when_no_project_specified()
        {
            Check.File("Check.cs").Contains().Class("Check");
        }

        [Fact]
        public void Should_throw_error_when_file_contains_no_class()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Project("*.csproj").File("Check.cs").Contains().Class("NotFoundClass");
                    });

            Assert.Equal("No class found that match 'NotFoundClass'.", e.Message);
        }
    }
}
