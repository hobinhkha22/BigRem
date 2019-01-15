using log4net;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            new EntertainemntMain().Show();
            Close();
        }

        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var getSelected = (RememberUtility.Model.Entertainment)listviewEt.SelectedItem;
            if (getSelected != null)
            {
                Clipboard.SetText(getSelected.Links, TextDataFormat.UnicodeText);
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
    }
}
