using System;
using System.IO;
using System.Windows.Forms;

namespace ModpackInstallerLauncher.utils
{
    /// <summary>
    /// This class implements a convenient way to log information both on the logging
    /// field of the main form and on files inside of the logs folder.
    /// </summary>
    public class Logging
    {
        private RichTextBox _loggingField = new RichTextBox();
        private readonly string _sessionString;
        public static readonly Logging INSTANCE = new Logging();

        /// <summary>
        /// Private constructor to assert the class as a singleton.
        /// </summary>
        private Logging() => this._sessionString = this.GetFileFormattedDate();

        /// <summary>
        /// Binds the logger into a RichTextBox that acts as the console for the logging output.
        /// </summary>
        /// <param name="loggingField"></param>
        public void BindOutput(RichTextBox loggingField) => this._loggingField = loggingField;

            /// <summary>
        /// Logs the given message on the logging field of the main form, with a specific formatting.
        /// </summary>
        /// <param name="message">The message to log</param>
        /// <param name="level">The program execution level where the log came from.</param>
        public void Log(string message, string level = "MAIN")
        {
            this._loggingField.AppendText($@"[{level.ToUpper()}] {message}" + "\n");
            this._loggingField.ScrollToCaret();
        }

        /// <summary>
        /// Logs a message into a file with a specific formatting.
        /// </summary> 
        /// <param name="message">The message to log into the file</param>
        /// <param name="level">The program execution level where the log came from.</param>
        public void LogFile(string message, string level = "MAIN")
        {
            if (level == null) throw new ArgumentNullException(nameof(level));
            string filepath = InternalFileManager.INSTANCE.QueryFilePath($@"logs/{this._sessionString}.log", true);
            File.AppendAllLines(filepath, new [] { $@"[{this.GetFileFormattedDate()}] [{level.ToUpper()}] {message}" });
        }

        /// <summary>
        /// Logs a message into both a file and the logging field of the main form.
        /// </summary>
        /// <param name="message">The message to log into the file</param>
        /// <param name="level">The program execution level where the log came from.</param>
        public void LogAll(string message, string level = "MAIN")
        {
            this.LogFile(message, level);
            this.Log(message, level);
        }
        
        /// <summary>
        /// Returns the current date formatted into a string.
        /// <returns>A string formatted into a reading-friendly format</returns>
        /// </summary>
        private string GetFormattedDate() => DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
        
        /// <summary>
        /// Returns the current date formatted into a string.
        /// <returns>A string formatted into a filename friendly format</returns>
        /// </summary>
        private string GetFileFormattedDate() => this.GetFormattedDate().Replace("/", ".").Replace(":", "-").Replace(" ", "");

    }
}