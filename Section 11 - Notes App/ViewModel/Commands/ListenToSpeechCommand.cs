using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Section_11___Notes_App.ViewModel.Commands
{
    public class ListenToSpeechCommand : ICommand
    {
        public NotesVM NotesVM { get; set; }
        public event EventHandler? CanExecuteChanged;

        public ListenToSpeechCommand(NotesVM notesVM)
        {
            NotesVM = notesVM;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            NotesVM.ListenToSpeech();
        }
    }
}
