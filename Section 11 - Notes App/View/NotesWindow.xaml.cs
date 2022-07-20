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
using System.Windows.Shapes;

namespace Section_11___Notes_App.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        public NotesWindow()
        {
            InitializeComponent();
        }

        private void contentRichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextPointer textStart = contentsRichTextBox.Document.ContentStart;
            TextPointer textEnd = contentsRichTextBox.Document.ContentEnd;

            int ammountOfCharacter = ((new TextRange(textStart, textEnd)).Text.Length) - 2;

            statusTextBlock.Text = $"Document Length: {ammountOfCharacter} characters";
        }

        private void boldButton_Click(object sender, RoutedEventArgs e)
        {
            contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
        }
    }
}
