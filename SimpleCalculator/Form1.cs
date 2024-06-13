using System.CodeDom.Compiler;
using System.DirectoryServices;

namespace SimpleCalculator
{
    public partial class CalculatorForm : Form
    {
        #region variables
        // As I said simple calculator only support these operations
        string[] _operatorList = new string[] {"+", "-", "*", "/"};

        // reservedNumber1 is before operator entered, reservedNumber2 will be set after =
        double? _reservedNumber1 = null, _reservedNumber2 = null;

        // I need to know which operator selected
        string _operator = null;

        // As you see after calculation it continue to keep previous value in textbox, we want to clear after =
        bool _cleartext = false;
        #endregion
        public CalculatorForm()
        {
            InitializeComponent();
        }

        private void ResultLabel_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // All buttons will be handled here
            var text = ((Button)sender).Text;


            // If the button is an operator, we need store the first value
            var isOperator = _operatorList.Any(p => p == text);
            if (isOperator)
            {
                if (double.TryParse(InputTextBox.Text, out double temp))
                {
                    _reservedNumber1 = temp;
                    InputTextBox.Clear();
                    ResultLabel.Text = $"{_reservedNumber1} {text}";
                    _operator  = text;
                }
            }
            else 
            if (text == "=")
            {
                if (double.TryParse(InputTextBox.Text, out double temp))
                {
                    _reservedNumber2 = temp;
                }
                Calculate();
                _cleartext = true;
            }
            else
            {
                if (_cleartext)
                {
                    InputTextBox.Text = text;

                    // Only once will be cleared then rest will be the same flow
                    _cleartext = false;
                }
                else
                {
                    InputTextBox.Text += text;
                }
            }
        }

        private void Calculate()
        {
            double? result = 0;
            switch (_operator)
            {
                case "+":
                    result = _reservedNumber1 + _reservedNumber2;
                    break;
                case "-":
                    result = _reservedNumber1 - _reservedNumber2;
                    break;
                case "*":
                    result = _reservedNumber1 * _reservedNumber2;
                    break;
                case "/":
                    result = _reservedNumber1 / _reservedNumber2;
                    break;
                default:
                    break;
            }
            ResultLabel.Text = result.ToString();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            // Create the clear button
            InputTextBox.Clear();
            ResultLabel.Text = String.Empty;
        }
    }
}
