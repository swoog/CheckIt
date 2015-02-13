namespace CheckIt.Tests.CheckAssembly
{
    using CheckIt.Compilation.Custom;

    public static class AssemblySetup
    {
        static AssemblySetup()
        {
            Check.SetBasePathSearch(@"..\..\..\");
            Locator.Bind<ICompilationInfo, CustomCompilationInfo>(new CustomCompilationInfo());
        }

        public static void Initialize()
        {
            // Do static initialization
        }
    }
}