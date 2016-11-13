using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal abstract class BinaryExpression : Expression
    {
        protected readonly Expression FirstExpression, SecondExpression;

        protected BinaryExpression(Expression first, Expression second)
        {
            FirstExpression = first;
            SecondExpression = second;
        }
    }
}
