namespace CheckIt.Tests.CheckSources
{
    using System.Linq;

    using Xunit;

    public class CheckSourcesTests
    {
        public CheckSourcesTests()
        {
            Check.SetBasePathSearch(@"..\..\..\CheckIt.Tests.Data\");
        }

        [Fact]
        public void Should_check_source_code_contains_class_when_call_sources()
        {
            Check.Class().FromProject(@"CheckIt.Tests.Data.csproj").Have().HasAny();
        }

        [Fact]
        public void Should_throw_error_when_no_sources_is_found()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Class().FromProject("NotFoundCsProj.csproj").Have().HasAny();
                    });

            Assert.Equal("No project found that match 'NotFoundCsProj.csproj'", ex.Message);
        }

        [Fact]
        public void Should_throw_error_when_source_code_not_contains_class_when_call_sources()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Class("NotFoundClass").FromProject("CheckIt.Tests.Data.csproj").Have().HasAny();
                    });

            Assert.Equal("No class found", ex.Message);
        }
    }
}
