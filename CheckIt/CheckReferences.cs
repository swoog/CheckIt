namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    using CheckIt.Compilation;
    using CheckIt.Syntax;

    internal class CheckReferences : CheckEnumerableBase<IReference>
    {
        private readonly IEnumerable<IReference> checkReferences;

        public CheckReferences(IEnumerable<IReference> checkReferences)
        {
            this.checkReferences = checkReferences;
        }

        public CheckReferences(ICompilationInfo compilationInfo, string pattern)
        {
            this.checkReferences =
                compilationInfo.Project.References.Where(r => FileUtil.FilenameMatchesPattern(r.Name, pattern))
                    .Select(r => new CheckReference(r.Name));
        }

        protected override IEnumerable<IReference> Gets()
        {
            return this.checkReferences;
        }
    }
}