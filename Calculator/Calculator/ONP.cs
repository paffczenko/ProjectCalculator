using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator
{
    class ONP
    {
        private Stack<Expression> stack;
        private string input;
        private StringBuilder output;
        private Stack<char> operators;

        public Expression Result
        {
            get { return FromInfix(); }
        }

        public ONP(string input)
        {
            this.input = input;
        }

        private void AppendSymbol<T>(T sym)
        {
            output.Append(" ");
            output.Append(sym);
        }

        private void CloseLeftBracket()
        {
            var leftBracket = operators.Pop();
            while (!leftBracket.Equals('('))
            {
                AppendSymbol(leftBracket);
                leftBracket = operators.Pop();
            }
        }

        private void AddOperatorToStack(string sym)
        {
            var first = stack.Pop();
            var second = stack.Pop();
            switch (Convert.ToChar(sym))
            {
                case '+':
                    stack.Push(second.Add(first));
                    break;

                case '*':
                    stack.Push(second.Multiply(first));
                    break;

                case '/':
                    stack.Push(second.Divide(first));
                    break;
                case '-':
                    stack.Push(second.Substract(first));
                    break;
            }
        }

        private void AddOperatorToOnP(char symChar)
        {
            if (operators.Count == 0 || GetPriority(operators.Peek()) < GetPriority(symChar))
            {
                operators.Push(symChar);
                return;
            }

            var oper = operators.Peek();

            while (GetPriority(oper) >= GetPriority(symChar) && operators.Count > 0)
            {
                AppendSymbol(oper);
                operators.Pop();
                oper = operators.Peek();
            }

            operators.Push(symChar);
        }

        public void AddSymbolToOnp(string sym)
        {
            var symChar = Convert.ToChar(sym);
            switch (symChar)
            {
                case '(':
                    operators.Push(symChar);
                    break;

                case ')':
                    CloseLeftBracket();
                    break;

                case '*':
                case '+':
                case '-':
                case '/':
                    AddOperatorToOnP(symChar);
                    break;
            }
        }

        private static byte GetPriority(char sign)
        {
            switch (sign)
            {
                case '(':
                    return 0;

                case ')':
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                default:
                    throw new ArgumentException("Błędny operator!");
            }

        }

        private Expression FromOnp()
        {
            var arguments = Regex.Split(input, @"\s+");
            stack = new Stack<Expression>();

            for (int i = 0; i < arguments.Length; i++)
            {
                var sym = arguments[i];
                if(Regex.IsMatch(sym, @"^[\+|\-|\*|/]$")) AddOperatorToStack(sym);
                else stack.Push(Constant.GetConstant(Convert.ToDouble(sym)));
            }
            return stack.Peek();
        }

        private Expression FromInfix()
        {
            operators = new Stack<char>();
            output = new StringBuilder();
            var arguments = Regex.Split(input, @"\s+");

            foreach (var sym in arguments)
            {
                if (!Regex.IsMatch(sym, @"^[\+|\-|\*|/|(|)]$")) AppendSymbol(sym);
                else AddSymbolToOnp(sym);
            }

            if (operators.Count == 1) AppendSymbol(operators.Peek());

            output = output.Remove(0, 1);
            input = output.ToString();
            return FromOnp();
        }
    }
}
