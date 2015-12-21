namespace CheckIt.Tests.CheckSources
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckInterfaceTests
    {
        public CheckInterfaceTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_contains_method_when_check_interface()
        {
            Check.Interfaces("IAssembly").Contains().Any().Method("Method");
        }

        [Fact]
        public void Should_throw_error_when_no_method_found()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.Interfaces("IAssembly").Contains().Any().Method("NoFoundMethod");
                    });

            Assert.Equal("No method found that match pattern 'NoFoundMethod'.", ex.Message);
        }
    }
}
