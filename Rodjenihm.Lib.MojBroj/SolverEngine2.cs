using Rodjenihm.Lib.Combinatorics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public sealed class SolverEngine2 : ISolverEngine
    {
        private readonly int[] opIds = new int[] { 0, 1, 2, 3 };
        private int[] variation;
        private int[] stNumbers;
        private int[] pattern;
        private int target;

        private int Calc(int x, int y, int opId)
        {
            switch (opId)
            {
                case 0:
                    return x * y;
                case 1:
                    return x + y;
                case 2:
                    return x - y;
                case 3:
                    return x / y;
                default:
                    break;
            }

            throw new InvalidOperationException(nameof(opId));
        }

        private IEnumerable<Solution> SolveBranch(int nIdx, int pIdx, int result, int stnIdx, int[] stOperators, int stoIdx)
        {
            int pushed = 0;
            while (pattern[pIdx] == 1)
            {
                stNumbers[stnIdx++] = variation[nIdx++];
                pushed++;
                pIdx++;
            }
            bool simple = pushed > 1;

            int right = stNumbers[stnIdx - 1];
            int left = stNumbers[stnIdx - 2];

            foreach (var opId in opIds)
            {
                if (opId == 0 && (left <= 1 || right <= 1 || simple && left > right))
                    continue;

                if (opId == 1 && simple && left > right)
                    continue;

                if (opId == 3 && (right <= 1 || left % right != 0))
                    continue;

                result = Calc(left, right, opId);

                if (result <= 0)
                    continue;

                if (pIdx == pattern.Length - 1)
                {
                    stOperators[stoIdx] = opId;

                    if (result == target)
                    {
                        yield return new Solution(target, variation, stOperators, pattern);
                        break;
                    }

                    if (opId == 0 && result < target)
                        break;
                }
                else
                {
                    stNumbers[stnIdx-- - 2] = result;
                    stOperators[stoIdx++] = opId;

                    foreach (var solution in SolveBranch(nIdx, pIdx + 1, result, stnIdx, stOperators, stoIdx))
                        yield return solution;

                    stnIdx++;
                    stNumbers[stnIdx - 1] = right;
                    stNumbers[stnIdx - 2] = left;
                    stoIdx--;
                }
            }
        }

        public IEnumerable<Solution> Solve(IEnumerable<int> numbers, int target, IRpnMap rpnMap)
        {
            this.target = target;

            for (int i = 2; i <= numbers.Count(); i++)
            {
                var combinations = new Combinations<int>(numbers, i);
                stNumbers = new int[i];

                foreach (var combination in combinations)
                {
                    var variations = new Permutations<int>(combination);

                    foreach (var variation in variations)
                    {
                        this.variation = variation.ToArray();
                        foreach (var pattern in rpnMap[i])
                        {
                            this.pattern = pattern;
                            foreach (var branch in SolveBranch(0, 0, 0, 0, new int[i - 1], 0))
                                yield return branch;
                        }
                    }
                }
            }
        }
    }
}
