using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

/*
 * Projekt - Zadanie 1
 * Autor: Paweł Szram, sem. 3, I stop.
*/

namespace Calculator
{
    public partial class Form1 : Form
    {
        private readonly char[] allowedSigns = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '-', '*', '/', ' ', ',', ')', '(' };

        public Form1()
        {
            InitializeComponent();
            RegisterClickEventForEachButton();
        }

        private bool IsCharSign(char sign)
        {
            if (sign == '+' || sign == '-' || sign == '/' || sign == '*' || sign == '(' || sign == ')') return true;
            return false;
        }

        private void AddSignToExpression(char sign)
        {
            resultTextBox.Text += " " + sign + " ";
            resultTextBox.SelectionStart = resultTextBox.Text.Length + 1;
            resultTextBox.SelectionLength = 0;
        }

        private void resultTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!ValidateTextBox(ref e) && e.KeyChar != (char)8)
            {
                MessageBox.Show("Error! Incorrect value!");
                e.Handled = true;
            }

            if (IsCharSign(e.KeyChar))
            {
                e.Handled = true;
                AddSignToExpression(e.KeyChar);
            }
        }

        private bool ValidateTextBox(ref KeyPressEventArgs sign)
        {
            foreach(char checkedSign in allowedSigns)
            {
                if (checkedSign == sign.KeyChar) return true;
            }
            return false;
        }

        private void buttonClick(object sender, EventArgs e)
        {
            string whichButton = ((Button) sender).Text;
            if(IsCharSign(whichButton[0])) AddSignToExpression(whichButton[0]);
            else resultTextBox.Text += whichButton;
        }

        private void RegisterClickEventForEachButton()
        {
            var listOfButtons = FindControls<Button>(this);
            foreach (Button actuallyButton in listOfButtons)
            {
                if (actuallyButton.Text == "C" || actuallyButton.Text == "=") continue;
                actuallyButton.Click += new EventHandler(buttonClick);
            }
        }

        private IEnumerable<T> FindControls<T>(Control control) where T : Control
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => FindControls<T>(ctrl))
                .Concat(controls)
                .Where(c => c.GetType() == typeof(T)).Cast<T>();
        }

        private void signButtonEqual_Click(object sender, EventArgs e)
        {
            try
            {
                string result = "( " + resultTextBox.Text + " )";
                ONP resultFromONP = new ONP(result);
                
                resultLabel.Text = "Result: " + resultFromONP.Result.Calculate();
                resultTextBox.Text = String.Empty;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                resultTextBox.Text = string.Empty;
            }
        }

        private void signButtonC_Click(object sender, EventArgs e)
        {
            resultTextBox.Text = String.Empty;
            resultLabel.Text = "Result: ";
        }
    }
}
