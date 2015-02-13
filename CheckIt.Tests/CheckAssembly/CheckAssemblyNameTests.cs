namespace CheckIt.Tests.CheckAssembly
{
    using CheckIt.Compilation.Custom;

    using Xunit;

    public class CheckAssemblyNameTests
    {
        public CheckAssemblyNameTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_check_assembly_name()
        {
            Check.Assembly("CheckIt.dll").Name().Match("^CheckIt$");
        }

        [Fact]
        public void Should_check_assembly_when_wildcare()
        {
            Check.Assembly("CheckIt.*.dll").Name().Match("^CheckIt");
        }

        [Fact]
        public void Should_check_not_match_assembly_name()
        {
            Check.Assembly("CheckIt.*.dll").Name().NotMatch("^Toto$");
        }

        [Fact]
        public void Should_throw_error_when_assembly_name_no_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("CheckIt*.dll").Name().Match("^Toto");
                });

            Assert.Equal("The folowing assembly doesn't respect pattern '^Toto' :\nCheckIt\nCheckIt.Tests\nCheckIt.Tests.Data", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_check_not_match_and_assembly_name_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("*.dll").Name().NotMatch("^CheckIt");
                });

            Assert.Equal("The folowing assembly match pattern '^CheckIt' :\nCheckIt\nCheckIt.Tests\nCheckIt.Tests.Data", e.Message);
        }
    }
}
