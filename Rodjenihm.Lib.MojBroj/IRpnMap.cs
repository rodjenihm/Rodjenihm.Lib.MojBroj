using System.Collections.Generic;

namespace Rodjenihm.Lib.MojBroj
{
    internal interface IRpnMap
    {
        List<IEnumerable<int[]>> Patterns { get; }
    }
}