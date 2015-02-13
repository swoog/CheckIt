namespace CheckIt
{
    using System.Collections.Generic;

    public interface IAssemblies : IEnumerable<CheckAssembly>, IPatternContains<IAssemblies, ICheckAssemblyContains>
	{
		CheckMatch Name();

	    CheckMatch FileName();
	}
}