using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public sealed class Solution
    {
        private int value;
        private int[] numbers;
        private int[] stOperators;
        private int[] pattern;
        private readonly string[] ops = new string[] { "*", "+", "-", "/" };

        public string Postfix { get; internal set; }
        public string Infix { get; internal set; }

        internal Solution()
        {
        }

        internal Solution(int value, int[] numbers, int[] stOperators, int[] pattern)
        {
            this.value = value;
            this.numbers = numbers ?? throw new ArgumentNullException(nameof(numbers));
            this.stOperators = stOperators ?? throw new ArgumentNullException(nameof(stOperators));
            this.pattern = pattern ?? throw new ArgumentNullException(nameof(pattern));
            Postfix = CreatePostfixFromStacks();
            Infix = $"{ConvertPostfixToInfix()} = {value}";
        }

        private string CreatePostfixFromStacks()
        {
            string output = string.Empty;
            var i = 0;
            var j = 0;

            foreach (var token in pattern)
            {
                if (token == 1)
                    output += numbers[i++] + " ";
                else
                    output += ops[stOperators[j++]] + " ";
            }

            return output;
        }

        private int GetPrecedence(string op)
        {
            switch (op)
            {
                default:
                    return 3;

                case "*":
                case "/":
                    return 2;

                case "+":
                case "-":
                    return 1;
            }
        }

        private string ConvertPostfixToInfix()
        {
            var split = Postfix.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var st = new Stack<(string expression, int precedence)>();

            foreach (var item in split)
            {
                if (ops.Contains(item))
                {
                    if (st.Count < 2)
                        return "Invalid postfix expression!";

                    var (rightExpression, rightPrecedence) = st.Pop();
                    var (leftExpression, leftPrecedence) = st.Pop();

                    var newPrecedence = GetPrecedence(item);

                    if (newPrecedence > rightPrecedence)
                        rightExpression = $"({rightExpression})";

                    if (newPrecedence > leftPrecedence)
                        leftExpression = $"({leftExpression})";

                    st.Push(($"{leftExpression} {item} {rightExpression}", GetPrecedence(item)));
                }
                else
                {
                    st.Push((item, 3));
                }
            }

            return st.Count == 1 ? st.Pop().expression : "Invalid postfix expression!";
        }
    }
}
