using System.Runtime.CompilerServices;

namespace XBase.Logging;

public readonly record struct ProxyLogger
{
    private readonly ILogSink _sink;
    private readonly LogLevel _level;
    private readonly string _area;

    public ProxyLogger(ILogSink sink, LogLevel level, string area)
    {
        _sink = sink;
        _level = level;
        _area = area;
    }
    
    /// <summary>
    /// Checks if this logger can be used.
    /// </summary>
    public bool IsValid => _sink != null;

    /// <summary>
    /// Logs an event.
    /// </summary>
    /// <param name="log">The message template.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Log(string log)
    {
        _sink.Log(_level, _area, log);
    }
}