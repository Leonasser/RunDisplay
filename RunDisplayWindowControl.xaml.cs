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

using System;
using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace RunDisplay
{
    /// <summary>
    /// Interaction logic for RunDisplayWindowControl.
    /// </summary>
    public partial class RunDisplayWindowControl : UserControl
    {
        private DispatcherTimer _timer;
        private DateTime _startTime;

        public RunDisplayWindowControl()
        {
            InitializeComponent();

            _startTime = DateTime.Now;

            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _timer.Tick += Timer_Tick;
            _timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            var elapsed = DateTime.Now - _startTime;
            Timer.Text = $"{elapsed.Hours:D2}:{elapsed.Minutes:D2}:{elapsed.Seconds:D2}";
        }
    }
}