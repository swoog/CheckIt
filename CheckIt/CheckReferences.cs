namespace CheckIt
{
    using System.Collections.Generic;
    using System.Linq;

    public class CheckReferences : CheckEnumerableBase<CheckReference>
    {
        private readonly IEnumerable<CheckReference> checkReferences;

        public CheckReferences(IEnumerable<CheckReference> checkReferences)
        {
            this.checkReferences = checkReferences;
        }

        public CheckReferences(ICompilationInfo compilationInfo, string pattern)
        {
            this.checkReferences =
                compilationInfo.Project.References.Where(r => FileUtil.FilenameMatchesPattern(r.Name, pattern))
                    .Select(r => new CheckReference(r.Name));
        }

        protected override IEnumerable<CheckReference> Gets()
        {
            return this.checkReferences;
        }
    }
}