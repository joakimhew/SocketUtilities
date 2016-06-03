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
            if (!IsDebugActivated)
                return;

            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

            var serialized = logFormat.Serialize();
#if DEBUG
            System.Diagnostics.Debug.WriteLine(serialized);
#endif

            WriteToFile("debug.log", serialized);
        }

        public void Info(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            if (!IsInfoActivated)
                return;

            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

            var serialized = logFormat.Serialize();
#if DEBUG
            System.Diagnostics.Debug.WriteLine(serialized);
#endif

            WriteToFile("info.log", serialized);
        }

        public void Warn(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            if (!IsWarnActivated)
                return;

            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

            var serialized = logFormat.Serialize();
#if DEBUG
            System.Diagnostics.Debug.WriteLine(serialized);
#endif

            WriteToFile("warn.log", serialized);

        }

        public void Error(string message, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            if (!IsErrorActivated)
                return;

            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

            var serialized = logFormat.Serialize();
#if DEBUG
            System.Diagnostics.Debug.WriteLine(serialized);
#endif

            WriteToFile("error.log", serialized);
        }

        public void Fatal(string message, bool includeStackTrace = false, string memberName = "",
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            if(!IsFatalActivated)
                return;

            var logFormat = new LogFormat
            {
                MemberName = memberName,
                Message = message,
                SourceFilePath = sourceFilePath,
                SourceLineNumber = sourceLineNumber,
                StackTrace = includeStackTrace ? Environment.StackTrace : null
            };

            var serialized = logFormat.Serialize();
#if DEBUG
            System.Diagnostics.Debug.WriteLine(serialized);
#endif

            WriteToFile("fatal.log", serialized);
        }


        public void Debug(Exception exception, bool includeStackTrace = false, string memberName = "",
           string sourceFilePath = "", int sourceLineNumber = 0)
        {
            Debug(exception.Message, includeStackTrace, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Info(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            Info(exception.Message, includeStackTrace, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Warn(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            Warn(exception.Message, includeStackTrace, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Error(Exception exception, bool includeStackTrace = false, string memberName = "",
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            Error(exception.Message, includeStackTrace, memberName, sourceFilePath, sourceLineNumber);
        }

        public void Fatal(Exception exception, bool includeStackTrace = false, string memberName = "", 
            string sourceFilePath = "", int sourceLineNumber = 0)
        {
            Fatal(exception.Message, includeStackTrace, memberName, sourceFilePath, sourceLineNumber);
        }

        public bool IsDebugActivated { get; set; } = true;
        public bool IsInfoActivated { get; set; } = true;
        public bool IsWarnActivated { get; set; } = true;
        public bool IsErrorActivated { get; set; } = true;
        public bool IsFatalActivated { get; set; } = true;


        private void WriteToFile(string fileName, string output)
        {
            if (!Directory.Exists(_rootLogFolder))
                throw new DirectoryNotFoundException("The directory provided for the FileLogger does not exist!");

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