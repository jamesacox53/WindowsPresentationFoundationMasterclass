using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Section_02___Calculator_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class CalculatorApp : Window
    {
        private double lastNumber;
        private string? op = null;

        public CalculatorApp()
        {
            InitializeComponent();
        }

        private void acButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
        }

        private void negativeButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;

                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), out double tempNumber))
            {
                double resNumber;
                
                if (lastNumber == 0)
                {
                    resNumber = tempNumber / 100;
                }
                else
                {
                    resNumber = lastNumber * (tempNumber / 100);
                }
                
                resultLabel.Content = resNumber.ToString();
            }
        }

        private void operatorButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNum;

            if (!double.TryParse(resultLabel.Content.ToString(), out tempNum)) 
                return;

            string oper;

            try
            {
                oper = GetContentFromButton(sender);
            }
            catch (Exception)
            {
                return;
            }

            lastNumber = tempNum;

            op = oper;

            resultLabel.Content = "0";
        }

        private void numberButton_Click(object sender, RoutedEventArgs e)
        {
            string number;
            
            try
            {
                number = GetContentFromButton(sender);
            }
            catch(Exception)
            {
                return;
            }

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = number;
                return;
            }

            resultLabel.Content = resultLabel.Content.ToString() + number;
        }

        private void decimalButton_Click(object sender, RoutedEventArgs e)
        {
            string? content = resultLabel.Content.ToString();

            if (content == null || content == "0")
            {
                resultLabel.Content = ".";
                return;
            }

            if (content.Contains('.')) return;

            resultLabel.Content = content + ".";
        }

        private void equalsButton_Click(object sender, RoutedEventArgs e)
        {
            if (op == null) return;

            double secondNumber;

            if (!double.TryParse(resultLabel.Content.ToString(), out secondNumber))
                return;

            double result;

            if (op == "+")
                result = lastNumber + secondNumber;
            else if (op == "-")
                result = lastNumber - secondNumber;
            else if (op == "*")
                result = lastNumber * secondNumber;
            else
            {
                if (secondNumber == 0) 
                {
                    MessageBox.Show("Cannot divide by 0.", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return; 
                }
                result = lastNumber / secondNumber;
            }

            resultLabel.Content = result.ToString();
            lastNumber = result;

            op = null;
        }

        private string GetContentFromButton(object sender)
        {
            Button button = (Button)sender;

            string? content = button.Content.ToString();

            if (content == null)
                throw new ArgumentNullException();

            return content.ToString();
        }
    }
}
