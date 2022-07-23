using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_11___Notes_App.ViewModel.Commands
{
    public class EndNoteEditingCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public NotesVM NotesVM { get; set; }

        public EndNoteEditingCommand(NotesVM notesVM)
        {
            NotesVM = notesVM;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Note? note = (parameter as Note);

            if (note == null) return;

            NotesVM.StopEditingNote(note);
        }
    }
}
