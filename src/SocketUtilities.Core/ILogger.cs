using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace SocketUtilities.Core
{
    public interface ILogger
    {
        /// <summary>
        /// The debug log level should be used in debugging purposes.
        /// </summary>
        /// <param name="message">Custom message for log</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Debug(string message, bool includeStackTrace = false, [CallerMemberName]string memberName = "",
             [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// The Info log level should be used in for general purpose logging.
        /// </summary>
        /// <param name="message">Custom message for log</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Info(string message, bool includeStackTrace = false, [CallerMemberName]string memberName = "", 
            [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// The Warn log level should be used in warning purposes where the application lifecycle is not affected.
        /// </summary>
        /// <param name="message">Custom message for log</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Warn(string message, bool includeStackTrace = false, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// The Error log level should be used in warning purposes where the application lifecycle is not nescessary affected.
        /// </summary>
        /// <param name="message">Custom message for log</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Error(string message, bool includeStackTrace = false, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// The Fatal log level should be used in warning purposes where the application lifecycle is affected.
        /// </summary>
        /// <param name="message">Custom message for log</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Fatal(string message, bool includeStackTrace = false, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);


        /// <summary>
        /// The debug log level should be used in debugging purposes.
        /// </summary>
        /// <param name="exception">The exception that called the logger</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Debug(Exception exception, bool includeStackTrace = false, [CallerMemberName]string memberName = "",
            [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// The Info log level should be used in for general purpose logging.
        /// </summary>
        /// <param name="exception">The exception that called the logger</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Info(Exception exception, bool includeStackTrace = false, [CallerMemberName]string memberName = "",
            [CallerFilePath]string sourceFilePath = "", [CallerLineNumber]int sourceLineNumber = 0);

        /// <summary>
        /// The Warn log level should be used in warning purposes where the application lifecycle is not affected.
        /// </summary>
        /// <param name="exception">The exception that called the logger</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Warn(Exception exception, bool includeStackTrace = false, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// The Error log level should be wheen the application lifecycle can be affected.
        /// </summary>
        /// <param name="exception">The exception that called the logger</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Error(Exception exception, bool includeStackTrace = false, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);

        /// <summary>
        /// The Fatal log level should be used when the application lifecycle is affected.
        /// </summary>
        /// <param name="exception">The exception that called the logger</param>
        /// <param name="includeStackTrace">Define if the stack trace should be included in the log</param>
        /// <param name="memberName">The member name of the caller.</param>
        /// <param name="sourceFilePath">The full path to the source file where the logger is called</param>
        /// <param name="sourceLineNumber">The line number where the loggers is called</param>
        void Fatal(Exception exception, bool includeStackTrace = false, [CallerMemberName] string memberName = "",
            [CallerFilePath] string sourceFilePath = "", [CallerLineNumber] int sourceLineNumber = 0);
    }

    internal class LogFormat
    {
        public LogFormat([CallerMemberName]string memberName = "")
        {
            LogType = memberName;
        }

        [JsonProperty(PropertyName = "Log type")]
        public string LogType { get; set; }

        [JsonProperty(PropertyName = "Message")]
        public string Message { get; set; }

        [JsonProperty(PropertyName = "Member name")]
        public string MemberName { get; set; }

        [JsonProperty(PropertyName = "Souce file path")]
        public string SourceFilePath { get; set; }

        [JsonProperty(PropertyName = "Source line number")]
        public int SourceLineNumber { get; set; }

        [JsonProperty(PropertyName = "Stack trace")]
        public string StackTrace { get; set; }

        [JsonProperty(PropertyName = "Log time")]
        public string LogTime => DateTime.Now.ToString(CultureInfo.CurrentCulture);

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public LogFormat Deserialize(string json)
        {
            return JsonConvert.DeserializeObject<LogFormat>(json);
        }
    }
}