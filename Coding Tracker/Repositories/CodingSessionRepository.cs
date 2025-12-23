using Dapper;
using Microsoft.Data.Sqlite;

namespace Coding_Tracker.Repositories
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

        public List<CodingSession> GetFilteredSession(DateTime? fromDate, DateTime? toDate)
        {
            const string sql = @"
                SELECT Id, StartTime, EndTime
                FROM CodingSessions
                WHERE (@FromDate IS NULL OR StartTime >= @FromDate)
                  AND (@ToDate IS NULL OR EndTime <= @ToDate)
                ORDER BY StartTime DESC;";

            using var connection = OpenConnection();
            return connection.Query<CodingSession>(sql, new { FromDate = fromDate, ToDate = toDate }).ToList();
        }
         
    }
}
