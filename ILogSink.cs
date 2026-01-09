namespace RedOwl;

public interface ILogSink : IDisposable
{
    public void Write(LogEntry entry);
}

public struct LogEntry
{
    public LogLevel Level;
    public int LineNumber;
    public string FilePath;
    public string Message;
    public DateTime Time;

    public string ShortName => Level.GetShortName();
    public string SimpleFormat => $"[{Time:HH:mm:ss}] {Message}";
    public string ComplexFormat => $"[{DateTime.Now:HH:mm:ss}] {Level.ToString()[..4]} {FilePath}({LineNumber}): {Message}";
}