namespace CheckIt.Tests.CheckAssembly
{
    using CheckIt.Compilation.Custom;

    public static class AssemblySetup
    {
        static AssemblySetup()
        {
            Check.SetBasePathSearch(@"..\..\..\");
            Locator.Bind<ICompilationInfoFactory, CustomCompilationInfoFactory>(new CustomCompilationInfoFactory());
        }

        public static void Initialize()
        {
            // Do static initialization
        }
    }
}