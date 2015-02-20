namespace CheckIt.Tests.CheckReferences
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckProjectReferenceAssemblyTests
    {
        public CheckProjectReferenceAssemblyTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_project_reference_When_check_code()
        {
            Check.Project("CheckIt.csproj").Contains().Any().Reference("System");
        }

        [Fact]
        public void Should_throw_error_When_project_not_contains_reference()
        {
            var e = Assert.Throws<MatchException>(
                () => Check.Project("CheckIt.csproj").Contains().Any().Reference("NotFoundReference"));
            Assert.Equal("No reference found that match pattern 'NotFoundReference'.", e.Message);
        }

        [Fact]
        public void Should_throw_error_When_project_contains_reference()
        {
            var e = Assert.Throws<MatchException>(
                () => Check.Project("CheckIt.csproj").Contains().No().Reference("System"));
            Assert.Equal("Reference found that match pattern 'System'.", e.Message);
        }
    }
}
