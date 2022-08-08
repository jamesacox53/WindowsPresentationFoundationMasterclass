using Section_11___Notes_App.Model;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Section_11___Notes_App.ViewModel.Helpers.Database.SQLLite
{
    public class SQLLiteAuthHelper : IAuthHelper
    {
        private string dbPath = Path.Combine(Environment.CurrentDirectory, "notesDb.db3");

        public async Task<bool> Login(IUser user)
        {
            SQLLiteUser? sqlLiteUser = user as SQLLiteUser;

            if (sqlLiteUser == null) return false;

            SQLLiteUser? sqlLiteUserFromDb = await GetUser(sqlLiteUser);

            if (sqlLiteUserFromDb == null) return false;

            App.User = sqlLiteUserFromDb;

            return true;
        }

        public async Task<bool> Register(IUser user)
        {
            SQLLiteUser? sqlLiteUser = user as SQLLiteUser;

            if (sqlLiteUser == null) return false;

            bool doesUserExist = await DoesUserExist(sqlLiteUser);

            if (doesUserExist) return false;

            await InsertUser(sqlLiteUser);

            return await Login(sqlLiteUser);
        }

        private async Task<SQLLiteUser?> GetUser(SQLLiteUser sqlLiteUser)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<SQLLiteUser>();
                List<SQLLiteUser> items = connection.Table<SQLLiteUser>().ToList();

                foreach (SQLLiteUser user in items)
                {
                    if ((user.Username == sqlLiteUser.Username) && (user.Password == sqlLiteUser.Password))
                        return user;
                }

                return null;
            }
        }


        private async Task<bool> DoesUserExist(SQLLiteUser sqlLiteUser)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<SQLLiteUser>();
                List<SQLLiteUser> items = connection.Table<SQLLiteUser>().ToList();

                foreach (SQLLiteUser user in items)
                {
                    if (user.Username == sqlLiteUser.Username) return true;
                }

                return false;
            }
        }

        private async Task<bool> InsertUser(SQLLiteUser sqlLiteUser)
        {
            using (SQLiteConnection connection = new SQLiteConnection(dbPath))
            {
                connection.CreateTable<SQLLiteUser>();
                int numRowsInserted = connection.Insert(sqlLiteUser);

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
    }
}
