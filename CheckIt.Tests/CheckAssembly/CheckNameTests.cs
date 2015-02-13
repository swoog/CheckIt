namespace CheckIt.Tests.CheckAssembly
{
    using System.Linq;

    using Xunit;

    public class CheckNameTests
    {
        public CheckNameTests()
        {
            Check.SetBasePathSearch(@"..\..\..\");
        }

        [Fact]
        public void Should_check_name_of_all_class()
        {
            Check.Class().FromAssembly("CheckIt.Tests.Data.dll").Have().Name().Match("^[A-Z].+$");
        }

        [Fact]
        public void Should_check_name_of_class()
        {
            Check.Class("Class1").FromAssembly("CheckIt.Tests.Data.dll").Have().Name().Match("^[A-Z].+$");
        }

        [Fact]
        public void Should_check_name_of_class_when_use_linq()
        {
            Check.Class().Where(c => c.Name == "Class1").Have().Name().Match("^Class1$");
        }

        [Fact]
        public void Should_check_not_match_name()
        {
            Check.Class("Class1").FromAssembly("CheckIt.Tests.Data.dll").Have().Name().NotMatch("^Toto$");
        }

        [Fact]
        public void Should_throw_error_when_name_no_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class("Class1").FromAssembly("CheckIt.Tests.Data.dll").Have().Name().Match("^[a-z].+$");
                });

            Assert.Equal("The folowing class doesn't respect pattern '^[a-z].+$' :\nClass1", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_check_not_match_and_name_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class("Class1").FromAssembly("CheckIt.Tests.Data.dll").Have().Name().NotMatch("[0-9]$");
                });

            Assert.Equal("The folowing class match pattern '[0-9]$' :\nClass1", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_no_assembly_matche()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Assembly("Toto").Count();
                });

            Assert.Equal("No assembly found that match 'Toto'", e.Message);
        }
    }
}
