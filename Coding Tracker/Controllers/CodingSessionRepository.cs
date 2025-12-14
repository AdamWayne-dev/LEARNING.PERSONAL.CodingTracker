using Dapper;
using Microsoft.Data.Sqlite;
using Coding_Tracker.Models;

namespace Coding_Tracker.Controllers
{
    internal class CodingSessionRepository
    {
        private readonly string _connectionString;

        public CodingSessionRepository(string databasePath)
        {
            _connectionString = $"Data Source={databasePath}";
        }

        private SqliteConnection CreateConnection() => new SqliteConnection(_connectionString);

        public int Add(CodingSession session)
        {
            const string sql = @"
                INSERT INTO CodingSessions (StartTime, EndTime)
                VALUES (@StartTime, @EndTime);
                SELECT last_insert_rowid();";

            using var connection = CreateConnection();
            return connection.ExecuteScalar<int>(sql, new
            {
                session.StartTime,
                session.EndTime,
                session.Duration
            });
        }

        public List<CodingSession> GetAll()
        {
            const string sql = @"
                SELECT Id, StartTime, EndTime
                FROM CodingSessions
                ORDER BY StartTime DESC;";

            using var connection = CreateConnection();
            return connection.Query<CodingSession>(sql).ToList();
        }
    }
}
