// <copyright file="MainForm.cs" company="PublicDomain.is">
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
        /// Handles the new tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnNewToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Ask user
            if (MessageBox.Show("Reset settings data?", "New", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                // Delete settings data
                File.Delete(this.settingsDataPath);

                // Create new settings file
                this.SaveSettingsFile(this.settingsDataPath, new SettingsData());

                // Load settings from disk
                this.settingsData = this.LoadSettingsFile(this.settingsDataPath);

                // Set topmost
                this.settingsData.AlwaysOnTop = this.alwaysOnTopToolStripMenuItem.Checked;

                // Reset toggle count
                this.toggleCount = 0;

                // Update toggle count
                this.togglesCountToolStripStatusLabel.Text = this.toggleCount.ToString();
            }
        }

        /// <summary>
        /// Handles the free releases public domainis tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnFreeReleasesPublicDomainisToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open our website
            Process.Start("https://publicdomain.is");
        }

        /// <summary>
        /// Handles the original thread donation codercom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOriginalThreadDonationCodercomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open orignal thread
            Process.Start("https://www.donationcoder.com/forum/index.php?topic=52558.0");
        }

        /// <summary>
        /// Handles the source code githubcom tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnSourceCodeGithubcomToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open GitHub repository
            Process.Start("https://github.com/publicdomain/mono-toggler");
        }

        /// <summary>
        /// Handles the about tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnAboutToolStripMenuItemClick(object sender, EventArgs e)
        {
            // TODO Add code
        }

        /// <summary>
        /// Handles the options tool strip menu item drop down item clicked.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnOptionsToolStripMenuItemDropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            // Set tool strip menu item
            ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)e.ClickedItem;

            // Toggle checked
            toolStripMenuItem.Checked = !toolStripMenuItem.Checked;

            // Set topmost by check box
            this.TopMost = this.alwaysOnTopToolStripMenuItem.Checked;
        }

        /// <summary>
        /// Handles the mono toggler check box checked changed.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMonoTogglerCheckBoxCheckedChanged(object sender, EventArgs e)
        {
            // Check status
            if (this.monoTogglerCheckBox.Checked)
            {
                // Enable
                this.EnableMono();
            }
            else
            {
                // Disable
                this.DisableMono();
            }
        }

        /// <summary>
        /// Handles the main form load.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainFormLoad(object sender, EventArgs e)
        {
            // Set topmost values
            this.alwaysOnTopToolStripMenuItem.Checked = this.settingsData.AlwaysOnTop;
            this.TopMost = this.settingsData.AlwaysOnTop;

            // Open registry key
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Multimedia\Audio", true))
            {
                // Check for value
                if (registryKey.GetValue("AccessibilityMonoMixState") == null)
                {
                    // Set default value 
                    registryKey.SetValue("AccessibilityMonoMixState", 0, RegistryValueKind.DWord);
                }
                else
                {
                    // Check for mono value
                    if ((int)registryKey.GetValue("AccessibilityMonoMixState") == 1)
                    {
                        // Change text
                        this.monoTogglerCheckBox.Text = "MONO press to stereo";
                    }
                }
            }
        }

        /// <summary>
        /// Handles the main form form closing.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnMainFormFormClosing(object sender, FormClosingEventArgs e)
        {
            // Save settings data to disk
            this.SaveSettingsFile(this.settingsDataPath, this.settingsData);
        }

        /// <summary>
        /// Handles the refresh tool strip menu item click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnRefreshToolStripMenuItemClick(object sender, EventArgs e)
        {
            // Open registry key
            using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Multimedia\Audio", true))
            {
                // Check for mono value
                if ((int)registryKey.GetValue("AccessibilityMonoMixState") == 1)
                {
                    // Set check box for it to trigger enabling
                    this.monoTogglerCheckBox.Checked = true;
                }
                else
                {
                    // Set check box for it to trigger disabling
                    this.monoTogglerCheckBox.Checked = false;
                }
            }
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
            try
            {
                // Update with status (count ypdate with change it again)
                this.togglesCountToolStripStatusLabel.Text = "Toggling...";

                // Set service controller
                ServiceController serviceController = new ServiceController("Audiosrv");

                // Restart service
                if ((serviceController.Status.Equals(ServiceControllerStatus.Running)) || (serviceController.Status.Equals(ServiceControllerStatus.StartPending)))
                {
                    serviceController.Stop();
                }
                serviceController.WaitForStatus(ServiceControllerStatus.Stopped);
                serviceController.Start();
                serviceController.WaitForStatus(ServiceControllerStatus.Running);
            }
            catch (Exception ex)
            {
                // Advise user
                MessageBox.Show($"Service restart error:{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Audiosrv", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
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
                    this.monoTogglerCheckBox.Text = "MONO press to stereo";
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
        /// Disables the mono.
        /// </summary>
        private void DisableMono()
        {
            try
            {
                // Open registry key
                using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Multimedia\Audio", true))
                {
                    // Set value 
                    registryKey.SetValue("AccessibilityMonoMixState", 0, RegistryValueKind.DWord);

                    // Restart audio
                    this.RestartAudio();

                    // Change text
                    this.monoTogglerCheckBox.Text = "STEREO press to mono";
                }

                // Raise toggle count
                this.toggleCount++;

                // Update toggle count
                this.togglesCountToolStripStatusLabel.Text = this.toggleCount.ToString();
            }
            catch (Exception ex)
            {
                // Advise user
                MessageBox.Show($"Error when disabling:{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Disable mono", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        /// <summary>
        /// Handles the exit tool strip menu item1 click.
        /// </summary>
        /// <param name="sender">Sender object.</param>
        /// <param name="e">Event arguments.</param>
        private void OnExitToolStripMenuItem1Click(object sender, EventArgs e)
        {
            // Close program
            this.Close();
        }
    }
}
