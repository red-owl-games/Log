namespace RedOwl;

#if BROWSER
using System.Runtime.InteropServices.JavaScript;

internal partial class ConsoleSink : ILogSink
{
    [JSImport("globalThis.console.log")]
    private static partial void ConsoleLog([JSMarshalAs<JSType.String>] string message);
    [JSImport("globalThis.console.debug")]
    private static partial void ConsoleDebug([JSMarshalAs<JSType.String>] string message);
    [JSImport("globalThis.console.info")]
    private static partial void ConsoleInfo([JSMarshalAs<JSType.String>] string message);
    [JSImport("globalThis.console.warn")]
    private static partial void ConsoleWarn([JSMarshalAs<JSType.String>] string message);
    [JSImport("globalThis.console.error")]
    private static partial void ConsoleError([JSMarshalAs<JSType.String>] string message);
    
    public void Write(LogEntry entry)
    {
        try {
            switch (entry.Level)
            {
                case LogLevel.Critical:
                case LogLevel.Error:           ConsoleError(entry.SimpleFormat); break;
                case LogLevel.Warn:            ConsoleWarn(entry.SimpleFormat);  break;
                case LogLevel.Debug:           ConsoleDebug(entry.SimpleFormat); break;
                case LogLevel.Trace:           ConsoleLog(entry.SimpleFormat);   break;
                case LogLevel.None:
                case LogLevel.Info:
                default:                       ConsoleInfo(entry.SimpleFormat);  break;
            }
        } catch {}
    }

    public void Dispose() {}
}
#else
internal class ConsoleSink : ILogSink
{
    private static readonly object _lock = new();
    
    private static ConsoleColor GetLogColor(LogLevel level) => level switch
    {
        LogLevel.Trace => ConsoleColor.DarkMagenta,
        LogLevel.Debug => ConsoleColor.Blue,
        LogLevel.Info  => ConsoleColor.Gray,
        LogLevel.Warn  => ConsoleColor.Yellow,
        LogLevel.Error => ConsoleColor.DarkRed,
        LogLevel.Critical => ConsoleColor.Red,
        _ => ConsoleColor.White,
    };
    
    public void Write(LogEntry entry)
    {
        lock (_lock)
        {
            var prev = Console.ForegroundColor;
            try
            {
                Console.ForegroundColor = GetLogColor(entry.Level);
                Console.WriteLine(entry.SimpleFormat);
            }
            finally
            {
                Console.ForegroundColor = prev;
            }
        }
    }

    public void Dispose() {}
}
#endif
