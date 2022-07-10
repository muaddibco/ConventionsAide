using System;
using System.Collections;
using System.Collections.Generic;

namespace ConventionsAide.Core.Logging.DataObjects
{
    public class LogEvents : IEnumerable<KeyValuePair<string, object>>
    {
        List<KeyValuePair<string, object>> _properties = new List<KeyValuePair<string, object>>();

        public string Message { get; }

        public LogEvents(string message)
        {
            Message = message;
        }

        public IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            return _properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() { return GetEnumerator(); }

        public LogEvents WithProperty(string name, object value)
        {
            _properties.Add(new KeyValuePair<string, object>(name, value));
            return this;
        }

        public static Func<LogEvents, Exception, string> Formatter { get; } = (l, e) => l.Message;
    }
}
