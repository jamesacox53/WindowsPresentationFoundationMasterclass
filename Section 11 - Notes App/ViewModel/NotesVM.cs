using Section_11___Notes_App.Model;
using Section_11___Notes_App.ViewModel.Commands;
using Section_11___Notes_App.ViewModel.Helpers;
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

        public ObservableCollection<Note> Notes { get; set; }

        public NewNotebookCommand NewNotebookCommand { get; set; }

        public NewNoteCommand NewNoteCommand { get; set; }

        public CloseApplicationCommand CloseApplicationCommand { get; set; }

        public EditCommand EditCommand { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;

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

        public NotesVM()
        {
            NewNotebookCommand = new NewNotebookCommand(this);
            NewNoteCommand = new NewNoteCommand(this);
            CloseApplicationCommand = new CloseApplicationCommand();
            EditCommand = new EditCommand(this);

            Notebooks = new ObservableCollection<Notebook>();
            Notes = new ObservableCollection<Note>();

            IsVisible = Visibility.Collapsed;

            GetNotebooks();
        }

        public void CreateNotebook()
        {
            Notebook newNotebook = new Notebook() 
            {
                Name = "New Notebook"
            };

            DatabaseHelper.Insert<Notebook>(newNotebook);

            GetNotebooks();
        }

        public void CreateNote(int notebookId)
        {
            Note newNote = new Note()
            {
                Title = "New Note",
                NotebookId = notebookId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            DatabaseHelper.Insert<Note>(newNote);

            GetNotes();
        }

        private void GetNotebooks()
        {
            List<Notebook> notebooks = DatabaseHelper.Read<Notebook>();

            Notebooks.Clear();

            foreach(Notebook notebook in notebooks)
            {
                Notebooks.Add(notebook);
            }
        }

        private void GetNotes()
        {
            if (SelectedNotebook == null) return;

            IEnumerable<Note> notes = DatabaseHelper.Read<Note>().Where(n => n.NotebookId == SelectedNotebook.Id);

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
    }
}
