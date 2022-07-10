using System;
using System.Collections.Generic;
using System.Linq;
using ConventionsAide.Core.Logging.DataObjects;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.Logging.Extensions
{
    public static class MicrosoftLoggerExtention
    {
        public static void Log(this ILogger logger, LogLevel logLevel, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, logLevel, title, message, eventData);
        }

        public static void Log(this ILogger logger, LogLevel logLevel, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, logLevel, message, eventData);
        }

        public static void Log(this ILogger logger, LogLevel logLevel, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, logLevel, title, message, exception, eventData);
        }

        public static void Log(this ILogger logger, LogLevel logLevel, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, logLevel, message, exception, eventData);
        }

        public static void Trace(this ILogger logger, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Trace, title, message, eventData);
        }

        public static void Trace(this ILogger logger, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Trace, message, eventData);
        }

        public static void Trace(this ILogger logger, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Trace, title, message, exception, eventData);
        }

        public static void Trace(this ILogger logger, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Trace, message, exception, eventData);
        }

        public static void Debug(this ILogger logger, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Debug, title, message, eventData);
        }

        public static void Debug(this ILogger logger, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Debug, message, eventData);
        }

        public static void Debug(this ILogger logger, Func<string> messageFactory, params KeyValuePair<string, object>[] eventData)
        {
            if (logger.IsEnabled(LogLevel.Debug))
            {
                string message = messageFactory?.Invoke();
                WriteLog(logger, LogLevel.Debug, message, eventData);
            }
        }

        public static void Debug(this ILogger logger, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Debug, title, message, exception, eventData);
        }

        public static void Debug(this ILogger logger, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Debug, message, exception, eventData);
        }

        public static void Info(this ILogger logger, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Information, title, message, eventData);
        }

        public static void Info(this ILogger logger, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Information, message, eventData);
        }

        public static void Info(this ILogger logger, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Information, title, message, exception, eventData);
        }

        public static void Info(this ILogger logger, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Information, message, exception, eventData);
        }

        public static void Warning(this ILogger logger, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Warning, title, message, eventData);
        }

        public static void Warning(this ILogger logger, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Warning, message, eventData);
        }

        public static void Warning(this ILogger logger, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Warning, title, message, exception, eventData);
        }

        public static void Warning(this ILogger logger, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Warning, message, exception, eventData);
        }

        public static void Error(this ILogger logger, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Error, title, message, eventData);
        }

        public static void Error(this ILogger logger, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Error, message, eventData);
        }

        public static void Error(this ILogger logger, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Error, title, message, exception, eventData);
        }

        public static void Error(this ILogger logger, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Error, message, exception, eventData);
        }

        public static void Critical(this ILogger logger, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Critical, title, message, eventData);
        }

        public static void Critical(this ILogger logger, string message, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Critical, message, eventData);
        }

        public static void Critical(this ILogger logger, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Critical, title, message, exception, eventData);
        }

        public static void Critical(this ILogger logger, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            WriteLog(logger, LogLevel.Critical, message, exception, eventData);
        }

        public static IDisposable TimeOperation<T>(this ILogger<T> logger, string message, params object?[] args)
        {
            return new TimeLogOperations<T>(logger, LogLevel.Debug, message, args);
        }

        private static void WriteLog(ILogger logger, LogLevel logLevel, string title, string message, params KeyValuePair<string, object>[] eventData)
        {
            var logEvent = BuildLogEvent(message, eventData);
            logEvent.WithProperty("title", title);
            logger?.Log(logLevel, default, logEvent, null, LogEvents.Formatter);
        }

        private static void WriteLog(ILogger logger, LogLevel logLevel, string message, params KeyValuePair<string, object>[] eventData)
        {
            var logEvent = BuildLogEvent(message, eventData);
            logger?.Log(logLevel, default, logEvent, null, LogEvents.Formatter);
        }

        private static void WriteLog(ILogger logger, LogLevel logLevel, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            var logEvent = BuildLogEvent(message, eventData);
            logger?.Log(logLevel, default, logEvent, exception, LogEvents.Formatter);
        }

        private static void WriteLog(ILogger logger, LogLevel logLevel, string title, string message, Exception exception, params KeyValuePair<string, object>[] eventData)
        {
            var logEvent = BuildLogEvent(message, eventData);
            logEvent.WithProperty("title", title);
            logger?.Log(logLevel, default, logEvent, exception, LogEvents.Formatter);
        }

        private static LogEvents BuildLogEvent(string message, params KeyValuePair<string, object>[] eventData)
        {
            Dictionary<string, object> context = eventData
                .Where(e => !string.IsNullOrEmpty(e.Key))
                .GroupBy(e => e.Key)                                // to handle duplicates
                .ToDictionary(g => g.Key, g => g.First().Value);    // takes first value for key

            var logEvent = new LogEvents(message);
            logEvent.WithProperty("context", context);

            return logEvent;
        }
    }
}
