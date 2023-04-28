namespace XBase.Logging;

public class ConsoleLogSink : ILogSink
{
    private readonly LogLevel _level;

    public ConsoleLogSink(LogLevel level = LogLevel.Information)
    {
        _level = level;
    }
    public bool IsEnabled(LogLevel level, string area)
    {
        return level >= _level;
    }

    public void Log(LogLevel level, string area, string log)
    {
        Console.WriteLine($"[{level}][{area}] {log}");
    }
}