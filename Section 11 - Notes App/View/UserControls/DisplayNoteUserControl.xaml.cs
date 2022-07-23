using Section_11___Notes_App.Model;
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

namespace Section_11___Notes_App.View.UserControls
{
    /// <summary>
    /// Interaction logic for DisplayNoteUserControl.xaml
    /// </summary>
    public partial class DisplayNoteUserControl : UserControl
    {
        public Note Note
        {
            get { return (Note)GetValue(NoteProperty); }
            set { SetValue(NoteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NoteProperty =
            DependencyProperty.Register("Note", typeof(Note), typeof(DisplayNoteUserControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DisplayNoteUserControl? displayNoteUserControl = (d as DisplayNoteUserControl);

            if (displayNoteUserControl == null) return;

            displayNoteUserControl.DataContext = displayNoteUserControl.Note;
        }

        public DisplayNoteUserControl()
        {
            InitializeComponent();
        }
    }


}
