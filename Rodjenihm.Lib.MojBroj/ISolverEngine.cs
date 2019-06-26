using System.Collections.Generic;

namespace Rodjenihm.Lib.MojBroj
{
    internal interface ISolverEngine
    {
        IEnumerable<Solution> Solve(IEnumerable<int> numbers, int target, IRpnMap rpnMap);
    }
}