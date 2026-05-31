// ------------------------------------------------------------
// Copyright (c) 2026 Leonardo
// 
// This software is provided free of charge for personal and
// corporate use, including companies, under the terms of the EULA.
// 
// Modification, redistribution, or copying, in whole or in part,
// is strictly prohibited without prior authorization from the author.
// 
// See LICENSE.txt for full details.
// ------------------------------------------------------------

using Microsoft.VisualStudio.Shell;
using System;
using System.Runtime.InteropServices;

namespace RunDisplay
{

    [Guid("65237212-976c-4aa9-b837-32958d86c8e9")]
    public class RunDisplayWindow : ToolWindowPane
    {
        public RunDisplayWindow() : base(null)
        {
            this.Caption = "RunDisplayWindow";

            this.Content = new RunDisplayWindowControl();
        }
    }
}
