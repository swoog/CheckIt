namespace CheckIt
{
    using System.Collections.Generic;

    public interface IAssemblies : IEnumerable<CheckAssembly>
    {
        CheckMatch Name();

        CheckInterfaces Interfaces(string empty);
    }
}