﻿// <copyright file="SettingsData.cs" company="PublicDomain.is">
//     CC0 1.0 Universal (CC0 1.0) - Public Domain Dedication
//     https://creativecommons.org/publicdomain/zero/1.0/legalcode
// </copyright>
// <auto-generated />

namespace PublicDomain
{
    // Directives
    using System.Collections.Generic;
    using System.Drawing;

    /// <summary>
    /// Urlister settings.
    /// </summary>
    public class SettingsData
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="T:PublicDomain.SettingsData"/> class.
        /// </summary>
        public SettingsData()
        {
            // Parameterless constructor
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="T:PublicDomain.SettingsData"/> always on top.
        /// </summary>
        /// <value><c>true</c> if always on top; otherwise, <c>false</c>.</value>
        public bool AlwaysOnTop { get; set; } = false;

        /// <summary>
        /// Gets or sets the mono enable command.
        /// </summary>
        /// <value>The mono enable command.</value>
        public string MonoEnableCommand { get; set; } = "powershell -windowstyle hidden -command \"Start-Process cmd -ArgumentList '/s,/c,REG ADD \"HKCU\\Software\\Microsoft\\Multimedia\\Audio\" /V AccessibilityMonoMixState /T REG_DWORD /D 1 /F & net stop \"Audiosrv\" & net start \"Audiosrv\"' -Verb runAs\"";

        /// <summary>
        /// Gets or sets the mono disable command.
        /// </summary>
        /// <value>The mono disable command.</value>
        public string MonoDisableCommand { get; set; } = "powershell -windowstyle hidden -command \"Start-Process cmd -ArgumentList '/s,/c,REG ADD \"HKCU\\Software\\Microsoft\\Multimedia\\Audio\" /V AccessibilityMonoMixState /T REG_DWORD /D 0 /F & net stop \"Audiosrv\" & net start \"Audiosrv\"' -Verb runAs\"";
    }
}