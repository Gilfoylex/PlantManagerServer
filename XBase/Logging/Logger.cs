namespace XBase.Logging;

public static class Logger
{
    public static ILogSink? Sink { get; set; }

    private static bool IsEnabled(LogLevel level, string area)
    {
        return Sink?.IsEnabled(level, area) == true;
    }

    public static ProxyLogger? TryGet(LogLevel level, string area)
    {
        if (!IsEnabled(level, area))
        {
            return null;
        }

        return new ProxyLogger(Sink!, level, area);
    }
    
    public static bool TryGet(LogLevel level, string area, out ProxyLogger outLogger)
    {
        var logger = TryGet(level, area);

        outLogger = logger.GetValueOrDefault();

        return logger.HasValue;
    }
}