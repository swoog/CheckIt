namespace CheckIt.Tests.CheckAssembly
{
    using CheckIt.Compilation;
    using CheckIt.Compilation.Custom;

    public static class AssemblySetup
    {
        static AssemblySetup()
        {
            Locator.Bind<ICompilationInfoFactory, CustomCompilationInfoFactory>(new CustomCompilationInfoFactory());
        }

        public static void Initialize()
        {
            // Do static initialization
            Check.SetBasePathSearch(@"..\..\..\");
        }
    }
}