using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace Section_11___Notes_App.ViewModel.Helpers.Database
{
    public class SQLLiteDatabaseHelper : IDatabaseHelper
    {

        private string dbPath = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public string DBPath 
        { 
            get 
            {
                return DBPath;
            } 
        }

        public async Task<bool> Insert<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBPath))
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

        public async Task<bool> Update<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBPath))
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

        public async Task<bool> Delete<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBPath))
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

        public async Task<List<T>> Read<T>() where T : new()
        {
            using (SQLiteConnection connection = new SQLiteConnection(DBPath))
            {
                connection.CreateTable<T>();
                List<T> items = connection.Table<T>().ToList();

                return items;
            }
        }
    }
}
