using Rodjenihm.Lib.Combinatorics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public class MojBrojSolver
    {
        private IRpnMap rpnMap = new RpnMap(6);
        private volatile bool solved = false;
        private int target;
        private readonly int[] opsId = new int[] { 0, 1, 2, 3 };
        private readonly char[] ops = new char[] { '*', '+', '-', '/' };

        public string Solution { get; private set; }
        public List<IEnumerable<int[]>> Map { get => rpnMap.Patterns; }

        public MojBrojSolver()
        {
        }

        private int Calc(int x, int y, int op)
        {
            switch (op)
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

            throw new InvalidOperationException(nameof(op));
        }

        private string CreatePostfixFromStacks(int[] numbers, List<int> ops, int[] pattern)
        {
            string output = string.Empty;
            var n = 0;
            var o = 0;

            foreach (var token in pattern)
            {
                if (token == 1)
                    output += numbers[n++] + " ";
                else
                    output += this.ops[ops[o++]] + " ";
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

        private void SolveBranch(int[] numbers, int n, int[] pattern, int p, int result, Stack<int> st, List<int> opss)
        {
            if (solved)
                return;

            if (p < pattern.Length)
            {
                if (pattern[p] == 1)
                {
                    st.Push(numbers[n]);
                    SolveBranch(numbers, n + 1, pattern, p + 1, result, new Stack<int>(st.Reverse()), new List<int>(opss));
                }
                else
                {
                    foreach (var opId in opsId)
                    {
                        int y = st.Pop();
                        int x = st.Pop();

                        if (opId == 3 && y == 0)
                            continue;

                        //if (op == '-' && x <= y)
                        //    continue;

                        //if (op == '+' && x > y)
                        //    continue;

                        result = Calc(x, y, opId);
                        st.Push(result);
                        opss.Add(opId);
                        SolveBranch(numbers, n, pattern, p + 1, result, new Stack<int>(st.Reverse()), new List<int>(opss));
                        opss.RemoveAt(opss.Count - 1);
                        st.Pop();
                        st.Push(x);
                        st.Push(y);
                    }
                }
            }
            else
            {
                if (result == target)
                {
                    Solution = ConvertPostfixToInfix(CreatePostfixFromStacks(numbers, opss, pattern));
                    solved = true;
                }
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
                            SolveBranch(variation.ToArray(), 0, pattern, 0, 0, new Stack<int>(i), new List<int>(i - 1));
                        }
                    }
                }
            }
        }
    }
}
