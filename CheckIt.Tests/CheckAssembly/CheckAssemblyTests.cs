namespace CheckIt.Tests.CheckAssembly
{
    using System.Linq;

    using Xunit;

    public class CheckAssemblyTests
    {
        public CheckAssemblyTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_check_assembly_information_when_match_one_assembly()
        {
            var assemblies = Check.Assembly("CheckIt.dll");

            Assert.Equal(1, assemblies.Count());
            Assert.Equal("CheckIt", assemblies.ElementAt(0).Name);
        }

        [Fact]
        public void Should_contains_class_when_check_specific_assembly()
        {
            Check.Assembly("CheckIt.dll").Contains().Any().Class("Check");
        }

        [Fact]
        public void Should_throw_error_when_check_specific_assembly_not_found()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt.dll").Contains().Any().Class("NotFoundClass");
                });

            Assert.Equal("No class found that match pattern 'NotFoundClass'.", e.Message);
        }
    }
}
