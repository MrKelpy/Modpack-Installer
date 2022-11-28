using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using ModpackInstallerLauncher.utils;
using ModpackInstallerLauncher.web;

namespace ModpackInstallerLauncher.gui
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public partial class Main : Form
    {
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private List<Modpack> _modpacks;
        private int page;

        public Main()
        {
            InitializeComponent();
            this.CenterToParent(); 
            Logging.INSTANCE.BindOutput(RichTextConsole);
            
            // Deletes the oh-so problematic tmp folder if it exists.
            string tmpPath = InternalFileManager.INSTANCE.QueryDirectoryPath("tmp");
            if (Directory.Exists(tmpPath)) Directory.Delete(tmpPath, true);
        }

        /// <summary>
        /// This external library method sends a specific message into a given window.
        /// A "Message" can be understood as a command or some sort of data that is passed into the window
        /// with a purpose.
        /// In this case, this method will be used to tell the window it is being grabbed by the mouse.
        /// </summary>
        /// <param name="hWnd">The window to send the message to</param>
        /// <param name="msg">The message to be sent</param>
        /// <param name="wParam">Additional message-specific info</param>
        /// <param name="lParam">Additional message-specific info</param>
        /// <returns>The result of the message processing through the window.</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        
        /// <summary>
        /// This external library method removes the mouse capture from the currently focused window.
        /// </summary>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
        public static extern bool ReleaseCapture();
        
        /// <summary>
        /// Update the list of modpacks when the form loads
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void Main_Load(object sender, EventArgs e)
        {
            this._modpacks = await RequestHandler.GetModpacksAsync(ConfigurationManager.AppSettings.Get("ModpacksUrl"));
            Logging.INSTANCE.LogAll($@"Loaded {this._modpacks.Count} modpacks.");
            this.UpdatePage(1);
            this.LabelPage.Visible = true;
            this.LabelDescription.Visible = true;
            this.LabelLink.Visible = true;
        }
        
        private void ButtonNext_Click(object sender, EventArgs e)
        {
            // Add 1 to the page number counter, or loop around to page 1.
            this.page += 1;
            this.UpdatePage(page);
        }

        
        private void ButtonPrevious_Click(object sender, EventArgs e)
        {
            // Take 1 from the page number counter, or loop around to the last page.
            this.page -= 1;
            this.UpdatePage(this.page);
        }
        
        /// <summary>
        /// Exits the program, closing the window.
        /// </summary>
        private void ButtonClose_Click(object sender, EventArgs e) => Application.Exit();
        
        /// <summary>
        /// Minimises the current window.
        /// </summary>
        private void ButtonMinimise_Click(object sender, EventArgs e) => this.WindowState = FormWindowState.Minimized;

        /// <summary>
        /// Allows the user to move the window by dragging the custom title bar.
        /// </summary>
        private void PanelTitleBar_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(this.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
        }

        /// <summary>
        /// Updates the page information displays and the page number.
        /// </summary>
        /// <param name="newIndex">The new page index</param>
        private void UpdatePage(int newIndex)
        {
            // Fixes the page index in case it goes out of bounds
            int index = newIndex > this._modpacks.Count ? 1 : 
                newIndex <= 0 ? this._modpacks.Count : newIndex;  // <--- Else
            
            // Needs to be up here just so if the wifi fails it still fixes the buttons on reload.
            this.ButtonNext.Visible = this.ButtonPrevious.Visible = this.ButtonInstall.Visible = true;

            // Handles the edge case where there were no modpacks available.
            if (this._modpacks.Count == 0)
            {
                this.LabelPage.Text = @"Page 0 of 0";
                this.LabelDescription.Text = this.LabelLink.Text = string.Empty;
                this.LabelModpackDisplay.Text = @"No modpacks found.";
                return;
            }

            // Sets the correct information on all the visuals in the current page.
            this.page = index;
            this.LabelDescription.Text = this._modpacks[index - 1].Description;
            this.LabelLink.Text = this._modpacks[index - 1].Url;
            this.LabelModpackDisplay.Text = this._modpacks[index - 1].Name;
            this.LabelPage.Text = $@"Page {index} of {this._modpacks.Count}";
            ProgressBarInstallation.Value = 0;
            ProgressBarInstallation.Text = "";
            
            // Handles the Install Button naming based on the modpack availability in the modpacks folder.
            this.ButtonInstall.Text = @"Install";
            if (InternalFileManager.INSTANCE.QueryFilePath(RequestHandler.FormatUrlToFilename(this.LabelLink.Text.Trim()) + ".zip") != null)
                this.ButtonInstall.Text = @"Switch";
        }
        
        /// <summary>
        /// Downloads the selected modpack into the modpacks folder and extracts it into the .minecraft/mods
        /// folder.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void ButtonInstall_Click(object sender, EventArgs e)
        {
            try
            {
                this.ButtonNext.Visible = this.ButtonPrevious.Visible = this.ButtonInstall.Visible = false;

                // Handles the visual changes to the page if the modpack isn't in cache
                if (!this.ButtonInstall.Text.Equals("Switch"))
                {
                    Logging.INSTANCE.LogAll("Preparing modpacks cache folder for download...");
                    InternalFileManager.INSTANCE.CleanupDirectory("modpacks", ".download");

                    // Downloads the modpack into the modpacks folder, to be stored for later use
                    Logging.INSTANCE.LogAll($@"Preparing to download '{this.LabelModpackDisplay.Text}'...");
                    await RequestHandler.DownloadModpack(this._modpacks[this.page - 1].Url, "modpacks",
                        ProgressBarInstallation);
                }

                ProgressBarInstallation.Value = 100; // Aesthetics

                // Clears the .minecraft/mods folder
                Logging.INSTANCE.LogAll("Clearing up the .minecraft/mods folder...");
                string modsFolder = Path.Combine(InternalFileManager.AppData, ".minecraft", "mods");
                if (Directory.Exists(modsFolder)) Directory.Delete(modsFolder, true);
                Directory.CreateDirectory(modsFolder);

                // Extracts the modpack into the .minecraft folder, replacing directories and mods. See 
                // InternalFileManager#SortExtractedModpack for more information.
                Logging.INSTANCE.LogAll("Extracting modpack...");
                string filename = RequestHandler.FormatUrlToFilename(this.LabelLink.Text.Trim()) + ".zip";

                await Task.Run(() => ZipFile.ExtractToDirectory(InternalFileManager.INSTANCE.QueryFilePath(filename),
                    InternalFileManager.INSTANCE.QueryDirectoryPath("tmp", true)));
                InternalFileManager.INSTANCE.SortExtractedModpack(
                    InternalFileManager.INSTANCE.QueryDirectoryPath("tmp"));
                Directory.Delete(InternalFileManager.INSTANCE.QueryDirectoryPath("tmp"));

                Logging.INSTANCE.LogAll($@"Using the '{this.LabelModpackDisplay.Text}' modpack!");
                this.UpdatePage(this.page); // Updates the current page
            }
            catch (InvalidOperationException exception)
            {
                Logging.INSTANCE.LogAll("An error occurred while installing the modpack: " + exception.Message);
                Logging.INSTANCE.Log("This usually happens when there's an issue with the download link.");
                Logging.INSTANCE.LogFile(exception.StackTrace);
            }
            catch (Exception exception)
            {
                Logging.INSTANCE.LogAll("An error occurred while installing the modpack: " + exception.Message);
                Logging.INSTANCE.LogFile(exception.StackTrace);
            }
            finally
            {
                InternalFileManager.INSTANCE.CleanupDirectory("modpacks", ".download");
                this.UpdatePage(this.page);
            }
        }

        /// <summary>
        /// Gets the list of modpacks from the github again, and updates the modpack list.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void ButtonReload_Click(object sender, EventArgs e)
        {
            // Blocks reloading if there's an ongoing process.
            if (!ButtonInstall.Visible)
            {
                RichTextConsole.AppendText("Cannot reload at this time." + "\n");
                return;
            }

            // Handles the visual alterations to the buttons and labels
            this.LabelModpackDisplay.Text = @"Reloading...";
            this.ButtonNext.Visible = this.ButtonPrevious.Visible = false;
            this.LabelDescription.Text = this.LabelLink.Text = this.LabelPage.Text = string.Empty;
            
            // Requests the list of modpacks and updates the page
            Logging.INSTANCE.LogAll("Reloading modpacks...");
            this._modpacks = await RequestHandler.GetModpacksAsync(ConfigurationManager.AppSettings.Get("ModpacksUrl"));
            Logging.INSTANCE.LogAll($@"Loaded {this._modpacks.Count} modpacks.");
            this.UpdatePage(1);
        }
        
        /// <summary>
        /// Clears the console.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonClearConsole_Click(object sender, EventArgs e) => RichTextConsole.Text = string.Empty;

        /// <summary>
        /// Opens the link written in the label in the default browser.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void LabelLink_Click(object sender, EventArgs e) => System.Diagnostics.Process.Start(LabelLink.Text);

        /// <summary>
        /// Deletes the cached modpacks over at the modpacks folder.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void DeleteCache_Click(object sender, EventArgs e)
        {
            // Blocks cache deletion if there's an ongoing process.
            if (!ButtonInstall.Visible)
            {
                RichTextConsole.AppendText("Cannot delete cache at this time." + "\n");
                return;
            }
            
            InternalFileManager.INSTANCE.CleanupDirectory("modpacks", "*");
            Logging.INSTANCE.LogAll("Deleted cached modpacks.");
            this.UpdatePage(this.page);
        }
    }
}