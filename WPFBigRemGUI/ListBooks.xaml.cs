using log4net;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System.Collections.Generic;
using System.Diagnostics;
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

            // Count object in db
            countObjectList.Content = booksUtil.GetListBooks().Count;
            countObjectList.Foreground = Brushes.ForestGreen;

            // show list book
            if (booksUtil.GetListBooks() != null)
            {
                var stopwatch = new Stopwatch();
                stopwatch.Restart();

                listviewBook.ItemsSource = booksUtil.GetListBooks();

                Show_ms.Content = stopwatch.Elapsed.TotalMilliseconds;
                Show_ms.Foreground = Brushes.ForestGreen;
            }
            else
            {
                Logs.Warn($"[ListBooks] There's no element in Db Book.");
            }
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            var getSelected = (RememberUtility.Model.Books)listviewBook.SelectedItem;
            if (getSelected != null)
            {
                // Copy Book name
                if (e.Key == Key.B && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Clipboard.SetText(getSelected.BookName, TextDataFormat.UnicodeText);
                }

                // Copy author
                if (e.Key == Key.F && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Clipboard.SetText(getSelected.Author, TextDataFormat.UnicodeText);
                }
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

        private void DeleteThisBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.D)
            {
                DeleteThisBook_Click(sender, e);
            }
        }

        private void DeleteThisBook_Click(object sender, RoutedEventArgs e)
        {
            var getBook = (RememberUtility.Model.Books)listviewBook.SelectedItem;

            if (MessageBox.Show($"Found '{getBook.bookName}'." +
                    $" Do you wanna delete '{getBook.bookName}'", "Confirm delete",
                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                booksUtil.DeleteBook(getBook.bookName);
                lblExportBookResult.Foreground = Brushes.Green;
                lblExportBookResult.Content = "Deleted";

                // Count second
                var stopwatch = new Stopwatch();
                stopwatch.Restart();

                // Count this object loading
                listviewBook.ItemsSource = booksUtil.GetListBooks();

                Show_ms.Content = stopwatch.Elapsed.TotalMilliseconds;

                // Count again
                countObjectList.Foreground = Brushes.ForestGreen;
                countObjectList.Content = booksUtil.GetListBooks().Count;
            }
            else
            {
                lblExportBookResult.Foreground = Brushes.Red;
                lblExportBookResult.Content = "Canceled";
            }
        }
    } // end class
}
