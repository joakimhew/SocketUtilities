using System;
using System.IO;
using System.Text;

namespace SocketUtilities.Core
{
    public sealed class FileLogger : ILogger
    {
        private readonly Encoding _encoding;
        private readonly string _rootLogFolder;

        public FileLogger(string rootLogFolder)
            : this(Encoding.UTF8, rootLogFolder)
        {
        }

        public FileLogger(Encoding encoding, string rootLogFolder)
        {
            _encoding = encoding;
            _rootLogFolder = rootLogFolder;
        }

        public void Debug(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());

            WriteToFile("debug.log", logFormat.Serialize());
        }

        public void Info(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("info.log", logFormat.Serialize());
        }

        public void Warn(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("warn.log", logFormat.Serialize());

        }

        public void Error(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("error.log", logFormat.Serialize());
        }

        public void Fatal(string message, bool includeStackTrace = false, string memberName = "",
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("fatal.log", logFormat.Serialize());
        }

        public void Info(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = exception.Message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("info.log", logFormat.Serialize());
        }

        public void Debug(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = exception.Message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("debug.log", logFormat.Serialize());
        }

        public void Warn(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = exception.Message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("warn.log", logFormat.Serialize());
        }

        public void Error(Exception exception, bool includeStackTrace = false, string memberName = "",
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = exception.Message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("error.log", logFormat.Serialize());
        }

        public void Fatal(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = exception.Message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

#if DEBUG
            System.Diagnostics.Debug.WriteLine(logFormat.Serialize());
#endif

            WriteToFile("fatal.log", logFormat.Serialize());
        }


        private void WriteToFile(string fileName, string output)
        {
            var filePath = $"{_rootLogFolder}\\{fileName}";

            if (!File.Exists(filePath))
            {
                using (var f = File.Create(filePath))
                {
                    var bytes = _encoding.GetBytes(output);
                    var newLineBytes = _encoding.GetBytes(Environment.NewLine);

                    f.Write(bytes, 0, bytes.Length);

                    //Print two new lines
                    f.Write(newLineBytes, 0, newLineBytes.Length);
                    f.Write(newLineBytes, 0, newLineBytes.Length);

                    f.Flush();
                }
            }

            else
            {
                using (var f = File.OpenWrite(filePath))
                {
                    f.Position = f.Length;
                    var bytes = _encoding.GetBytes(output);
                    var newLineBytes = _encoding.GetBytes(Environment.NewLine);

                    f.Write(bytes, 0, bytes.Length);

                    //Print two new lines
                    f.Write(newLineBytes, 0, newLineBytes.Length);
                    f.Write(newLineBytes, 0, newLineBytes.Length);

                    f.Flush();
                }
            }
        }
    }
}