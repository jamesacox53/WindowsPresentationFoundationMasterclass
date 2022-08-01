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
        private IDatabaseHelper databaseHelper;

        public ObservableCollection<Notebook> Notebooks { get; set; }

        private Notebook selectedNotebook;

        public Notebook SelectedNotebook
        {
            get { return selectedNotebook; }
            set 
            { 
                selectedNotebook = value;
                OnPropertyChanged("SelectedNotebook");
                GetNotes();
            }
        }

        private Note selectedNote;

        public Note SelectedNote
        {
            get { return selectedNote; }
            set
            {
                selectedNote = value;
                OnPropertyChanged("SelectedNote");
                SelectedNoteChanged?.Invoke(this, new EventArgs());
            }
        }

        public ObservableCollection<Note> Notes { get; set; }

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
            databaseHelper = DatabaseHelper.Database;

            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand();
            EditCommand = new EditCommand(this);
            EndEditingCommand = new EndEditingCommand(this);
            EditNoteCommand = new EditNoteCommand(this);
            EndNoteEditingCommand = new EndNoteEditingCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            IsVisibleNote = Visibility.Collapsed;

            GetNotebooks();
        }

        public async void CreateNotebook()
        {
            Notebook newNotebook = new Notebook()
            {
                Name = "New Notebook",
                UserId = App.UserId
            };

            await databaseHelper.Insert<Notebook>(newNotebook);

            GetNotebooks();
        }

        public async void CreateNote(int notebookId)
        {
            Note newNote = new Note()
            {
                Title = "New Note",
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            await databaseHelper.Insert<Note>(newNote);

            GetNotes();
        }

        public async void GetNotebooks()
        {
            List<Notebook> listOfNotebooks = await databaseHelper.Read<Notebook>();

            IEnumerable<Notebook> notebooks = listOfNotebooks.Where(nb => nb.UserId == App.UserId); 

            Notebooks.Clear();

            foreach(Notebook notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private async void GetNotes()
        {
            if (SelectedNotebook == null) return;

            List<Note> listOfNotes = await databaseHelper.Read<Note>();

            IEnumerable<Note> notes = listOfNotes.Where(n => n.NotebookId == SelectedNotebook.Id);

            Notes.Clear();

            foreach (Note note in notes)
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

        public async void StopEditing(Notebook notebook)
        {
            IsVisible = Visibility.Collapsed;

            await databaseHelper.Update(notebook);

            GetNotebooks();
        }

        public void StartEditingNote()
        {
            IsVisibleNote = Visibility.Visible;
        }

        public async void StopEditingNote(Note note)
        {
            IsVisibleNote = Visibility.Collapsed;

            await databaseHelper.Update(note);

            GetNotes();
        }
    }
}
