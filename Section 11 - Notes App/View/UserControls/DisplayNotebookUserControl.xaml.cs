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
    /// Interaction logic for DisplayNotebookUserControl.xaml
    /// </summary>
    public partial class DisplayNotebookUserControl : UserControl
    {
        public Notebook Notebook
        {
            get { return (Notebook)GetValue(NotebookProperty); }
            set { SetValue(NotebookProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NotebookProperty =
            DependencyProperty.Register("Notebook", typeof(Notebook), typeof(DisplayNotebookUserControl), new PropertyMetadata(null, SetValues));

        private static void SetValues(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DisplayNotebookUserControl? displayNotebookUserControl = (d as DisplayNotebookUserControl);

            if (displayNotebookUserControl == null) return;

            displayNotebookUserControl.DataContext = displayNotebookUserControl.Notebook;
        }

        public DisplayNotebookUserControl()
        {
            InitializeComponent();
        }
    }
}
