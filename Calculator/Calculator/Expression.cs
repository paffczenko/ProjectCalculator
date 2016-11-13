using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Calculator
{
    public abstract class Expression
    {
        public abstract double Calculate(double? point = null);
        public abstract override string ToString();

        public virtual Expression Add(Expression expr)
        {
            return new Addition(this, expr);
        }

        public virtual Expression Substract(Expression expr)
        {
            return new Substraction(this, expr);
        }

        public Expression Multiply(Expression expr)
        {
            return new Multiplication(this, expr);
        }

        public Expression Divide(Expression expr)
        {
            return new Division(this, expr);
        }
    }
}
