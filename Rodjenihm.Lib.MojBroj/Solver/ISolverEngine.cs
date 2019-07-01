using System.Collections.Generic;

namespace Rodjenihm.Lib.MojBroj.Solver
{
    public interface ISolverEngine
    {
        IEnumerable<Solution> Solve(IEnumerable<int> numbers, int target, IRpnMap rpnMap);
    }
}