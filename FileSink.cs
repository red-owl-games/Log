namespace RedOwl;

public class FileSink : ILogSink
{
    private static readonly Lock Lock = new();
    private readonly FileStream _stream;
    private readonly StreamWriter _writer;
    
    public FileSink(string filepath, bool append = false)
    {
        var dir = Path.GetDirectoryName(filepath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir)) 
            Directory.CreateDirectory(dir);
        
        _stream = new FileStream(
            filepath,
            append ? FileMode.Append : FileMode.Create,
            FileAccess.Write,
            FileShare.Read
        );

        _writer = new StreamWriter(_stream)
        {
            AutoFlush = true // ensures writes are flushed immediately
        };
    }
    
    public void Write(LogEntry entry)
    {
        lock (Lock)
        {
            _writer.WriteLine(entry.SimpleFormat);
        }
    }
    
    public void Dispose()
    {
        lock (Lock)
        {
            _writer?.Dispose();
            _stream?.Dispose();
        }
    }
}