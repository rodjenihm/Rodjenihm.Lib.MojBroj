using System.Collections.Generic;

namespace Rodjenihm.Lib.MojBroj
{
    public interface IRpnMap
    {
        List<IEnumerable<int[]>> Patterns { get; }
        int Size { get; }
        IEnumerable<int[]> this[int i] { get; }
    }
}