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
        private readonly char[] ops = new char[] { '*', '+', '-', '/' };

        public string Solution { get; private set; }

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

        private string CreatePostfixFromStacks(int[] numbers, int[] opIds, int stoIdx, int[] pattern)
        {
            string output = string.Empty;
            var i = 0;
            var j = 0;

            foreach (var token in pattern)
            {
                if (token == 1)
                    output += numbers[i++] + " ";
                else
                    output += ops[opIds[j++]] + " ";
            }

            return output;
        }

        private string ConvertPostfixToInfix(string postfix)
        {
            var split = postfix.Split(' ');
            var st = new Stack<string>();

            foreach (var item in split)
            {
                if (item == "")
                    continue;

                if (item == "*" || item == "+" || item == "-" || item == "/")
                {
                    if (st.Count < 2)
                        return "Invalid postfix expression!";

                    var right = st.Pop();
                    var left = st.Pop();
                    st.Push($"({left} {item} {right})");
                }
                else
                {
                    st.Push(item.ToString());
                }
            }

            return st.Count == 1 ? st.Pop() : "Invalid postfix expression!";
        }

        private void SolveBranch(int[] numbers, int nIdx, int[] pattern, int pIdx, int result, int[] stn, int stnIdx, int[] sto, int stoIdx)
        {
            if (solved)
                return;

            if (pIdx == pattern.Length)
            {
                if (result == target)
                {
                    solved = true;
                    Solution = ConvertPostfixToInfix(CreatePostfixFromStacks(numbers, sto, stoIdx, pattern));
                }

                return;
            }

            while (pattern[pIdx] == 1)
            {
                stn[stnIdx++] = numbers[nIdx++];
                pIdx++;
            }

            foreach (var opId in opIds)
            {
                int right = stn[stnIdx - 1];
                int left = stn[stnIdx - 2];

                if (opId == 1 && (left == 1 || right == 1))
                    continue;

                if (opId == 2 && left <= right)
                    continue;

                if (opId == 3 && right == 0)
                    continue;

                if (opId == 3 && left % right != 0)
                    continue;

                result = Calc(left, right, opId);
                stn[stnIdx-- - 2] = result;
                sto[stoIdx++] = opId;

                SolveBranch(numbers, nIdx, pattern, pIdx + 1, result, stn, stnIdx, sto, stoIdx);

                stnIdx++;
                stn[stnIdx - 1] = right;
                stn[stnIdx - 2] = left;
                stoIdx--;
            }

        }

        public void Solve(IEnumerable<int> numbers, int target)
        {
            solved = false;
            this.target = target;

            if (numbers.Count() != rpnMap.Size + 1)
                rpnMap = new RpnMap(numbers.Count());

            for (int i = 2; i <= numbers.Count(); i++)
            {
                if (solved)
                    return;

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
        }
    }
}
