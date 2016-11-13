using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Substraction : BinaryExpression
    {
        public Substraction(Expression first, Expression second) : base(first, second)
        {

        }

        public override double Calculate(double? point = default(double?))
        {
            return FirstExpression.Calculate(point) - SecondExpression.Calculate(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(FirstExpression);
            sb.Append(" - ");
            sb.Append(SecondExpression);
            return sb.ToString();
        }
    }
}
