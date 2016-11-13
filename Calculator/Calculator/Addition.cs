using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Addition : BinaryExpression
    {
        public Addition(Expression first, Expression second) : base(first, second)
        {

        }

        public override double Calculate(double? point = default(double?))
        {
            return FirstExpression.Calculate(point) + SecondExpression.Calculate(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("( ");
            sb.Append(FirstExpression);
            sb.Append(" + ");
            sb.Append(SecondExpression);
            sb.Append(" )");
            return sb.ToString();
        }
    }
}
