using System.Collections.Generic;

namespace Rodjenihm.Lib.MojBroj
{
    internal interface IRpnMap
    {
        List<IEnumerable<int[]>> Patterns { get; }
        int Size { get; }
        IEnumerable<int[]> this[int i] { get; }
    }
}