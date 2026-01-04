namespace RedOwl;

public enum LogLevel
{
    Trace,
    Debug,
    Info,
    Warn,
    Error,
    Critical,
    None,
}

public static class LogLevelExtensions
{
    public static string GetShortName(this LogLevel level) => level switch
    {
        LogLevel.Trace => "Trace",
        LogLevel.Debug => "Debug",
        LogLevel.Info  => "Info",
        LogLevel.Warn  => "Warn",
        LogLevel.Error => "Error",
        LogLevel.Critical => "Crit",
        _ => "None",
    };
}