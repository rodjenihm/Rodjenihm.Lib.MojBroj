using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rodjenihm.Lib.MojBroj
{
    public static class Equation
    {
        private static readonly string[] operators = new string[] { "*", "+", "-", "/" };

        internal static string CreatePostfixFromStacks(int[] numbers, int[] stOperators, int[] pattern)
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

        public static string ConvertPostfixToInfix(string postfix)
        {
            var stack = new Stack<(string expression, int operatorPrecedence)>();

            foreach (var item in postfix.Split(' '))
            {
                if (int.TryParse(item, out int temp))
                {
                    stack.Push((item, 3));
                }
                else if (operators.Contains(item))
                {
                    if (stack.Count < 2)
                        throw new ArgumentException("Invalid postfix expression", nameof(postfix));

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

            return stack.Count != 1 ? throw new ArgumentException("Invalid postfix expression", nameof(postfix)) : stack.Pop().expression;
        }

        public static bool TryConvertPostfixToInfix(string postfix, out string infix)
        {
            infix = string.Empty;
            var success = true;

            try
            {
                infix = ConvertInfixToPostfix(postfix);
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public static string ConvertInfixToPostfix(string infix)
        {
            return infix;
        }
    }
}
