using Rodjenihm.Lib.Combinatorics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public class MojBrojSolver
    {
        private IRpnMap rpnMap = new RpnMap(6);
        private volatile bool solved = false;
        private int target;
        private readonly int[] opIds = new int[] { 0, 1, 2, 3 };

        public Solution Solution { get; private set; }

        public MojBrojSolver()
        {
        }

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

        private void SolveBranch(int[] numbers, int nIdx, int[] pattern, int pIdx, int result, int[] stNumbers, int stnIdx, int[] stOperators, int stoIdx)
        {
            if (solved)
                return;

            if (pIdx == pattern.Length)
            {
                if (result == target)
                {
                    solved = true;
                    Solution = new Solution(target, numbers, stOperators, pattern);
                }

                return;
            }

            while (pattern[pIdx] == 1)
            {
                stNumbers[stnIdx++] = numbers[nIdx++];
                pIdx++;
            }

            int right = stNumbers[stnIdx - 1];
            int left = stNumbers[stnIdx - 2];

            foreach (var opId in opIds)
            {
                if (opId == 0 && (left == 1 || right == 1))
                    continue;

                if (opId == 2 && left <= right)
                    continue;

                if (opId == 3 && right == 0)
                    continue;

                if (opId == 3 && left % right != 0)
                    continue;

                if (opId == 3 && right == 1)
                    continue;

                result = Calc(left, right, opId);
                stNumbers[stnIdx-- - 2] = result;
                stOperators[stoIdx++] = opId;

                SolveBranch(numbers, nIdx, pattern, pIdx + 1, result, stNumbers, stnIdx, stOperators, stoIdx);

                stnIdx++;
                stNumbers[stnIdx - 1] = right;
                stNumbers[stnIdx - 2] = left;
                stoIdx--;
            }

        }
        public void Solve(IEnumerable<int> numbers, int target)
        {
            solved = false;
            this.target = target;

            if (numbers.Count() != rpnMap.Size + 1)
                rpnMap = new RpnMap(numbers.Count());

            for (int i = 2; i <= numbers.Count() && !solved; i++)
            {
                var combinations = new Combinations<int>(numbers, i);

                foreach (var combination in combinations)
                {
                    var variations = new Permutations<int>(combination);

                    foreach (var variation in variations)
                    {
                        foreach (var pattern in rpnMap[i])
                        {
                            SolveBranch(variation.ToArray(), 0, pattern, 0, 0, new int[i], 0, new int[i - 1], 0);
                        }
                    }
                }
            }

            if (!solved)
            {
                Solution = new Solution();
                Solution.Postfix = Solution.Infix = "Couln't find solution!";
            }
        }
    }
}
