#nullable enable
using System;
using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace ConventionsAide.Core.Logging.Extensions
{
    public class TimeLogOperations<T>:IDisposable
    {
        readonly ILogger<T> _logger;
        readonly LogLevel _level;
        readonly string _message;
        readonly object?[] _args;
        readonly Stopwatch _stopwatch;


        public TimeLogOperations(ILogger<T> logger, LogLevel level, string message, object?[] args)
        {
            _logger = logger;
            _level = level;
            _message = message;
            _args = args;
            _stopwatch = Stopwatch.StartNew();
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _logger.Log(_level, $"{_message} completed in {_stopwatch.ElapsedMilliseconds}ms", _args);
        }
    }
}