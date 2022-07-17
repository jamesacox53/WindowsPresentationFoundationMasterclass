using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Path = System.IO.Path;

namespace Section_11___Notes_App.ViewModel.Helpers
{
    public class DatabaseHelper
    {
        public static string dbFile = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public static bool Insert<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
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

        public static bool Update<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
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

        public static bool Delete<T>(T item)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
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

        public static List<T> Read<T>() where T : new()
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbFile))
            {
                connection.CreateTable<T>();
                List<T> items = connection.Table<T>().ToList();

                return items;
            }
        }
    }
}
