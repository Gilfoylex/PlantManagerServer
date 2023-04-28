
namespace XBase.Logging;

public interface ILogSink
{
    /// <summary>
    /// Checks if given log level and area is enabled.
    /// </summary>
    /// <param name="level">The log event level.</param>
    /// <param name="area">The log area.</param>
    /// <returns><see langword="true"/> if given log level is enabled.</returns>
    bool IsEnabled(LogLevel level, string area);

    /// <summary>
    /// Logs an event.
    /// </summary>
    /// <param name="level">The log event level.</param>
    /// <param name="area">The area that the event originates.</param>
    /// <param name="log">The message template.</param>
    void Log(
        LogLevel level,
        string area,
        string log);
}