using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public sealed class Solution
    {
        public string Postfix { get; internal set; }
        public string Infix { get; internal set; }

        internal Solution()
        {
        }

        internal Solution(int value, int[] numbers, int[] stOperators, int[] pattern)
        {
            Postfix = Equation.CreatePostfixFromStacks(numbers, stOperators, pattern);
            Infix = $"{Equation.ConvertPostfixToInfix(Postfix)} = {value}";
        }
    }
}
