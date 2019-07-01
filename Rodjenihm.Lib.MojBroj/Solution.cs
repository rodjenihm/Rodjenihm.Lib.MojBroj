using System;
using System.Collections.Generic;
using System.Linq;

namespace Rodjenihm.Lib.MojBroj
{
    public sealed class Solution
    {
        private readonly int[] numbers;
        private readonly int[] stOperators;
        private readonly int[] pattern;
        private readonly string[] operators = new string[] { "*", "+", "-", "/" };

        public string Postfix { get; internal set; }
        public string Infix { get; internal set; }

        internal Solution()
        {
        }

        internal Solution(int value, int[] numbers, int[] stOperators, int[] pattern)
        {
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
                    output += operators[stOperators[j++]] + " ";
            }

            return output;
        }

        private string ConvertPostfixToInfix()
        {
            var stack = new Stack<(string expression, int operatorPrecedence)>();

            foreach (var item in Postfix.Split(' '))
            {
                if (int.TryParse(item, out int temp))
                {
                    stack.Push((item, 3));
                }
                else if (operators.Contains(item))
                {
                    if (stack.Count < 2)
                        return "Invalid postfix expression";

                    (var rightExpression, var rightOperatorPrecedence) = stack.Pop();
                    (var leftExpression, var leftOperatorPrecedence) = stack.Pop();
                    var rootOperatorPrecedence = (item == "*" || item == "/") ? 2 : 1;

                    if (leftOperatorPrecedence < rootOperatorPrecedence)
                    {
                        leftExpression = $"({leftExpression})";
                    }

                    if (rightOperatorPrecedence < rootOperatorPrecedence)
                    {
                        rightExpression = $"({rightExpression})";

                    }

                    if ((rightOperatorPrecedence == rootOperatorPrecedence) && (item == "-" || item == "/"))
                    {
                        rightExpression = $"({rightExpression})";
                    }

                    stack.Push((leftExpression + item + rightExpression, rootOperatorPrecedence));
                }
            }

            return stack.Count != 1 ? "Invalid postfix expression" : stack.Pop().expression;
        }
    }
}
