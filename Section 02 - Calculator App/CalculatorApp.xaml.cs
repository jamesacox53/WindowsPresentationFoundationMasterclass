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
        public CalculatorApp()
        {
            InitializeComponent();

            resultLabel.Content = "12345";
        }

        private void acButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void negativeButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void percentageButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void divideButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void sevenButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("7");
        }

        private void eightButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("8");
        }

        private void nineButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("9");
        }

        private void multiplyButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void fourButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("4");
        }

        private void fiveButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("5");
        }

        private void sixButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("6");
        }

        private void minusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void oneButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("1");
        }

        private void twoButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("2");
        }

        private void threeButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("3");
        }

        private void plusButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void zeroButton_Click(object sender, RoutedEventArgs e)
        {
            buttonPressed("0");
        }

        private void decimalButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void equalsButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonPressed(string element)
        {
            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = element;
                return;
            }

            resultLabel.Content = resultLabel.Content.ToString() + element;
        }
    }
}
