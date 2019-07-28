using NUnit.Framework;
using System;

namespace Rodjenihm.Lib.MojBroj.Test
{
    public class EquationTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        [TestCase("4 3 +", "4+3")]
        [TestCase("3 2 1 + *", "3*(2+1)")]
        [TestCase("3 2 + 4 -", "3+2-4")]
        public void ConvertPostfixToInfix_ValidPostfixInput_OutputsInfix(string postfix, string expected)
        {
            var actual = Equation.ConvertPostfixToInfix(postfix);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        [TestCase("+ * 3")]
        [TestCase("3 4 + *")]
        [TestCase("3 + 5 *")]
        public void ConvertPostfixToInfix_InvalidPostfixInput_ThrowsArgumentException(string postfix)
        {
            Assert.Throws<ArgumentException>(() => Equation.ConvertPostfixToInfix(postfix));
        }
    }
}