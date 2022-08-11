using Section_11___Notes_App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Path = System.IO.Path;

namespace Section_11___Notes_App.ViewModel.Helpers.Database.SQLLite
{
    public class SQLLiteDatabaseHelper : IDatabaseHelper
    {

        private string dbPath = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        private async Task<bool> Insert<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<T>();
                int numRowsInserted = connection.Insert(item);

                if (numRowsInserted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> UpdateNote(INote note)
        {
            SQLLiteNote? sqlLiteNote = note as SQLLiteNote;

            if (sqlLiteNote == null) return false;

            bool successfullyUpdated = await Update<SQLLiteNote>(sqlLiteNote);

            return successfullyUpdated;
        }

        public async Task<bool> UpdateNotebook(INotebook notebook)
        {
            SQLLiteNotebook? sqlLiteNotebook = notebook as SQLLiteNotebook;

            if (sqlLiteNotebook == null) return false;

            bool successfullyUpdated = await Update<SQLLiteNotebook>(sqlLiteNotebook);

            return successfullyUpdated;
        }

        private async Task<bool> Update<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<T>();
                int numRowsInserted = connection.Update(item);

                if (numRowsInserted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<bool> DeleteNote(INote note)
        {
            SQLLiteNote? sqlLiteNote = note as SQLLiteNote;

            if (sqlLiteNote == null) return false;

            bool successfullyUpdated = await Delete<SQLLiteNote>(sqlLiteNote);

            return successfullyUpdated;
        }

        public async Task<bool> DeleteNotebook(INotebook notebook)
        {
            SQLLiteNotebook? sqlLiteNotebook = notebook as SQLLiteNotebook;

            if (sqlLiteNotebook == null) return false;

            bool successfullyUpdated = await Delete<SQLLiteNotebook>(sqlLiteNotebook);

            return successfullyUpdated;
        }

        public async Task<bool> Delete<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<T>();
                int numRowsInserted = connection.Delete(item);

                if (numRowsInserted > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task<IEnumerable<INote>> ReadNotes(INotebook notebook)
        {
            SQLLiteNotebook? sqlLiteNotebook = notebook as SQLLiteNotebook;

            if (sqlLiteNotebook == null) return null;

            List<SQLLiteNote> listOfNotes = await Read<SQLLiteNote>();

            if (listOfNotes == null) return null;

            IEnumerable<INote> notes = listOfNotes.Where(n => n.NotebookId == sqlLiteNotebook.Id);

            return notes;
        }

        public async Task<IEnumerable<INotebook>> ReadNotebooks(IUser user)
        {
            SQLLiteUser? sqlLiteUser = user as SQLLiteUser;

            if (sqlLiteUser == null) return null;

            List<SQLLiteNotebook> listOfNotebooks = await Read<SQLLiteNotebook>();

            if (listOfNotebooks == null) return null;

            IEnumerable<INotebook> notebooks = listOfNotebooks.Where(nb => nb.UserId == sqlLiteUser.Id);

            return notebooks;
        }

        private async Task<List<T>> Read<T>() where T : new()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<T>();
                List<T> items = connection.Table<T>().ToList();

                return items;
            }
        }

        public INote CreateNote(INotebook notebook) 
        {
            SQLLiteNotebook? sqlLiteNotebook = notebook as SQLLiteNotebook;

            if (sqlLiteNotebook == null) return null;

            SQLLiteNote newNote = new SQLLiteNote()
            {
                Title = "New Note",
                NotebookId = sqlLiteNotebook.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return newNote;
        }

        public INotebook CreateNotebook(IUser user) 
        {
            SQLLiteUser? sqlLiteUser = user as SQLLiteUser;

            if (sqlLiteUser == null) return null;

            SQLLiteNotebook newNotebook = new SQLLiteNotebook()
            {
                Name = "New Notebook",
                UserId = sqlLiteUser.Id
            };

            return newNotebook;
        }

        public IUser CreateUser()
        {
            return new SQLLiteUser();
        }

        public async Task<bool> InsertRTFFile(INote note)
        {
            bool successful = await SaveRTFFile(new RichTextBox(), note);

            return successful;
        }

        public async Task<bool> UpdateRTFFile(RichTextBox richTextBox, INote note)
        {
            bool successful = await SaveRTFFile(richTextBox, note);

            return successful;
        }

        private async Task<bool> SaveRTFFile(RichTextBox richTextBox, INote note) 
        {
            if (richTextBox == null) return false;

            SQLLiteNote? sqlLiteNote = note as SQLLiteNote;

            if (sqlLiteNote == null) return false;

            string rtfFileName = $"{sqlLiteNote.Id}.rtf";

            string rtfFilePath = Path.Combine(Environment.CurrentDirectory, rtfFileName);

            sqlLiteNote.FileLocation = rtfFilePath;

            await Database.DatabaseHelper.UpdateNote(sqlLiteNote);

            using (FileStream fileStream = new FileStream(rtfFilePath, FileMode.Create))
            {
                TextRange contents = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                contents.Save(fileStream, DataFormats.Rtf);
            }

            return true;
        }

        public async Task<bool> DeleteRTFFile(INote note)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoadRTFFileIntoRichTextBox(RichTextBox richTextBox, INote note)
        {
            if (richTextBox == null) return false;

            SQLLiteNote? sqlLiteNote = note as SQLLiteNote;

            if (sqlLiteNote == null) return false;

            string fileLocation = sqlLiteNote.FileLocation;

            if (string.IsNullOrEmpty(fileLocation)) return false;

            using (FileStream fileStream = new FileStream(fileLocation, FileMode.Open))
            {
                TextRange contents = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

                contents.Load(fileStream, DataFormats.Rtf);
            }

            return true;
        }

        public async Task<bool> InsertOnlyNoteObject(INote note)
        {
            SQLLiteNote? sqlLiteNote = note as SQLLiteNote;

            if (sqlLiteNote == null) return false;

            bool wasSuccessful = await Insert<SQLLiteNote>(sqlLiteNote);

            return wasSuccessful;
        }

        public async Task<bool> InsertNotebook(INotebook notebook)
        {
            SQLLiteNotebook? sqlLiteNotebook = notebook as SQLLiteNotebook;

            if (sqlLiteNotebook == null) return false;

            bool wasSuccessful = await Insert<SQLLiteNotebook>(sqlLiteNotebook);

            return wasSuccessful;
        }
    }
}
