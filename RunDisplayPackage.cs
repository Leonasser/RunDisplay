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

using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;

namespace RunDisplay
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid(PackageGuidString)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [ProvideAutoLoad(UIContextGuids.SolutionExists, PackageAutoLoadFlags.BackgroundLoad)]
    [ProvideToolWindow(typeof(RunDisplayWindow))]
    public sealed class RunDisplayPackage : AsyncPackage
    {
        public const string PackageGuidString = "31708192-883a-483b-912b-f5b2be3065d0";

        private DebuggerEvents _debuggerEvents;

        protected override async Task InitializeAsync(CancellationToken cancellationToken, IProgress<ServiceProgressData> progress)
        {
            await this.JoinableTaskFactory.SwitchToMainThreadAsync(cancellationToken);

            if (await GetServiceAsync(typeof(DTE)) is DTE2 dte)
            {
                _debuggerEvents = dte.Events.DebuggerEvents;
                _debuggerEvents.OnEnterRunMode += OnEnterRunModeHandler;
                _debuggerEvents.OnEnterBreakMode += OnEnterBreakModeHandler;
                _debuggerEvents.OnEnterDesignMode += OnEnterDesignModeHandler;
            }
            else
            {
                Debug.WriteLine("DTE service not available.");
            }

        }

        private void OnEnterRunModeHandler(dbgEventReason reason) => ShowRunDisplay();
        private void OnEnterBreakModeHandler(dbgEventReason reason, ref dbgExecutionAction action) => HideRunDisplay();
        private void OnEnterDesignModeHandler(dbgEventReason reason) => CloseRunDisplay();
        private void ShowRunDisplay()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var window = this.FindToolWindow(typeof(RunDisplayWindow), 0, true);
            if (window?.Frame is IVsWindowFrame frame)
            {
                frame.Show();
                frame.SetProperty((int)__VSFPROPID.VSFPROPID_FrameMode, VSFRAMEMODE.VSFM_MdiChild);
            }
        }

        private void HideRunDisplay()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var window = this.FindToolWindow(typeof(RunDisplayWindow), 0, false);
            if (window?.Frame is IVsWindowFrame frame)
            {
                frame.Hide();
            }
        }

        private void CloseRunDisplay()
        {
            ThreadHelper.ThrowIfNotOnUIThread();
            var window = this.FindToolWindow(typeof(RunDisplayWindow), 0, false);
            if (window?.Frame is IVsWindowFrame frame)
            {
                frame.CloseFrame((uint)__FRAMECLOSE.FRAMECLOSE_SaveIfDirty);
            }
        }
    }
}
