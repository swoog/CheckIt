namespace CheckIt.Tests.CheckSources
{
    using Xunit;

    public class CheckFileTests
    {
        [Fact]
        public void Should_contains_class_when_check_source()
        {
            Check.Project("*.csproj").File("Check.cs").Contains().Class("Check");
        }
    }
}
