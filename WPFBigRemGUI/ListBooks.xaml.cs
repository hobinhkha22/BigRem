using log4net;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for ListBooks.xaml
    /// </summary>
    public partial class ListBooks : Window
    {
        private BooksUtil booksUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(ListBooks));

        public ListBooks()
        {
            LoggerUtil.HandleLogPath();
            Logs.Info($"[WPFBigRemGUI.ListBooks] Starting ListBooks wpf gui.");
            InitializeComponent();
            booksUtil = new BooksUtil();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            lblListBook.Foreground = Brushes.Green;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;


            // show list book
            if (booksUtil.GetListBooks() != null)
            {
                listviewBook.ItemsSource = booksUtil.GetListBooks();
            }
            else
            {
                Logs.Warn($"[ListBooks] There's no element in Db Book.");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            new Books().Show();
            Close();
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
                    listviewBook.ItemsSource as List<RememberUtility.Model.Books>, FileConstant.Books, fullPath);
                lblExportBookResult.Foreground = Brushes.Green;
                lblExportBookResult.Content = "Export book successful.";
            }
        }

        private void CopyBookNameContextMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            var indexx = listviewBook.SelectedIndex;
            if (indexx > -1)
            {
                var books = (RememberUtility.Model.Books)listviewBook.SelectedItem;
                Clipboard.SetText(books.BookName, TextDataFormat.UnicodeText);
            }
        }

        private void CopyAuthorContextMenu_Click(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
            if (listviewBook.SelectedIndex > -1)
            {
                var books = (RememberUtility.Model.Books)listviewBook.SelectedItem;
                Clipboard.SetText(books.Author, TextDataFormat.UnicodeText);
            }
        }

    } // end class
}
