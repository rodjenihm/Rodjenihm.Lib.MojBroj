﻿using Rodjenihm.Lib.Combinatorics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public sealed class RpnMap : IRpnMap
    {
        public List<IEnumerable<int[]>> Patterns { get; private set; }
        public int Size { get => Patterns.Count; }

        public RpnMap(int maxDigitCount)
        {
            Patterns = new List<IEnumerable<int[]>>(maxDigitCount - 1);
            InitializeRpnMap(maxDigitCount);
        }

        public IEnumerable<int[]> this[int i]
        {
            get
            {
                if (i - 2 > Patterns.Capacity && i - 2 < 0)
                    throw new IndexOutOfRangeException($"RpnMap indexer has to be in 2..{Patterns.Capacity + 1} range");

                return Patterns[i - 2];
            }
        }

        private void InitializeRpnMap(int maxDigitCount)
        {
            for (int i = 2; i <= maxDigitCount; i++)
                Patterns.Add(CreateRpnPatternsForDigitCount(i));
        }

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

        private IEnumerable<int[]> CreateRpnPatternsForDigitCount(int count)
        {
            var based = new int[2 * count - 1];

            for (int i = 0; i < based.Length; i++)
                based[i] = i < count ? 1 : 0;

            foreach (var pattern in new Permutations<int>(based, start: 2, end: based.Length - 1))
            {
                if (IsValidRpnPattern(pattern.ToArray()))
                    yield return pattern.ToArray();
            }
        }
    }
}
