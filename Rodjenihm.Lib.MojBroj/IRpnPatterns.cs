using System.Collections.Generic;

namespace Rodjenihm.Lib.MojBroj
{
    internal interface IRpnPatterns
    {
        IEnumerable<int[]> CreateRpnPatternsForDigitCount(int count);
    }
}