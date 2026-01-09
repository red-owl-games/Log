using System.Runtime.CompilerServices;

namespace RedOwl;

public interface ILogger : IDisposable
{
    LogLevel Level { get; set; }
    void AddSink(ILogSink sink);
    void Print(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
    void Trace(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
    void Debug(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
    void Info(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
    void Warn(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
    void Error(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0);
}

public class Logger : ILogger
{
    public LogLevel Level { get; set; } = LogLevel.Warn;

    private readonly List<ILogSink> _sinks = [];

    public Logger()
    {
        _sinks.Add(new ConsoleSink());
    }

    public void AddSink(ILogSink sink) => _sinks.Add(sink);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Print(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Write(LogLevel.None, message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Trace(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Write(LogLevel.Trace, message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Debug(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Write(LogLevel.Debug, message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Info(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Write(LogLevel.Info, message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Warn(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Write(LogLevel.Warn, message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Error(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Write(LogLevel.Error, message, filePath, lineNumber);

    internal void Write(LogLevel level, string message, string filePath = "", int lineNumber = 0)
    {
        if (level < Level) return;
        var entry = new LogEntry
        {
            LineNumber = lineNumber,
            FilePath = filePath,
            Level = level,
            Message = message,
            Time = DateTime.Now,
        };
        foreach (var sink in _sinks) sink.Write(entry);
    }

    public void Dispose()
    {
        foreach (var sink in _sinks) sink.Dispose();
    }
}