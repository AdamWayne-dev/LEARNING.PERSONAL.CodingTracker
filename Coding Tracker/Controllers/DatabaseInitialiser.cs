using Dapper;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker.Controllers
{
    internal class DatabaseInitialiser
    {
        public DatabaseInitialiser(string databasePath)
        {
             using var connection = new SqliteConnection($"Data Source={databasePath}");
             connection.Open();

            const string createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT NOT NULL,
                    EndTime TEXT NOT NULL
                );";

            connection.Execute(createTableQuery);
        }
    }
}
