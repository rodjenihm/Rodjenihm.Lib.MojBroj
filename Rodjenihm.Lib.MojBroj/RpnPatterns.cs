using Rodjenihm.Lib.Combinatorics;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    internal sealed class RpnPatterns : IRpnPatterns
    {
        private bool IsValidRpnPattern(int[] pattern)
        {
            int stackCount = 0;

            foreach (var token in pattern)
            {
                if (token == 1)
                    stackCount++;
                else if (token == 0)
                {
                    if (stackCount < 2)
                        return false;
                    stackCount--;
                }
            }

            return stackCount == 1;
        }

        public IEnumerable<int[]> CreateRpnPatternsForDigitCount(int count)
        {
            var based = new int[2 * count - 1];

            for (int i = 0; i < based.Length; i++)
                based[i] = i < count ? 1 : 0;

            foreach (var pattern in new Permutations<int>(based, 2, based.Length - 1))
            {
                if (IsValidRpnPattern(pattern.ToArray()))
                    yield return pattern.ToArray();
            }
        }
    }
}
