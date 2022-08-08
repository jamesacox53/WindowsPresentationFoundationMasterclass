using Section_11___Notes_App.Model;
using Section_11___Notes_App.ViewModel.Commands;
using Section_11___Notes_App.ViewModel.Helpers.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;

namespace Section_11___Notes_App.ViewModel
{
    public class NotesVM : INotifyPropertyChanged
    {
        public ObservableCollection<INotebook> Notebooks { get; set; }

        private INotebook selectedNotebook;

        public INotebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set 
            { 
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private INote selectedNote;

        public INote SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        public ObservableCollection<INote> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public CloseApplicationCommand CloseApplicationCommand { get; set; }

        public EditCommand EditCommand { get; set; }

        public EndEditingCommand EndEditingCommand { get; set; }

        public EditNoteCommand EditNoteCommand { get; set; }

        public EndNoteEditingCommand EndNoteEditingCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public event EventHandler SelectedNoteChanged;

        private Visibility isVisible;

        public Visibility IsVisible
        {
            get { return isVisible; }
            set 
            {
                isVisible = value;
                OnPropertyChanged("IsVisible");
            }
        }

        private Visibility isVisibleNote;

        public Visibility IsVisibleNote
        {
            get { return isVisibleNote; }
            set
            {
                isVisibleNote = value;
                OnPropertyChanged("IsVisibleNote");
            }
        }

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand();
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            EditNoteCommand = new EditNoteCommand(this);
            EndNoteEditingCommand = new EndNoteEditingCommand(this);

            Notebooks = new ObservableCollection<INotebook>();
            Notes = new ObservableCollection<INote>();

            IsVisible = Visibility.Collapsed;

            IsVisibleNote = Visibility.Collapsed;

            GetNotebooks();
        }

        public async void CreateNotebook()
        {
            INotebook newNotebook = Database.DatabaseHelper.CreateNotebook(App.User);

            if (newNotebook == null) return;

            await Database.DatabaseHelper.InsertNotebook(newNotebook);

            GetNotebooks();
        }

        public async void CreateNote(INotebook notebook)
        {
            INote newNote = Database.DatabaseHelper.CreateNote(notebook);

            await Database.DatabaseHelper.InsertNote(newNote);

            GetNotes();
        }

        public async void GetNotebooks()
        {
            IEnumerable<INotebook> notebooks = await Database.DatabaseHelper.ReadNotebooks(App.User); 

            Notebooks.Clear();

            if (notebooks == null) return;

            foreach(INotebook notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async void GetNotes()
        {
            if (SelectedNotebook == null) return;

            IEnumerable<INote> notes = await Database.DatabaseHelper.ReadNotes(SelectedNotebook);

            Notes.Clear();

            if (notes == null) return;

            foreach (INote note in notes)
            {
                Notes.Add(note);
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartEditing()
        {
            IsVisible = Visibility.Visible;
        }

        public async void StopEditing(INotebook notebook)
        {
            IsVisible = Visibility.Collapsed;

            await  Database.DatabaseHelper.UpdateNotebook(notebook);

            GetNotebooks();
        }

        public void StartEditingNote()
        {
            IsVisibleNote = Visibility.Visible;
        }

        public async void StopEditingNote(INote note)
        {
            IsVisibleNote = Visibility.Collapsed;

            await Database.DatabaseHelper.UpdateNote(note);

            GetNotes();
        }
    }
}
