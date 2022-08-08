using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_11___Notes_App.ViewModel.Commands
{
    public class NewNoteCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged 
        {
            add 
            {
                CommandManager.RequerySuggested += value;
            }
            remove 
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public NewNoteCommand(NotesVM notesVM)
        {
            NotesVM = notesVM;
        }

        public bool CanExecute(object? parameter)
        {
            return CheckNotebookExists(parameter);
        }

        public void Execute(object? parameter)
        {
            if (!CheckNotebookExists(parameter)) return;

            INotebook selectedNotebook = parameter as INotebook;

            NotesVM.CreateNote(selectedNotebook);
        }

        private bool CheckNotebookExists(object? parameter) 
        {
            if (parameter == null) return false;

            INotebook selectedNotebook = parameter as INotebook;

            if (selectedNotebook == null) return false;

            return true;
        }
    }
}
