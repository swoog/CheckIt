namespace CheckIt.Tests.CheckSources
{
    using System.Linq;

    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckProjectsTests
    {
        public CheckProjectsTests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_all_project_end_with_csproj()
        {
            Check.Project().Have().Name().Match(".csproj$");
        }

        [Fact]
        public void Should_check_project_end_with_csproj()
        {
            Check.Project(@"*.csproj").Have().Name().Match(".csproj$");
        }

        [Fact]
        public void Should_throw_error_when_project_equal_to_CheckIt()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Project(@"*.csproj").Have().Name().Not().EqualTo("CheckIt.csproj");
                });

            Assert.Equal("The folowing projects match 'CheckIt.csproj' :\nCheckIt.csproj", ex.Message);
        }


        [Fact]
        public void Should_throw_error_when_project_are_not_in_correct_format()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Project(@"Check.cs").Have().Name().EqualTo("CheckIt.csproj");
                });

            Assert.Equal("The folowing project are not in correct xml format \"Check.cs\"", ex.Message);
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

            Assert.Equal("No class found that match pattern 'NotFoundClass'.", ex.Message);
        }

        [Fact]
        public void Should_throw_error_when_assembly_project()
        {
            var ex = Assert.Throws<MatchException>(
               () =>
               {
                   Check.Project("CheckIt.csproj").Have().AssemblyName().Match("CheckIt.dll");
               });

            Assert.Equal("The folowing assemblies doesn't respect pattern 'CheckIt.dll' :\nCheckIt", ex.Message);
        }
    }
}
