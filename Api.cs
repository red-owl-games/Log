using System.Runtime.CompilerServices;

namespace RedOwl;

public static class Log
{
    public static ILogger Logger { get; } = new Logger();
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Print(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Logger.Print(message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Trace(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Logger.Trace(message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Debug(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Logger.Debug(message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Info(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Logger.Info(message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Warn(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Logger.Warn(message, filePath, lineNumber);
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void Error(string message, [CallerFilePath] string filePath = "", [CallerLineNumber] int lineNumber = 0) 
        => Logger.Error(message, filePath, lineNumber);
}