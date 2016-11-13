using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    internal class Division : BinaryExpression
    {
        public Division(Expression first, Expression second) : base(first, second)
        {
        }

        public override double Calculate(double? point = default(double?))
        {
            return FirstExpression.Calculate(point) / SecondExpression.Calculate(point);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(FirstExpression);
            sb.Append(" / ");
            sb.Append(SecondExpression);
            return sb.ToString();
        }
    }
}
