namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Syntax;

    internal class AssemblyMatcher : IAssemblyMatcher
    {
        private readonly IEnumerable<IAssembly> checkAssemblies;

        public AssemblyMatcher(IEnumerable<IAssembly> checkAssemblies)
        {
            this.checkAssemblies = checkAssemblies;
        }

        public CheckMatch Name()
        {
            var values = this.checkAssemblies.Select(a => new CheckMatchValue(a.Name, a.Name, a.Position)).ToList();

            return new CheckMatch(values, "assembly");
        }

        public CheckMatch FileName()
        {
            var values = this.checkAssemblies.Select(a => new CheckMatchValue(a.Name, a.FileName, a.Position)).ToList();

            return new CheckMatch(values, "assembly");
        }
    }
}