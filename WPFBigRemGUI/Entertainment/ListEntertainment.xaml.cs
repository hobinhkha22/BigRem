using ConnectionSampleCode.HandleUtil;
using log4net;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace WPFBigRemGUI.Entertainment
{
    /// <summary>
    /// Interaction logic for ListEntertainment.xaml
    /// </summary>
    public partial class ListEntertainment : Window
    {
        private EntertainmentUtil entertainmentUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(ListBooks));

        public ListEntertainment()
        {
            LoggerUtil.HandleLogPath();
            Logs.Info($"[WPFBigRemGUI.ListEntertainment] Starting ListEntertainment wpf GUI.");
            InitializeComponent();
            entertainmentUtil = new EntertainmentUtil();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lblListEt.Foreground = Brushes.Green;

            var LiveTime = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            LiveTime.Tick += Timer_Tick;
            LiveTime.Start();

            // Count objects        
            ListObjectEt.Content = entertainmentUtil.GetListEntertainments().Count;
            ListObjectEt.Foreground = Brushes.ForestGreen;

            var stopwatch = new Stopwatch();
            stopwatch.Restart();
            Show_ms.Content = stopwatch.Elapsed.TotalMilliseconds;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            // show list book
            if (entertainmentUtil.GetListEntertainments() != null)
            {
                listviewEt.ItemsSource = entertainmentUtil.GetListEntertainments();
            }
            else
            {
                Logs.Warn($"[WPFBigRemGUI.ListEntertainment] There's no element in Db Entertainment.");
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = DateTime.Now.ToString("HH:mm:ss");
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            new EntertainemntMain().Show();
            Close();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var getSelected = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;
            if (getSelected != null)
            {
                // Copy Enter name [Ctrl - B]
                if (e.Key == Key.B && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Clipboard.SetText(getSelected.EnterName, TextDataFormat.UnicodeText);
                }

                // Copy Link [Ctrl + F]
                if (e.Key == Key.F && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Clipboard.SetText(getSelected.Links, TextDataFormat.UnicodeText);
                }
            }
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            var savefile = new SaveFileDialog
            {
                // Set extension
                // example: saveFileDialog1.Filter = "RichTextFormate|*.rtf|Text Files|*.txt|All Files|*.*"; 
                Filter = "Data file (*.xlsx)|*.xlsx|All file (*.*)|*.*",
                DefaultExt = "xlsx",
                AddExtension = true
            };

            if (savefile.ShowDialog() == true)
            {
                string fullPath = savefile.FileName;

                HandleRandom.ExportExcel(
                    listviewEt.ItemsSource as List<RememberUtility.Model.Entertainment>, FileConstant.Entertainment, fullPath);
                lblResultExportFile.Foreground = Brushes.Green;
                lblResultExportFile.Content = "Export file successfull.";
            }
        }

        private void CopyEtNameContextMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (listviewEt.SelectedIndex > -1)
            {
                var et = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;
                Clipboard.SetText(et.EnterName, TextDataFormat.UnicodeText);
            }
        }

        private void CopyLinkContextMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (listviewEt.SelectedIndex > -1)
            {
                var et = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;
                Clipboard.SetText(et.Links, TextDataFormat.UnicodeText);
            }
        }

        private void CheckAlive_Click(object sender, RoutedEventArgs e)
        {
            var et = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;
            var returnStatusCode = WebHealthCheck.TestWebSite(et.Links);
            lblResultStatus.Foreground = Brushes.Green;
            lblResultStatus.Content = returnStatusCode;
        }

        private void OpenWithBrowser_Click(object sender, RoutedEventArgs e)
        {
            var getBrowser = PathHandle.GetStandardBrowserPath();

            var etLink = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;
            var getFileName = Path.GetFileName(getBrowser);
            var returnOpenWtihBrowser = etLink.Links;
            var ArgumentsPlus = string.Empty;

            if (getFileName == "chrome.exe")
            {
                ArgumentsPlus = " -incognito " + returnOpenWtihBrowser;
                Process.Start(getBrowser, ArgumentsPlus);
            }
            else if (getFileName == "firefox.exe")
            {
                ArgumentsPlus = " -private " + returnOpenWtihBrowser;
                Process.Start(getBrowser, ArgumentsPlus);
            }
            else
            {
                Process.Start(getBrowser, returnOpenWtihBrowser);
            }
        }

        private void OpenWithBrowserContextMenu_KeyDown(object sender, KeyEventArgs e)
        {
            // Open with chrome [Keyboard: B]
            if (e.Key == Key.C) // chrome
            {
                OpenWithBrowser_Click(sender, e);
            }
        }


    }
}