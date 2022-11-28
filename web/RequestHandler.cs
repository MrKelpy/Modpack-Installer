using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using HtmlAgilityPack;
using ModpackInstallerLauncher.utils;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace ModpackInstallerLauncher.web
{
    /// <summary>
    /// This class implements a bunch of useful methods related to
    /// sending and receiving requests through the internet.
    /// </summary>
    public static class RequestHandler
    {
        private static readonly HttpClient ClientInstance = new HttpClient();

        /// <summary>
        /// This method sends a GET request to the specified URL and tries to parse out the modpacks from it.
        /// This should only be used for the modpacks page.
        /// </summary>
        /// <param name="url">The GitHub Gist link for the modpack list.</param>
        /// <returns>A list of the Modpack objects to be used.</returns>
        public static async Task<List<Modpack>> GetModpacksAsync(string url)
        {
            try
            {
                Logging.INSTANCE.LogAll("Requesting modpack list from the Gist...", "DOWNLOADER");
                HtmlDocument doc = new HtmlDocument();
                string data = await ClientInstance.GetStringAsync(url);
                doc.LoadHtml(data);

                Logging.INSTANCE.LogAll("Parsing modpack list...", "DOWNLOADER");
                // Fetch all lines in the line list that don't start with "//" and aren't empty.
                List<HtmlNode> gistLines = doc.DocumentNode.Descendants("tr")
                    .Where(x => x.InnerText.Trim() != "" && !x.InnerText.Trim().StartsWith("//")).ToList();

                // Transform the nodes into a list of strings, containing all the modpacks.
                List<string> modpacks = String.Join("", gistLines.Select(x => x.InnerText.Trim()).ToList()).Split(';')
                    .ToList();

                // Format them into Modpack objects.
                return modpacks.Where(x => x.Split('#').Length == 3).Select(x => new Modpack(x.Split('#'))).ToList();
            }
            catch (Exception e)
            {
                Logging.INSTANCE.LogAll("Failed to fetch modpack list from the Gist.", "DOWNLOADER");
                Logging.INSTANCE.Log("Is your WiFi on?", "DOWNLOADER");
                Logging.INSTANCE.LogAll(e.Message, "DOWNLOADER");
                return new List<Modpack>();
            }
        }

        /// <summary>
        /// Downloads a file from the specified URL and saves it to the specified path, under
        /// the name of the url after formatting.
        /// </summary>
        /// <param name="url">The URL to download the file from</param>
        /// <param name="directory">The directory to download the files into</param>
        /// <param name="loading">The ProgressBar to update with the download progress</param> 
        public static async Task DownloadModpack(string url, string directory, ProgressBar loading)
        {
            string filename = RequestHandler.FormatUrlToFilename(url) + ".download";
            string parentDirectory = InternalFileManager.INSTANCE.QueryDirectoryPath(directory, true);
            loading.Value = 5; // Aesthetics
            
            // Request the file size and the content in a stream, and a FileStream to write to.
            Logging.INSTANCE.LogAll("Requesting file size...", "DOWNLOADER");
            using (HttpResponseMessage response = await ClientInstance.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))
            using (Stream contentStream = await ClientInstance.GetStreamAsync(url))
            using (FileStream fileStream = File.Create(Path.Combine(parentDirectory, filename)))
            {
                byte[] buffer = new byte[(int) Math.Pow(2, 17)];  // Sets the buffer (Amount of bytes read per iteration)
                
                // Request the file size in bytes
                long? contentSize = response.Content.Headers.ContentLength;
                Logging.INSTANCE.LogAll("File size: " + contentSize + " bytes", "DOWNLOADER");
                Logging.INSTANCE.LogAll("Downloading...", "DOWNLOADER");
                while (true)
                {
                    // Reads a chunk of bytes from the contentStream
                    var byteChunk = await contentStream.ReadAsync(buffer, 0, buffer.Length);
                    
                    // Write the chunk to the file and update the loading bar
                    await fileStream.WriteAsync(buffer, 0, byteChunk);
                    int percentageDownloaded = (int)(fileStream.Length / (double) contentSize * 100.0D);
                    loading.Value = percentageDownloaded > 5 ? percentageDownloaded : 5;
                    loading.Text = $@"Downloading... {loading.Value}%";
                    
                    if (byteChunk == 0) break; // If the chunk is empty, break.
                }
            }
            
            // Change the file extension to a zip, and reset the loading bar.
            File.Move(Path.Combine(parentDirectory, filename), Path.Combine(parentDirectory, filename.Replace(".download", ".zip")));
        }
        
        /// <summary>
        /// Formats the specified URL into a string to be used as a file name.
        /// </summary>
        /// <param name="url">The URL to format</param>
        /// <returns>A filename compatible string</returns>
        public static string FormatUrlToFilename(string url)
        {
            char[] separators = {'#', '$', '%', '&', '/', '(', ')', '=', '}', ']', '[', '{', '€', '§', '£', '@', '|', '\\', '?', '*', '+', '~', '^', '°', '<', '>', ';', ':', ','};
            string[] splitString = url.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return String.Join(".", splitString);
        }
    }
}