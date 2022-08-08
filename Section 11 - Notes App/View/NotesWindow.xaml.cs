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
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;
using Section_11___Notes_App.ViewModel;
using Section_11___Notes_App.Model;
using Path = System.IO.Path;
using System.IO;
using Section_11___Notes_App.ViewModel.Helpers.Database;

namespace Section_11___Notes_App.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    { 
        private SpeechRecognitionEngine speechRecognizer;

        private NotesVM? notesVM;

        public NotesWindow()
        {
            InitializeComponent();
            
            speechRecognizer = SetUpSpeechRecognitionEngine();

            notesVM = (Resources["NotesVM"] as NotesVM); 

            if (notesVM != null)
            {
                notesVM.SelectedNoteChanged += NotesVM_SelectedNoteChanged;
            }

            SetUpFontFamilyComboBox();

            SetUpFontSizeComboBox();
        }

        private void SetUpFontFamilyComboBox()
        {
            IEnumerable<FontFamily> fontFamilies = Fonts.SystemFontFamilies.OrderBy(f => f.Source);
            fontFamilyComboBox.ItemsSource = fontFamilies;
        }

        private void SetUpFontSizeComboBox()
        {
            IEnumerable<double> fontSizes = new List<double>() 
            {
                8, 9, 10, 12, 14, 16, 20
            };
            fontSizeComboBox.ItemsSource = fontSizes;
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
            bool isBoldChecked = boldButton.IsChecked ?? false;

            if (isBoldChecked)
            {
                contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontWeightProperty, FontWeights.Normal);
            }
        }

        bool isRecording = false;

        private void SpeechButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isRecording)
            {
                speechRecognizer.RecognizeAsync(RecognizeMode.Multiple);
                isRecording = true;
            }
            else
            {
                speechRecognizer.RecognizeAsyncStop();
                isRecording = false;
            }
        }

        private SpeechRecognitionEngine SetUpSpeechRecognitionEngine()
        {
            RecognizerInfo? currentCulture = (from r in SpeechRecognitionEngine.InstalledRecognizers()
                                              where r.Culture.Equals(Thread.CurrentThread.CurrentCulture)
                                              select r).FirstOrDefault();

            SpeechRecognitionEngine speechRecognition;

            if (currentCulture != null)
            {
                speechRecognition = new SpeechRecognitionEngine(currentCulture);
            }
            else
            {
                speechRecognition = new SpeechRecognitionEngine();
            }

            GrammarBuilder builder = new GrammarBuilder();
            builder.AppendDictation();
            Grammar grammar = new Grammar(builder);

            speechRecognition.LoadGrammar(grammar);

            speechRecognition.SetInputToDefaultAudioDevice();

            speechRecognition.SpeechRecognized += SpeechRecognition_SpeechRecognized;

            return speechRecognition;
        }

        private void SpeechRecognition_SpeechRecognized(object? sender, SpeechRecognizedEventArgs e)
        {
            string recognizedText = e.Result.Text;

            contentsRichTextBox.Document.Blocks.Add(new Paragraph(new Run(recognizedText)));
        }

        private void contentsRichTextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            Object selectedFontWeight = contentsRichTextBox.Selection.GetPropertyValue(Inline.FontWeightProperty);
            Object selectedFontStyle = contentsRichTextBox.Selection.GetPropertyValue(Inline.FontStyleProperty);
            Object selectedFontDecoration = contentsRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty);

            setButtonCheckStatus(boldButton, selectedFontWeight, FontWeights.Bold);
            setButtonCheckStatus(italicsButton, selectedFontStyle, FontStyles.Italic);
            setButtonCheckStatus(underlineButton, selectedFontDecoration, TextDecorations.Underline);

            fontFamilyComboBox.SelectedItem = contentsRichTextBox.Selection.GetPropertyValue(Inline.FontFamilyProperty);
            fontSizeComboBox.Text = contentsRichTextBox.Selection.GetPropertyValue(Inline.FontSizeProperty).ToString();
        }

        private void setButtonCheckStatus(ToggleButton button, Object selectedProperty, Object propertyWanted)
        {
            bool isUnsetValue = (selectedProperty == DependencyProperty.UnsetValue);

            if (isUnsetValue)
            {
                button.IsChecked = false;
                return;
            }

            button.IsChecked = selectedProperty.Equals(propertyWanted);
        }

        private void italicsButton_Click(object sender, RoutedEventArgs e)
        {
            bool isItalicsChecked = italicsButton.IsChecked ?? false;

            if (isItalicsChecked)
            {
                contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void underlineButton_Click(object sender, RoutedEventArgs e)
        {
            bool isUnderlineChecked = underlineButton.IsChecked ?? false;

            if (isUnderlineChecked)
            {
                contentsRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
            }
            else
            {
                TextDecorationCollection textDecorations;
                TextDecorationCollection? textDec = (contentsRichTextBox.Selection.GetPropertyValue(Inline.TextDecorationsProperty) as TextDecorationCollection);

                if (textDec == null) return;

                textDec.TryRemove(TextDecorations.Underline, out textDecorations);

                contentsRichTextBox.Selection.ApplyPropertyValue(Inline.TextDecorationsProperty, textDecorations);
            }
        }

        private void fontFamilyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Object selectedItem = fontFamilyComboBox.SelectedItem;

            if (selectedItem == null) return;

            contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontFamilyProperty, selectedItem);
        }

        private void fontSizeComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string fontSizeString = fontSizeComboBox.Text;

            if (fontSizeString == null || !(double.TryParse(fontSizeString, out double res))) return;

            contentsRichTextBox.Selection.ApplyPropertyValue(Inline.FontSizeProperty, fontSizeString);
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (notesVM == null) return;

            INote selectedNote = notesVM.SelectedNote;

            if (selectedNote == null) return;

            try
            {
                await Database.DatabaseHelper.UpdateRTFFile(contentsRichTextBox, selectedNote);
            }
            catch (Exception)
            {
                return;
            }
        }

        private async void NotesVM_SelectedNoteChanged(object? sender, EventArgs e)
        {
            contentsRichTextBox.Document.Blocks.Clear();

            if (notesVM == null) return;

            INote selectedNote = notesVM.SelectedNote;

            if (selectedNote == null) return;

            try
            {
                await Database.DatabaseHelper.LoadRTFFileIntoRichTextBox(contentsRichTextBox, selectedNote);
            }
            catch (Exception)
            {
                return;
            }
            }

        /*
        
        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);

            if (!string.IsNullOrEmpty(App.UserId)) return;
            
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.ShowDialog();

            if (notesVM == null) return;

            notesVM.GetNotebooks();
        }

        */
    }
}
