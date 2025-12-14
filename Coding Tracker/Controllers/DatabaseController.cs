using Microsoft.Data.Sqlite;
using Dapper;
using System.Globalization;

namespace Coding_Tracker.Controllers
{
    internal class DatabaseController
    {
        private readonly SqliteConnection _connection;

        public DatabaseController(string databasePath)
        {
            _connection = new SqliteConnection($"Data Source={databasePath}");
            _connection.Open();
            InitializeDatabase();
        }

        private void InitializeDatabase()
        {
            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime TEXT NOT NULL,
                    EndTime TEXT NOT NULL
                );";
            _connection.Execute(createTableQuery);
        }
    }
}
