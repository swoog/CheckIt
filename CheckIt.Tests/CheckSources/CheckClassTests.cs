namespace CheckIt.Tests.CheckSources
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckClassTests
    {
        public CheckClassTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_contains_method_when_check_class()
        {
            Check.Class("Check").Contains().Any().Method("Method");
        }
    }
}
