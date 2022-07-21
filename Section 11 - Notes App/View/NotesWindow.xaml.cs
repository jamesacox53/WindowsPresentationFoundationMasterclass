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

namespace Section_11___Notes_App.View
{
    /// <summary>
    /// Interaction logic for NotesWindow.xaml
    /// </summary>
    public partial class NotesWindow : Window
    {
        private SpeechRecognitionEngine speechRecognizer;

        public NotesWindow()
        {
            InitializeComponent();

            speechRecognizer = SetUpSpeechRecognitionEngine();
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
    }
}
