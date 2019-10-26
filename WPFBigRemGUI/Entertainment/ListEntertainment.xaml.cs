using ConnectionSampleCode.HandleUtil;
using log4net;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        private bool DisabledEventTEmporary = false; // false: disable; true: enable
        public EntertainmentUtil entertainmentUtil;
        public List<RememberUtility.Model.Entertainment> GetAllRecordEntertainments;

        private static readonly ILog Logs = LogManager.GetLogger(typeof(ListBooks));

        public ListEntertainment()
        {
            LoggerUtil.HandleLogPath();
            Logs.Info($"[WPFBigRemGUI.ListEntertainment] Starting ListEntertainment wpf GUI.");
            InitializeComponent();

            entertainmentUtil = new EntertainmentUtil();

            // list total
            GetAllRecordEntertainments = entertainmentUtil.GetListEntertainments();

            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lblListEt.Foreground = Brushes.Green;

            var LiveTime = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            LiveTime.Tick += Timer_Tick;
            LiveTime.Start();

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            listviewEt.ItemsSource = entertainmentUtil.GetfirstEntertainment(30);
            ListObjectEt.Content = entertainmentUtil.GetListEntertainment(30, GetAllRecordEntertainments).Count;
        }

        /// <summary>
        /// Load data to list view
        /// </summary>
        public void ReloadData()
        {
            // show list book
            if (entertainmentUtil.GetListEntertainments() != null)
            {
                // Count objects        
                ListObjectEt.Foreground = Brushes.ForestGreen;
                ListObjectEt.Content = entertainmentUtil.GetListEntertainment(30, GetAllRecordEntertainments).Count;

                var stopwatch = new Stopwatch();
                stopwatch.Restart();

                // Reload all
                listviewEt.ItemsSource = entertainmentUtil.GetfirstEntertainment(30);

                // Refresh list view
                ICollectionView view = CollectionViewSource.GetDefaultView(listviewEt.ItemsSource);
                view.Refresh();

                // Count objects        
                ListObjectEt.Foreground = Brushes.ForestGreen;
                ListObjectEt.Content = entertainmentUtil.GetListEntertainment(30, GetAllRecordEntertainments).Count;

                // count seconds
                Show_ms.Content = stopwatch.Elapsed.TotalMilliseconds;
                stopwatch.Stop();
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
            new MainWindow().Show();
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
                AddExtension = true,
                FileName = FileConstant.SaveFileName + HandleRandom.GetDateTimeNow()
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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (mnuAlwaysOnTop.IsChecked)
            {
                mnuAlwaysOnTop.IsCheckable = true;
                Topmost = true;
                Activate();
            }
            else
            {
                Topmost = false;
                Activate();
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

        private void DeleteItem_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D) // Press key
            {
                DeleteItem_Click(sender, e);
            }
        }

        private void DeleteItem_Click(object sender, RoutedEventArgs e)
        {
            // click
            var etLink = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;

            if (MessageBox.Show($"Found '{etLink.EnterName}'." +
                     $" Do you wanna delete '{etLink.EnterName}'", "Confirm delete",
                     MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                entertainmentUtil.DeleteEntertainment(etLink.EnterName);
                lblResultStatus.Foreground = Brushes.Green;
                lblResultStatus.Content = "Deleted";

                // Count second
                var stopwatch = new Stopwatch();
                stopwatch.Restart();

                // Count this object loading
                listviewEt.ItemsSource = entertainmentUtil.GetListEntertainments();

                Show_ms.Content = stopwatch.Elapsed.TotalMilliseconds;


                // Count again
                ListObjectEt.Foreground = Brushes.ForestGreen;
                ListObjectEt.Content = entertainmentUtil.GetListEntertainments().Count;
                stopwatch.Stop();
            }
            else
            {
                lblResultStatus.Foreground = Brushes.Red;
                lblResultStatus.Content = "Canceled";
            }
        }

        private void MenuAddEt_Click(object sender, RoutedEventArgs e)
        {
            EntertainemntMain entertainemntMain = new EntertainemntMain();

            entertainemntMain.ShowDialog();
        }

        private void Grid_KeyDown(object sender, KeyEventArgs e)
        {
            Debug.WriteLine($"[Grid_KeyDown] was called");
            if (e.Key == Key.F5)
            {
                if (!DisabledEventTEmporary)
                {
                    ReloadData();
                }
            }
        }

        private void MenuAFind_Click(object sender, RoutedEventArgs e)
        {
            new FindEt().ShowDialog();
        }


        private void ListviewEt_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            DisabledEventTEmporary = true;

            Debug.WriteLine($"[ListviewEt_ScrollChanged] was called");
            // Get the border of the listview (first child of a listview)
            Decorator border = VisualTreeHelper.GetChild(sender as ListView, 0) as Decorator;

            // Get scrollviewer
            ScrollViewer scrollViewer = border.Child as ScrollViewer;

            // check if load full from db
            var result = (listviewEt.ItemsSource as List<RememberUtility.Model.Entertainment>).Count;
            var resultFromDb = entertainmentUtil.GetListEntertainments().Count; // always be the maximum number

            if (result != resultFromDb)
            {
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    // get 30 records more
                    var currentRecords = listviewEt.ItemsSource as List<RememberUtility.Model.Entertainment>;
                    var getNextRecords = entertainmentUtil.GetListEntertainment(currentRecords.Count, GetAllRecordEntertainments);

                    currentRecords.AddRange(getNextRecords);
                    var stopwatch = new Stopwatch();
                    stopwatch.Restart();

                    listviewEt.ItemsSource = currentRecords;

                    // Refresh list view
                    ICollectionView view = CollectionViewSource.GetDefaultView(listviewEt.ItemsSource);
                    view.Refresh();

                    // count seconds
                    Show_ms.Content = stopwatch.Elapsed.TotalMilliseconds;
                    stopwatch.Stop();

                    // count again
                    ListObjectEt.Content = currentRecords.Count;
                }
            }
            DisabledEventTEmporary = false;
        }
    }
}