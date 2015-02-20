﻿namespace CheckIt.Tests.CheckSources
{
    using CheckIt.Tests.CheckAssembly;

    using Xunit;

    public class CheckFileTests
    {
        public CheckFileTests()
        {
            AssemblySetup.Initialize();
        }

        [Fact]
        public void Should_contains_class_when_check_source()
        {
            Check.File("Check.cs").FromProject("*.csproj").Contains().Any().Class("Check");
        }

        [Fact]
        public void Should_contains_one_file()
        {
            Check.File("Check.cs").FromProject("CheckIt.csproj").Contains().One().Class();
        }

		[Fact]
        public void Should_contains_class_when_check_specific_file()
        {
            Check.File("Check.cs").FromProject("CheckIt.Tests.Data.csproj").Contains().Any().Class("ClassHaveDifferentNameFromFile");
        }

        [Fact]
        public void Should_throw_error_when_check_specific_file_not_found()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.File("Check.cs").FromProject("CheckIt.csproj").Contains().Any().Class("ClassHaveDifferentNameFromFile");
                    });

            Assert.Equal("No class found that match pattern 'ClassHaveDifferentNameFromFile'.", e.Message);
        }

        [Fact]
        public void Should_contains_class_when_no_project_specified()
        {
            Check.File("Check.cs").Contains().Any().Class("Check");
        }

        [Fact]
        public void Should_throw_error_when_file_contains_no_class()
        {
            var e = Assert.Throws<MatchException>(
                () =>
                    {
                        Check.File("Check.cs").FromProject("*.csproj").Contains().Any().Class("NotFoundClass");
                    });

            Assert.Equal("No class found that match pattern 'NotFoundClass'.", e.Message);
        }
    }
}
