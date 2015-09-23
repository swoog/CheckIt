namespace CheckIt.Tests.CheckSources
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckClassNameTests
    {
        public CheckClassNameTests()
        {
            AssemblySetup.Initialize();
            Check.SetBasePathSearch(@"..\..\..\CheckIt.Tests.Data\");
        }

        [Fact]
        public void Should_check_class_name_start_upper_case_when_call_sources()
        {
            Check.Class().FromProject(@"CheckIt.Tests.Data.csproj").Have().Name().Match("^[A-Z]");
        }

        [Fact]
        public void Should_throw_error_when_class_name_not_match()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class("Class1").FromProject("CheckIt.Tests.Data.csproj").Have().Name().Match("^[a-z]");
                });

            Assert.Equal("The folowing class doesn't respect pattern '^[a-z]' :\nClass1 on line 8 from file Class1.cs", ex.Message);
        }

        [Fact]
        public void Should_throw_error_when_class_name_not_match2()
        {
            Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class().FromProject("CheckIt.Tests.Data.csproj").Have().Name().Match("^[a-z]");
                });
        }

        [Fact]
        public void Should_throw_error_when_check_class_and_use_from_file()
        {
            var e = Assert.Throws<MatchException>(
                 () =>
                 {
                     Check.Class().FromFile("Class1.cs").Have().Name().Match("Class2");
                 });

            Assert.Equal("The folowing class doesn't respect pattern 'Class2' :\nClass1 on line 8 from file Class1.cs", e.Message);
        }

        [Fact]
        public void Should_check_class_and_use_not_from_file()
        {
            Check.Class().Not().FromFile("Class1.cs").Have().Name().Not().Match("Class1");
        }

        [Fact]
        public void Should_throw_error_check_class_and_use_not_from_file()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class().Not().FromFile("Class1.cs").Have().Name().Not().Match("Class2");
                });

            Assert.Equal("The folowing class match pattern 'Class2' :\nClass2 on line 4 from file Class2.cs", e.Message);
        }

        [Fact]
        public void Should_throw_error_when_not_found_class()
        {
            var ex = Assert.Throws<MatchException>(
                () =>
                {
                    Check.Class("UnknowClass").Have().Name().Match("^UnknowClass$");
                });

            Assert.Equal("No class found that match 'UnknowClass'.", ex.Message);
        }


        [Fact]
        public void Should_throw_error_when_not_found_class_when_use_from_project()
        {
            var ex = Assert.Throws<MatchException>(
             () =>
             {
                 Check.Class("Class1").FromProject(@"CheckIt.csproj").Have().Name().Match("^Class1$");
             });

            Assert.Equal("No class found that match 'Class1'.", ex.Message);
        }

        [Fact]
        public void Should_check_class_when_match_two_class()
        {
            Check.Class("Class?").Have().Name().Match("^Class.$");
        }
    }
}
