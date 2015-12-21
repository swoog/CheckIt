namespace CheckIt.Syntax
{
    using System.Collections.Generic;

    public interface IProjects : IEnumerable<IProject>, IPatternContains<IProjectMatcher, ICheckProjectContains>
    {
    }
}