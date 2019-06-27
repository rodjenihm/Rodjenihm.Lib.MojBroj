using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public sealed class MojBrojSolver
    {
        private readonly ISolverEngine solverEngine;
        private IRpnMap rpnMap = new RpnMap(6);

        public MojBrojSolver(ISolverEngine solverEngine)
        {
            this.solverEngine = solverEngine;
        }

        public IEnumerable<Solution> Solve(IEnumerable<int> numbers, int target)
        {
            if (numbers.Count() != rpnMap.Size + 1)
                rpnMap = new RpnMap(numbers.Count());

            return solverEngine.Solve(numbers, target, rpnMap);
        }
    }
}
