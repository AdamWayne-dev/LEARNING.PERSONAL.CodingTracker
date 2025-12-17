internal class CodingSession
{
    public int Id { get; set; }
    public DateTime StartTime { get; private set; }
    public DateTime EndTime { get; private set; }

    public TimeSpan Duration => EndTime - StartTime;

    public static CodingSession Create(DateTime startTime, DateTime endTime)
    {
        if (endTime < startTime)
            throw new ArgumentException("EndTime must be after StartTime.");

        return new CodingSession
        {
            StartTime = startTime,
            EndTime = endTime
        };
    }
}
