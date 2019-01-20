using System;
using System.IO;

namespace KirLogger
{
    public class Logger
    {
        private static int _specialId = 1;

        private static int _idSequence;
        private static int _datetimeSequence;
        private static int _levelSequence;
        private static int _messageSequence;

        private static string _filePath;
        private static TextWriter _writer;

        private static readonly string[] Sequence = new string[4];

        public static int IdSequence
        {
            get => _idSequence;
            set
            {
                ChangeSequence(_idSequence, value);
                _idSequence = value;
            }
        }

        public static int DatetimeSequence
        {
            get => _datetimeSequence;
            set
            {
                ChangeSequence(_datetimeSequence, value);
                _datetimeSequence = value;
            }
        }

        public static int LevelSequence
        {
            get => _levelSequence;
            set
            {
                ChangeSequence(_levelSequence, value);
                _levelSequence = value;
            }
        }

        public static int MessageSequence
        {
            get => _messageSequence;
            set
            {
                ChangeSequence(_messageSequence, value);
                _messageSequence = value;
            }
        }

        /// <summary>
        /// Restores the sequence.
        /// </summary>
        public static void Restore()
        {
            _idSequence = 0;
            _datetimeSequence = 1;
            _levelSequence = 2;
            _messageSequence = 3;
        }

        static Logger() => Restore();

        
        /// <summary>
        /// Obligatory in  the end. Closes the file stream used for writing down logs.
        /// </summary>
        public static void Close() => _writer.Close();

        private static void ChangeSequence(int beforePosition, int afterPosition)
        {
            CheckOutOfRangeException(afterPosition);
            if (_idSequence == afterPosition)
                _idSequence = beforePosition;
            else if (_levelSequence == afterPosition)
                _levelSequence = beforePosition;
            else if (_messageSequence == afterPosition)
                _messageSequence = beforePosition;
            else if (_datetimeSequence == afterPosition) _datetimeSequence = beforePosition;
        }

        private static void CheckOutOfRangeException(int value)
        {
            if (value > 3 || value < 0) throw new Exception("Out of range exception. Value >= 0 && Value < 4");
        }

        /// <summary>
        /// Obligatory first step .Creates a *.log file for the future logs.
        /// </summary>
        /// <param name="filePath">Sets the file path; e.g. /Users/Folder</param>
        /// <param name="fileName">Sets the file name; e.g. fileLog (lib will automatically add *.log extension)</param>
        public static void FileSetup(string filePath, string fileName)
        {
            _filePath = filePath + @"/" + fileName + ".log";
            _writer = new StreamWriter(File.Create(_filePath));
        }

        /// <summary>
        /// Sets the custom sequence of how the each type will write down in file.
        /// </summary>
        /// <param name="idSequence">Sets the ID sequence.</param>
        /// <param name="timeStampSequence">Sets the datetime sequence.</param>
        /// <param name="levelSequence">Sets the level sequence.</param>
        /// <param name="messageSequence">Sets the message sequence.</param>
        public static void SequenceSetup(int idSequence, int timeStampSequence, int levelSequence, int messageSequence)
        {
            IdSequence = idSequence;
            DatetimeSequence = timeStampSequence;
            LevelSequence = levelSequence;
            MessageSequence = messageSequence;
        }

        /// <summary>
        /// All levels including custom levels.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void ALL(string message) => InLog(message, "ALL");

        /// <summary>
        /// Designates fine-grained informational events that are most useful to debug an application.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void DEBUG(string message) => InLog(message, "DEBUG");

        /// <summary>
        /// Designates informational messages that highlight the progress of the application at coarse-grained level.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void INFO(string message) => InLog(message, "INFO");

        /// <summary>
        /// Designates potentially harmful situations.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void WARN(string message) => InLog(message, "WARN");

        /// <summary>
        /// Designates error events that might still allow the application to continue running.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void ERROR(string message) => InLog(message, "ERROR");

        /// <summary>
        /// Designates very severe error events that will presumably lead the application to abort.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void FATAL(string message) => InLog(message, "FATAL");

        /// <summary>
        /// The highest possible rank and is intended to turn off logging.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void OFF(string message) => InLog(message, "OFF");

        /// <summary>
        /// Designates finer-grained informational events than the DEBUG.
        /// </summary>
        /// <param name="message">Sets the message in log file.</param>
        public static void TRACE(string message) => InLog(message, "ALL");

        private static void InLog(string message, string level)
        {
            Sequence[_idSequence] = _specialId++.ToString();
            Sequence[_datetimeSequence] = DateTime.Now.ToString();
            Sequence[_levelSequence] = level;
            Sequence[_messageSequence] = message;
            string data = string.Empty;
            foreach (var s in Sequence) data += s + "    ";
            _writer.WriteLine(data);
            _writer.Flush();
        }
    }
}