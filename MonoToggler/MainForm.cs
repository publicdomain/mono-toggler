﻿// <copyright file="MainForm.cs" company="PublicDomain.is">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>

namespace MonoToggler
{
    // Directives
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.IO;
    using System.Reflection;
    using System.ServiceProcess;
    using System.Windows.Forms;
    using System.Xml.Serialization;
    using Microsoft.VisualBasic;
    using Microsoft.Win32;
    using PublicDomain;

    /// <summary>
    /// Description of MainForm.
    /// </summary>
    public partial class MainForm : Form
    {
        /// <summary>
        /// Gets or sets the associated icon.
        /// </summary>
        /// <value>The associated icon.</value>
        private System.Drawing.Icon associatedIcon = null;

        /// <summary>
        /// The settings data.
        /// </summary>
        private SettingsData settingsData = null;

        /// <summary>
        /// The settings data path.
        /// </summary>
        private string settingsDataPath = $"{Application.ProductName}-SettingsData.txt";

        /// <summary>
        /// The toggle count.
        /// </summary>
        private int toggleCount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MonoToggler.MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            // The InitializeComponent() call is required for Windows Forms designer support.
            this.InitializeComponent();

            /* Set icons */

            // Set associated icon from exe file
            this.associatedIcon = System.Drawing.Icon.ExtractAssociatedIcon(typeof(MainForm).GetTypeInfo().Assembly.Location);

            // Set public domain weekly tool strip menu item image
            this.freeReleasesPublicDomainisToolStripMenuItem.Image = this.associatedIcon.ToBitmap();

            /* Settings data */

            // Check for settings file
            if (!File.Exists(this.settingsDataPath))
            {
                // Create new settings file
                this.SaveSettingsFile(this.settingsDataPath, new SettingsData());
            }

            // Load settings from disk
            this.settingsData = this.LoadSettingsFile(this.settingsDataPath);
        }

        /// <summary>
        /// Ons the new tool strip menu item1 click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnNewToolStripMenuItem1Click(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the free releases public domainis tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnFreeReleasesPublicDomainisToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the original thread donation codercom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnOriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the source code githubcom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnSourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the about tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the options tool strip menu item drop down item clicked.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnOptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the mono toggler check box checked changed.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnMonoTogglerCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the main form load.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnMainFormLoad(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the main form form closing.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the enable command tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnEnableCommandToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the disable command tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnDisableCommandToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Ons the refresh tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnRefreshToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Loads the settings file.
        /// </summary>
        /// <returns>The settings file.</returns>
        /// <param name="settingsFilePath">Settings file path.</param>
        private SettingsData LoadSettingsFile(string settingsFilePath)
        {
            // Use file stream
            using (FileStream fileStream = File.OpenRead(settingsFilePath))
            {
                // Set xml serialzer
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsData));

                // Return populated settings data
                return xmlSerializer.Deserialize(fileStream) as SettingsData;
            }
        }

        /// <summary>
        /// Saves the settings file.
        /// </summary>
        /// <param name="settingsFilePath">Settings file path.</param>
        /// <param name="settingsDataParam">Settings data parameter.</param>
        private void SaveSettingsFile(string settingsFilePath, SettingsData settingsDataParam)
        {
            try
            {
                // Use stream writer
                using (StreamWriter streamWriter = new StreamWriter(settingsFilePath, false))
                {
                    // Set xml serialzer
                    XmlSerializer xmlSerializer = new XmlSerializer(typeof(SettingsData));

                    // Serialize settings data
                    xmlSerializer.Serialize(streamWriter, settingsDataParam);
                }
            }
            catch (Exception exception)
            {
                // Advise user
                MessageBox.Show($"Error saving settings file.{Environment.NewLine}{Environment.NewLine}Message:{Environment.NewLine}{exception.Message}", "File error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Restarts the audio.
        /// </summary>
        private void RestartAudio()
        {
            // Set service controller
            ServiceController serviceController = new ServiceController
            {
                MachineName = ".",
                ServiceName = "Audiosrv"
            };

            // Restart the service
            serviceController.Stop();
            serviceController.Start();
        }

        /// <summary>
        /// Enables the mono.
        /// </summary>
        private void EnableMono()
        {
            try
            {
                // Open registry key
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Multimedia\Audio", true))
                {
                    // Set value 
                    registryKey.SetValue("AccessibilityMonoMixState", 1, RegistryValueKind.DWord);

                    // Restart audio
                    this.RestartAudio();

                    // Change text
                    this.monoTogglerCheckBox.Text = "Mono";
                }

                // Raise toggle count
                this.toggleCount++;

                // Update toggle count
                this.togglesCountToolStripStatusLabel.Text = this.toggleCount.ToString();
            }
            catch (Exception ex)
            {
                // Advise user
                MessageBox.Show($"Error when enabling:{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Enable mono", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Ons the exit tool strip menu item1 click.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="e">E.</param>
        private void OnExitToolStripMenuItem1Click(object sender, EventArgs e)
        {
            // TODO Add code
        }
    }
}
