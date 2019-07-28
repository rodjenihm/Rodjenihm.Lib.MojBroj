using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rodjenihm.Lib.MojBroj
{
    public static class Equation
    {
        private static readonly string[] operators = new string[] { "*", "+", "-", "/" };

        internal static int Precedence(char @operator)
        {
            switch (@operator)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;
            }
            return -1;
        }

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
                infix = ConvertPostfixToInfix(postfix);
            }
            catch
            {
                success = false;
            }

            return success;
        }

        public static string ConvertInfixToPostfix(string infix)
        {
            var postfix = string.Empty;
            var stack = new Stack<char>();

            int i = 0;
            while (i < infix.Length)
            {
                var token = infix[i];
                if (char.IsLetterOrDigit(token))
                {
                    var number = string.Empty;
                    while (i < infix.Length && char.IsLetterOrDigit(infix[i]))
                    {
                        number += infix[i++];
                    }
                    postfix += $"{number} ";
                }
                else
                {
                    if (token == '(')
                    {
                        stack.Push(token);
                    }
                    else if (token == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                        {
                            postfix += stack.Pop() + " ";
                        }

                        if (stack.Count > 0 && stack.Peek() != '(')
                        {
                            throw new ArgumentException("Invalid infix expression", nameof(infix));
                        }
                        else
                        {
                            stack.Pop();
                        }
                    }
                    else if (operators.Contains(token.ToString()))
                    {
                        while (stack.Count > 0 && Precedence(token) <= Precedence(stack.Peek()))
                        {
                            postfix += stack.Pop() + " ";
                        }

                        stack.Push(token);
                    }
                    i++;
                }
            }

            while (stack.Count > 1)
            {
                postfix += $"{stack.Pop()} ";
            }
            postfix += stack.Pop();
            return postfix;
        }

        public static bool TryConvertInfixToPostfix(string infix, out string postfix)
        {
            postfix = string.Empty;
            var success = true;

            try
            {
                postfix = ConvertInfixToPostfix(infix);
            }
            catch
            {
                success = false;
            }

            return success;
        }
    }
}
