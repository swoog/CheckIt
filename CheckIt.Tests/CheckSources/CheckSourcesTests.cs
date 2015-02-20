namespace CheckIt.Tests.CheckSources
{
    using System.Linq;

    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckSourcesTests
    {
        public CheckSourcesTests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_check_source_code_contains_class_when_call_sources()
        {
            Check.Project(@"CheckIt.Tests.Data.csproj").Contains().Any().Class();
        }

        [Fact]
        public void Should_throw_error_when_no_sources_is_found()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Project("NotFoundCsProj.csproj").Contains().Any().Class();
                    });

            Assert.Equal("No project found that match 'NotFoundCsProj.csproj'.", ex.Message);
        }

        [Fact]
        public void Should_throw_error_when_source_code_not_contains_class_when_call_sources()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Project("CheckIt.Tests.Data.EmptyProject.csproj").Contains().Any().Class();
                });

            Assert.Equal("No class found.", ex.Message);
        }

        [Fact]
        public void Should_throw_error_when_source_code_not_contains_specific_class_when_call_sources()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Project("CheckIt.Tests.Data.csproj").Contains().Any().Class("NotFoundClass");
                    });

            Assert.Equal("No Class found that match pattern 'NotFoundClass'.", ex.Message);
        }
    }
}
