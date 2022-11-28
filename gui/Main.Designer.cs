using System.ComponentModel;

namespace ModpackInstallerLauncher.gui
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.layout = new System.Windows.Forms.Panel();
            this.DeleteCache = new System.Windows.Forms.Button();
            this.ButtonReload = new System.Windows.Forms.Button();
            this.ButtonClearConsole = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ButtonInstall = new System.Windows.Forms.Button();
            this.ProgressBarInstallation = new System.Windows.Forms.ProgressBar();
            this.LabelPage = new System.Windows.Forms.Label();
            this.LabelLink = new System.Windows.Forms.Label();
            this.LabelDescription = new System.Windows.Forms.Label();
            this.LabelModpackDisplay = new System.Windows.Forms.Label();
            this.ButtonPrevious = new System.Windows.Forms.Button();
            this.ButtonNext = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.RichTextConsole = new System.Windows.Forms.RichTextBox();
            this.PanelTitleBar = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.ButtonMinimise = new System.Windows.Forms.Button();
            this.ButtonClose = new System.Windows.Forms.Button();
            this.layout.SuspendLayout();
            this.panel2.SuspendLayout();
            this.PanelTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // layout
            // 
            this.layout.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(51)))));
            this.layout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.layout.Controls.Add(this.DeleteCache);
            this.layout.Controls.Add(this.ButtonReload);
            this.layout.Controls.Add(this.ButtonClearConsole);
            this.layout.Controls.Add(this.panel2);
            this.layout.Controls.Add(this.label2);
            this.layout.Controls.Add(this.panel1);
            this.layout.Controls.Add(this.RichTextConsole);
            this.layout.Controls.Add(this.PanelTitleBar);
            this.layout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layout.Location = new System.Drawing.Point(0, 0);
            this.layout.Name = "layout";
            this.layout.Size = new System.Drawing.Size(800, 450);
            this.layout.TabIndex = 0;
            // 
            // DeleteCache
            // 
            this.DeleteCache.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.DeleteCache.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.DeleteCache.FlatAppearance.BorderSize = 0;
            this.DeleteCache.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DeleteCache.ForeColor = System.Drawing.SystemColors.Control;
            this.DeleteCache.Location = new System.Drawing.Point(754, 138);
            this.DeleteCache.Name = "DeleteCache";
            this.DeleteCache.Size = new System.Drawing.Size(31, 31);
            this.DeleteCache.TabIndex = 12;
            this.DeleteCache.Text = "D";
            this.DeleteCache.UseVisualStyleBackColor = false;
            this.DeleteCache.Click += new System.EventHandler(this.DeleteCache_Click);
            // 
            // ButtonReload
            // 
            this.ButtonReload.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.ButtonReload.FlatAppearance.BorderSize = 0;
            this.ButtonReload.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonReload.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonReload.Location = new System.Drawing.Point(754, 64);
            this.ButtonReload.Name = "ButtonReload";
            this.ButtonReload.Size = new System.Drawing.Size(31, 31);
            this.ButtonReload.TabIndex = 11;
            this.ButtonReload.Text = "R";
            this.ButtonReload.UseVisualStyleBackColor = false;
            this.ButtonReload.Click += new System.EventHandler(this.ButtonReload_Click);
            // 
            // ButtonClearConsole
            // 
            this.ButtonClearConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.ButtonClearConsole.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ButtonClearConsole.FlatAppearance.BorderSize = 0;
            this.ButtonClearConsole.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClearConsole.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonClearConsole.Location = new System.Drawing.Point(754, 101);
            this.ButtonClearConsole.Name = "ButtonClearConsole";
            this.ButtonClearConsole.Size = new System.Drawing.Size(31, 31);
            this.ButtonClearConsole.TabIndex = 10;
            this.ButtonClearConsole.Text = "C";
            this.ButtonClearConsole.UseVisualStyleBackColor = false;
            this.ButtonClearConsole.Click += new System.EventHandler(this.ButtonClearConsole_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ButtonInstall);
            this.panel2.Controls.Add(this.ProgressBarInstallation);
            this.panel2.Controls.Add(this.LabelPage);
            this.panel2.Controls.Add(this.LabelLink);
            this.panel2.Controls.Add(this.LabelDescription);
            this.panel2.Controls.Add(this.LabelModpackDisplay);
            this.panel2.Controls.Add(this.ButtonPrevious);
            this.panel2.Controls.Add(this.ButtonNext);
            this.panel2.Location = new System.Drawing.Point(12, 62);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(428, 350);
            this.panel2.TabIndex = 6;
            // 
            // ButtonInstall
            // 
            this.ButtonInstall.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.ButtonInstall.FlatAppearance.BorderSize = 0;
            this.ButtonInstall.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonInstall.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonInstall.Location = new System.Drawing.Point(37, 305);
            this.ButtonInstall.Name = "ButtonInstall";
            this.ButtonInstall.Size = new System.Drawing.Size(353, 31);
            this.ButtonInstall.TabIndex = 13;
            this.ButtonInstall.Text = "Install";
            this.ButtonInstall.UseVisualStyleBackColor = false;
            this.ButtonInstall.Click += new System.EventHandler(this.ButtonInstall_Click);
            // 
            // ProgressBarInstallation
            // 
            this.ProgressBarInstallation.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ProgressBarInstallation.Location = new System.Drawing.Point(37, 305);
            this.ProgressBarInstallation.Name = "ProgressBarInstallation";
            this.ProgressBarInstallation.Size = new System.Drawing.Size(353, 31);
            this.ProgressBarInstallation.TabIndex = 12;
            // 
            // LabelPage
            // 
            this.LabelPage.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelPage.Location = new System.Drawing.Point(37, 273);
            this.LabelPage.Name = "LabelPage";
            this.LabelPage.Size = new System.Drawing.Size(353, 29);
            this.LabelPage.TabIndex = 10;
            this.LabelPage.Text = "0/0";
            this.LabelPage.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.LabelPage.Visible = false;
            // 
            // LabelLink
            // 
            this.LabelLink.Cursor = System.Windows.Forms.Cursors.Hand;
            this.LabelLink.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelLink.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.LabelLink.Location = new System.Drawing.Point(37, 227);
            this.LabelLink.Name = "LabelLink";
            this.LabelLink.Size = new System.Drawing.Size(353, 33);
            this.LabelLink.TabIndex = 9;
            this.LabelLink.Text = "https://stackoverflow.com/questions/65976232/how-to-change-the-combobox-dropdown-" + "button-color/65976649#65976649";
            this.LabelLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelLink.Visible = false;
            this.LabelLink.Click += new System.EventHandler(this.LabelLink_Click);
            // 
            // LabelDescription
            // 
            this.LabelDescription.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelDescription.Location = new System.Drawing.Point(37, 85);
            this.LabelDescription.Name = "LabelDescription";
            this.LabelDescription.Size = new System.Drawing.Size(353, 113);
            this.LabelDescription.TabIndex = 8;
            this.LabelDescription.Text = resources.GetString("LabelDescription.Text");
            this.LabelDescription.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.LabelDescription.Visible = false;
            // 
            // LabelModpackDisplay
            // 
            this.LabelModpackDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.LabelModpackDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelModpackDisplay.ForeColor = System.Drawing.SystemColors.Control;
            this.LabelModpackDisplay.Location = new System.Drawing.Point(37, 16);
            this.LabelModpackDisplay.Name = "LabelModpackDisplay";
            this.LabelModpackDisplay.Size = new System.Drawing.Size(353, 36);
            this.LabelModpackDisplay.TabIndex = 6;
            this.LabelModpackDisplay.Text = "Loading";
            this.LabelModpackDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ButtonPrevious
            // 
            this.ButtonPrevious.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.ButtonPrevious.FlatAppearance.BorderSize = 0;
            this.ButtonPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonPrevious.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonPrevious.Location = new System.Drawing.Point(3, 16);
            this.ButtonPrevious.Name = "ButtonPrevious";
            this.ButtonPrevious.Size = new System.Drawing.Size(28, 36);
            this.ButtonPrevious.TabIndex = 5;
            this.ButtonPrevious.Text = "<";
            this.ButtonPrevious.UseVisualStyleBackColor = false;
            this.ButtonPrevious.Click += new System.EventHandler(this.ButtonPrevious_Click);
            // 
            // ButtonNext
            // 
            this.ButtonNext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.ButtonNext.FlatAppearance.BorderSize = 0;
            this.ButtonNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonNext.ForeColor = System.Drawing.SystemColors.Control;
            this.ButtonNext.Location = new System.Drawing.Point(396, 16);
            this.ButtonNext.Name = "ButtonNext";
            this.ButtonNext.Size = new System.Drawing.Size(28, 36);
            this.ButtonNext.TabIndex = 4;
            this.ButtonNext.Text = ">";
            this.ButtonNext.UseVisualStyleBackColor = false;
            this.ButtonNext.Click += new System.EventHandler(this.ButtonNext_Click);
            // 
            // label2
            // 
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label2.Location = new System.Drawing.Point(456, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(1, 348);
            this.label2.TabIndex = 3;
            this.label2.Text = "label2";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(446, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1, 348);
            this.panel1.TabIndex = 2;
            // 
            // RichTextConsole
            // 
            this.RichTextConsole.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(62)))), ((int)(((byte)(62)))));
            this.RichTextConsole.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RichTextConsole.Cursor = System.Windows.Forms.Cursors.Cross;
            this.RichTextConsole.ForeColor = System.Drawing.SystemColors.Menu;
            this.RichTextConsole.Location = new System.Drawing.Point(477, 64);
            this.RichTextConsole.Margin = new System.Windows.Forms.Padding(5);
            this.RichTextConsole.Name = "RichTextConsole";
            this.RichTextConsole.ReadOnly = true;
            this.RichTextConsole.Size = new System.Drawing.Size(271, 348);
            this.RichTextConsole.TabIndex = 1;
            this.RichTextConsole.Text = "";
            // 
            // PanelTitleBar
            // 
            this.PanelTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(46)))), ((int)(((byte)(46)))));
            this.PanelTitleBar.Controls.Add(this.label1);
            this.PanelTitleBar.Controls.Add(this.pictureBox1);
            this.PanelTitleBar.Controls.Add(this.ButtonMinimise);
            this.PanelTitleBar.Controls.Add(this.ButtonClose);
            this.PanelTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.PanelTitleBar.Location = new System.Drawing.Point(0, 0);
            this.PanelTitleBar.Name = "PanelTitleBar";
            this.PanelTitleBar.Size = new System.Drawing.Size(800, 30);
            this.PanelTitleBar.TabIndex = 0;
            this.PanelTitleBar.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitleBar_MouseDown);
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(31, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 30);
            this.label1.TabIndex = 2;
            this.label1.Text = "Modpack Installer";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::ModpackInstallerLauncher.Properties.Resources.logo;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 30);
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // ButtonMinimise
            // 
            this.ButtonMinimise.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButtonMinimise.FlatAppearance.BorderSize = 0;
            this.ButtonMinimise.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonMinimise.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonMinimise.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.ButtonMinimise.Location = new System.Drawing.Point(696, 0);
            this.ButtonMinimise.Name = "ButtonMinimise";
            this.ButtonMinimise.Size = new System.Drawing.Size(52, 30);
            this.ButtonMinimise.TabIndex = 3;
            this.ButtonMinimise.Text = "—";
            this.ButtonMinimise.TextImageRelation = System.Windows.Forms.TextImageRelation.TextAboveImage;
            this.ButtonMinimise.UseVisualStyleBackColor = true;
            this.ButtonMinimise.Click += new System.EventHandler(this.ButtonMinimise_Click);
            // 
            // ButtonClose
            // 
            this.ButtonClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.ButtonClose.FlatAppearance.BorderSize = 0;
            this.ButtonClose.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.ButtonClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.ButtonClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ButtonClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ButtonClose.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.ButtonClose.Location = new System.Drawing.Point(748, 0);
            this.ButtonClose.Name = "ButtonClose";
            this.ButtonClose.Size = new System.Drawing.Size(52, 30);
            this.ButtonClose.TabIndex = 1;
            this.ButtonClose.Text = "✕";
            this.ButtonClose.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ButtonClose.UseVisualStyleBackColor = true;
            this.ButtonClose.Click += new System.EventHandler(this.ButtonClose_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.layout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Main";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.layout.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.PanelTitleBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button DeleteCache;

        private System.Windows.Forms.ProgressBar ProgressBarInstallation;

        private System.Windows.Forms.Label LabelPage;

        private System.Windows.Forms.Label LabelLink;
        private System.Windows.Forms.Button ButtonClearConsole;

        private System.Windows.Forms.Label LabelDescription;

        private System.Windows.Forms.Button ButtonInstall;

        private System.Windows.Forms.Label LabelModpackDisplay;

        private System.Windows.Forms.Button ButtonPrevious;
        private System.Windows.Forms.Panel panel2;

        private System.Windows.Forms.Button ButtonNext;

        private System.Windows.Forms.Label label2;

        private System.Windows.Forms.RichTextBox RichTextConsole;
        private System.Windows.Forms.Panel panel1;

        private System.Windows.Forms.PictureBox pictureBox1;

        private System.Windows.Forms.Label label1;

        private System.Windows.Forms.Button ButtonMinimise;

        private System.Windows.Forms.Button ButtonReload;

        private System.Windows.Forms.Button ButtonClose;

        private System.Windows.Forms.Panel PanelTitleBar;

        private System.Windows.Forms.Panel layout;

        #endregion
    }
}