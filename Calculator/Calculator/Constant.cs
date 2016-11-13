using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Constant : Expression
    {
        private double _value;

        protected Constant(double value)
        {
            _value = value;
        }

        public static Constant GetConstant(double value)
        {
            return new Constant(value);
        }

        public override double Calculate(double? point = null)
        {
            return _value;   
        }

        public override string ToString()
        {
            return string.Format("{0:0.00}", _value);
        }
    }
}
