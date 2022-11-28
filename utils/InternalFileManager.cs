using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace ModpackInstallerLauncher.utils
{
    /// <summary>
    /// This class implements a management util to interact with internal program files and paths.
    /// This is a singleton class.
    /// </summary>
    public class InternalFileManager
    {
        public static InternalFileManager INSTANCE = new InternalFileManager();
        public static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private readonly string _basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ConfigurationManager.AppSettings.Get("AppDataFolder"));

        private InternalFileManager()
        {
            this.EnsureDirectory(this._basePath);
        }
        
        /// <summary>
        /// Ensures that a given directory path exists.
        /// </summary>
        /// <param name="path">The path for the directory to ensure the existence of.</param>
        /// <returns>The path ensured, for convenience.</returns>
        private string EnsureDirectory(string path)
        {
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            return path;
        }
        
        /// <summary>
        /// Ensures that a given file path exists.
        /// </summary>
        /// <param name="path">The path for the directory to ensure the existence of.</param>
        /// <returns>The path ensured, for convenience.</returns>
        private string EnsureFile(string path)
        {
            if (!File.Exists(path))
            {
                Directory.CreateDirectory(Directory.GetParent(path)?.FullName ?? string.Empty);
                File.Create(path).Close();
            }
            return path;
        }

        /// <summary>
        /// Removes any files ending with a specified extension in the given directory.
        /// </summary>
        /// <param name="dirname">The directory to be cleaned (Relative path from the internal space)</param>
        /// <param name="blacklist">The list of file extensions to remove</param>
        public void CleanupDirectory(string dirname, params string[] blacklist)
        {
            string directoryPath = this.QueryDirectoryPath(dirname);
            if (directoryPath == null) return;

            foreach (string file in Directory.GetFiles(directoryPath))
            {
                if (blacklist.Contains(Path.GetExtension(file)) || blacklist[0] == "*")
                {
                    File.Delete(this.QueryFilePath(file));
                    Logging.INSTANCE.LogFile($@"Deleted file '{file}' from {directoryPath}" , "INTERNAL");
                }
            }
        }

        /// <summary>
        /// Sorts an extracted modpack in a given path, into .minecraft.
        /// Any directories present inside the path will be moved to the .minecraft/ directory, replacing the
        /// existing ones.
        /// Any files present inside the path will be moved to the .minecraft/mods/ directory.
        /// </summary>
        /// <param name="path">The path to sort the contents from</param>
        public void SortExtractedModpack(string path)
        {
            foreach (string directory in Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly))
            {
                string targetDirectory = Path.Combine(AppData, ".minecraft", Path.GetFileName(directory));
                if (Directory.Exists(targetDirectory)) Directory.Delete(targetDirectory, true);
                Directory.Move(directory, targetDirectory);
                Logging.INSTANCE.LogFile($@"Moved directory '{directory}' to '{targetDirectory}'", "INTERNAL");
            }
            
            foreach (string file in Directory.GetFiles(path, "*", SearchOption.TopDirectoryOnly))
            {
                string targetFile = Path.Combine(AppData, ".minecraft", "mods", Path.GetFileName(file));
                File.Move(file, targetFile);
                Logging.INSTANCE.LogFile($@"Moved file '{file}' to '{targetFile}'", "INTERNAL");
            }
        }

        /// <summary>
        /// Returns the full path to a given directory in the internal file system.
        /// </summary>
        /// <param name="dirname">The name of the directory to return.</param>
        /// <param name="create">Whether to create the directory if it doesn't exist, or not.</param>
        /// <returns>The full path to the specified directory</returns>
        public string QueryDirectoryPath(string dirname, bool create = false)
        {
            if (create) return this.EnsureDirectory(Path.Combine(this._basePath, dirname));
            return Directory.GetDirectories(this._basePath, "*", SearchOption.AllDirectories).FirstOrDefault(x => x.Contains(dirname));
        }
        
        /// <summary>
        /// Returns the full path to a given file in the internal file system.
        /// </summary>
        /// <param name="filename">The name of the file to return.</param>
        /// <param name="create">Whether to create the directory if it doesn't exist, or not.</param>
        /// <returns>The full path to the specified directory</returns>
        public string QueryFilePath(string filename, bool create = false)
        {
            if (create) return this.EnsureFile(Path.Combine(this._basePath, filename));
            return Directory.GetFiles(this._basePath, "*", SearchOption.AllDirectories).FirstOrDefault(x => x.Contains(filename));
        }
        
    }
}