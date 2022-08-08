using Newtonsoft.Json;
using Section_11___Notes_App.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;
using System.Xml;

namespace Section_11___Notes_App.ViewModel.Helpers.Database.Firebase
{
    public class FirebaseDatabaseHelper : IDatabaseHelper
    {
        private string dbPath = $"https://notes-app-wpf-c4211-default-rtdb.firebaseio.com/";

        public async Task<bool> InsertOnlyNoteObject(INote note)
        {
            FirebaseNote? firebaseNote = note as FirebaseNote;

            if (firebaseNote == null) return false;

            string? localId = await Insert<FirebaseNote>(firebaseNote);

            if (localId == null) return false;

            firebaseNote.Id = localId;

            bool wasSuccessful = await UpdateNote(firebaseNote);

            return wasSuccessful;
        }

        public async Task<bool> InsertNotebook(INotebook notebook)
        {
            FirebaseNotebook? firebaseNotebook = notebook as FirebaseNotebook;

            if (firebaseNotebook == null) return false;

            string? localId = await Insert<FirebaseNotebook>(firebaseNotebook);

            if (localId == null) return false;

            firebaseNotebook.Id = localId;

            bool wasSuccessful = await UpdateNotebook(firebaseNotebook);

            return wasSuccessful;
        }

        private async Task<string?> Insert<T>(T item)
        {
            string jsonBody = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                string url = $"{dbPath}{item.GetType().Name.ToLower()}.json";
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string resultJson = await response.Content.ReadAsStringAsync();
                    FirebaseInsert? result = JsonConvert.DeserializeObject<FirebaseInsert>(resultJson);

                    if (result == null) return null;

                    return result.name;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<IEnumerable<INote>> ReadNotes(INotebook notebook)
        {
            FirebaseNotebook? firebaseNotebook = notebook as FirebaseNotebook;

            if (firebaseNotebook == null) return null;

            List<FirebaseNote> listOfNotes = await Read<FirebaseNote>();

            if (listOfNotes == null) return null;

            IEnumerable<INote> notes = listOfNotes.Where(n => n.NotebookId == firebaseNotebook.Id);

            return notes;
        }

        public async Task<IEnumerable<INotebook>> ReadNotebooks(IUser user)
        {
            FirebaseUser? firebaseUser = user as FirebaseUser;

            if (firebaseUser == null) return null;

            List<FirebaseNotebook> listOfNotebooks = await Read<FirebaseNotebook>();

            if (listOfNotebooks == null) return null;

            IEnumerable<INotebook> notebooks = listOfNotebooks.Where(nb => nb.UserId == firebaseUser.LocalId);

            return notebooks;
        }

        private async Task<List<T>> Read<T>() where T : new()
        {
            using (var client = new HttpClient())
            {
                string url = $"{dbPath}{typeof(T).Name.ToLower()}.json";
                var result = await client.GetAsync(url);
                string jsonResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    Dictionary<string, T>? objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);
                    if (objects == null) return null;

                    List<T> values = new List<T>();

                    foreach (KeyValuePair<string, T> dictionaryEntry in objects)
                    {
                        values.Add(dictionaryEntry.Value);
                    }

                    return values;
                }
                else
                {
                    return null;
                }
            }
        }

        private async Task<Stream> ReadWithId<T>(string itemId) where T : new()
        {
            using (var client = new HttpClient())
            {
                string url = $"{dbPath}{typeof(T).Name.ToLower()}/{itemId}.json";
                var result = await client.GetAsync(url);
                string jsonResult = await result.Content.ReadAsStringAsync();

                if (result.IsSuccessStatusCode)
                {
                    Dictionary<string, T>? objects = JsonConvert.DeserializeObject<Dictionary<string, T>>(jsonResult);
                    if (objects == null || objects.Count <= 0) return null;

                    foreach (KeyValuePair<string, T> dictionaryEntry in objects)
                    {
                        string jsonBody = JsonConvert.SerializeObject(dictionaryEntry.Value);
                        StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                        return content.ReadAsStream();   
                    }
                }
                else
                {
                    return null;
                }
            }
            return null;
        }

        public async Task<bool> UpdateNote(INote note)
        {
            FirebaseNote? firebaseNote = note as FirebaseNote;

            if (firebaseNote == null) return false;

            bool successfullyUpdated = await Update<FirebaseNote>(firebaseNote, firebaseNote.Id);

            return successfullyUpdated;
        }

        public async Task<bool> UpdateNotebook(INotebook notebook)
        {
            FirebaseNotebook? firebaseNotebook = notebook as FirebaseNotebook;

            if (firebaseNotebook == null) return false;

            bool successfullyUpdated = await Update<FirebaseNotebook>(firebaseNotebook, firebaseNotebook.Id);

            return successfullyUpdated;
        }

        private async Task<bool> Update<T>(T item, string itemId)
        {
            string jsonBody = JsonConvert.SerializeObject(item);
            StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

            using (var client = new HttpClient())
            {
                string url = $"{dbPath}{item.GetType().Name.ToLower()}/{itemId}.json";
                var result = await client.PatchAsync(url, content);

                if (result.IsSuccessStatusCode)
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
            FirebaseNote? firebaseNote = note as FirebaseNote;

            if (firebaseNote == null) return false;

            bool successfullyUpdated = await Delete<FirebaseNote>(firebaseNote, firebaseNote.Id);

            return successfullyUpdated;
        }

        public async Task<bool> DeleteNotebook(INotebook notebook)
        {
            FirebaseNotebook? firebaseNotebook = notebook as FirebaseNotebook;

            if (firebaseNotebook == null) return false;

            bool successfullyUpdated = await Delete<FirebaseNotebook>(firebaseNotebook, firebaseNotebook.Id);

            return successfullyUpdated;
        }

        private async Task<bool> Delete<T>(T item, string itemId)
        {
            using (var client = new HttpClient())
            {
                string url = $"{dbPath}{item.GetType().Name.ToLower()}/{itemId}.json";
                var result = await client.DeleteAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public INote CreateNote(INotebook notebook)
        {
            FirebaseNotebook? firebaseNotebook = notebook as FirebaseNotebook;

            if (firebaseNotebook == null) return null;

            FirebaseNote newNote = new FirebaseNote()
            {
                Title = "New Note",
                NotebookId = firebaseNotebook.Id,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
            };

            return newNote;
        }

        public INotebook CreateNotebook(IUser user)
        {
            FirebaseUser? firebaseUser = user as FirebaseUser;

            if (firebaseUser == null) return null;

            FirebaseNotebook newNotebook = new FirebaseNotebook()
            {
                Name = "New Notebook",
                UserId = firebaseUser.LocalId
            };

            return newNotebook;
        }

        public IUser CreateUser()
        {
            return new FirebaseUser();
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

            FirebaseNote? firebaseNote = note as FirebaseNote;

            if (firebaseNote == null) return false;

            string rtfFileName = $"{firebaseNote.Id}.rtf";

            string rtfFilePath = Path.Combine(Environment.CurrentDirectory, rtfFileName);

            firebaseNote.RTFFileLocation = rtfFilePath;

            await Database.DatabaseHelper.UpdateNote(firebaseNote);

            FileStream fileStream = new FileStream(rtfFilePath, FileMode.Create);

            TextRange contents = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            contents.Save(fileStream, DataFormats.Rtf);

            return true;
        }

        public async Task<bool> DeleteRTFFile(INote note)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> LoadRTFFileIntoRichTextBox(RichTextBox richTextBox, INote note)
        {
            if (richTextBox == null) return false;

            FirebaseNote? firebaseNote = note as FirebaseNote;

            if (firebaseNote == null) return false;

            string fileLocation = firebaseNote.RTFFileLocation;

            if (string.IsNullOrEmpty(fileLocation)) return false;

            FileStream fileStream = new FileStream(fileLocation, FileMode.Open);

            TextRange contents = new TextRange(richTextBox.Document.ContentStart, richTextBox.Document.ContentEnd);

            try
            {
                contents.Load(fileStream, DataFormats.Rtf);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
