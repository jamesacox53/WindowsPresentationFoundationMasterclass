using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public interface IDatabaseHelper
    {
        public async Task<bool> InsertNote(INote note) 
        {
            bool isSuccessful = await InsertOnlyNoteObject(note);

            if (!isSuccessful) return false;

            isSuccessful = await InsertRTFFile(note);

            return isSuccessful;
        }

        public Task<bool> InsertOnlyNoteObject(INote note);

        public Task<bool> InsertNotebook(INotebook notebook);

        public Task<bool> UpdateNote(INote note);

        public Task<bool> UpdateNotebook(INotebook notebook);

        public Task<bool> DeleteNote(INote note);

        public Task<bool> DeleteNotebook(INotebook notebook);

        public Task<IEnumerable<INote>> ReadNotes(INotebook notebook);

        public Task<IEnumerable<INotebook>> ReadNotebooks(IUser user);

        public Task<bool> InsertRTFFile(INote note);

        public Task<bool> UpdateRTFFile(RichTextBox richTextBox, INote note);

        public Task<bool> DeleteRTFFile(INote note);

        public Task<bool> LoadRTFFileIntoRichTextBox(RichTextBox richTextBox, INote note);

        public INote CreateNote(INotebook notebook);

        public INotebook CreateNotebook(IUser user);

        public IUser CreateUser();
    }
}
