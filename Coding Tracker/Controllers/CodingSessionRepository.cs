using Dapper;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker.Controllers
{
    internal class CodingSessionRepository
    {
        private readonly string _connectionString;

        public CodingSessionRepository(string databasePath)
        {
            _connectionString = $"Data Source={databasePath}";
        }

        private SqliteConnection OpenConnection() => new SqliteConnection(_connectionString);

        public int AddSession(CodingSession session)
        {
            const string sql = @"
                INSERT INTO CodingSessions (StartTime, EndTime)
                VALUES (@StartTime, @EndTime);
                SELECT last_insert_rowid();";

            using var connection = OpenConnection();
            return connection.ExecuteScalar<int>(sql, session);
        }

        public List<CodingSession> GetAllSessions()
        {
            const string sql = @"
                SELECT Id, StartTime, EndTime
                FROM CodingSessions
                ORDER BY StartTime DESC;";

            using var connection = OpenConnection();
            return connection.Query<CodingSession>(sql).ToList();
        }
    }
}
