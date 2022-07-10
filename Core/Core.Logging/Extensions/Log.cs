using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ConventionsAide.Core.Logging.Extensions
{
    public static class Log
    {
        /// <summary>
        /// Short way to wrap event log argument in a name and value format
        /// </summary>
        /// <param name="name">Name of the field</param>
        /// <param name="value">The Value to log</param>
        /// <returns>Structed argument in a KeyValuePair</returns>
        public static KeyValuePair<string, object> Args(string name, object value)
        {
            return new KeyValuePair<string, object>(name, value);
        }

        /// <summary>
        /// Short way to wrap event log argument in a name and value format
        /// And associate it with the log only if the value is not null
        /// </summary>
        /// <param name="name">Name of the field</param>
        /// <param name="value">The Value to log</param>
        /// <returns>Structed argument in a KeyValuePair</returns>
        public static KeyValuePair<string, object> ArgsIfNotNull(string name, object value)
        {
            if (value == null)
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }

            return Args(name, value);
        }

        /// <summary>
        /// Short way to wrap event log argument in a name and value format
        /// And associate it with the log only if the string value is not null or empty
        /// </summary>
        /// <param name="name">Name of the field</param>
        /// <param name="value">The Value to log</param>
        /// <returns>Structed argument in a KeyValuePair</returns>
        public static KeyValuePair<string, object> ArgsIfNotNull(string name, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }

            return Args(name, value);
        }

        /// <summary>
        /// Short way to wrap event log argument by passing the object.
        /// The field name that will be used is the instance name of the passed object.
        /// <br/>
        /// For example:<br/>
        ///   * Args(()=> myObject); will be index in the json log as {"myObject":"value"};<br/>
        ///   * Args(()=> myObject.property); -> {"property":"property value"};<br/>
        ///   * Args(()=> myObject.ToLogFormat()); -> {"myObject":"result of ToLogFormat()"}.
        /// </summary>
        /// <param name="expression">The object to Log by the Instance name</param>
        /// <returns>Structed argument in a KeyValuePair</returns>
        public static KeyValuePair<string, object> Args(Expression<Func<object>> expression)
        {
            var defaultKeyValuePair = new KeyValuePair<string, object>(string.Empty, default);

            try
            {
                MemberExpression memberExpression = GetMemberExpression(expression);

                if (memberExpression == null)
                {
                    return defaultKeyValuePair;
                }

                var variableName = memberExpression.Member.Name;
                var func = expression.Compile();
                var variableValue = func();

                return new KeyValuePair<string, object>(variableName, variableValue);
            }
            catch (Exception)
            {
                return defaultKeyValuePair;
            }
        }

        /// <summary>
        /// Short way to wrap event log argument by passing the object.
        /// The field name that will be used is the instance name of the passed object.
        /// <br/>
        /// For example:<br/>
        ///   * Args(()=> myObject); will be index in the json log as {"myObject":"value"};<br/>
        ///   * Args(()=> myObject.property); -> {"property":"property value"};<br/>
        ///   * Args(()=> myObject.ToLogFormat()); -> {"myObject":"result of ToLogFormat()"}.
        /// </summary>
        /// <param name="expression">The object to Log by the Instance name</param>
        /// <returns>Structed argument in a KeyValuePair</returns>
        public static KeyValuePair<string, object> ArgsIfNotNull(Expression<Func<object>> expression)
        {
            var keyValuePair = Args(expression);
            if (keyValuePair.Value == null)
            {
                return new KeyValuePair<string, object>(string.Empty, null);
            }

            return keyValuePair;
        }

        private static MemberExpression GetMemberExpression(Expression<Func<object>> expression)
        {
            MemberExpression memberExpression = null;

            if (expression.Body is UnaryExpression unaryExpressionBody
                && unaryExpressionBody.Operand is MemberExpression)
            {
                memberExpression = (MemberExpression)unaryExpressionBody.Operand;
            }
            else if (expression.Body is MethodCallExpression methodCallExpression
                && methodCallExpression.Object is MemberExpression)
            {
                memberExpression = (MemberExpression)methodCallExpression.Object;
            }
            else if (expression.Body is MemberExpression)
            {
                memberExpression = (MemberExpression)expression.Body;
            }

            return memberExpression;
        }

        /// <summary>
        /// Adds the member ID to the logging context.
        /// </summary>
        public static KeyValuePair<string, object> MemberId(int? memberId)
        {
            return new KeyValuePair<string, object>("memberId", memberId);
        }
    }
}
