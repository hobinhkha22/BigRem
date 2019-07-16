using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using log4net;
using System.Windows;
using System.Windows.Media;
using RememberUtility.Enum;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.IO;
using ConnectionSampleCode.HandleUtil;
using System.Windows.Input;
using System;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for Books.xaml
    /// </summary>
    public partial class Books : Window
    {
        private string GetStringValue;
        private BooksUtil booksUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(Books));


        public Books()
        {
            LoggerUtil.HandleLogPath();
            Logs.Info($"[WPFBigRemGUI.Books] Starting Books wpf gui.");
            InitializeComponent();
            booksUtil = new BooksUtil();
            txtBookName.Text = "";
            txtAuthor.Text = "";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var listConstantValue = typeof(CategoriesBookConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            lstListCategory.ItemsSource = listConstantValue;

            btnBookAdd.IsEnabled = true;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            // Run Asynchronous
            DoBackupAsync();
        }

        // Destructor
        ~Books()
        {
            Console.WriteLine("Destructor");
        }

        private async void DoBackupAsync()
        {
            await Task.Run(() =>
            {
                // Backup Database
                if (booksUtil.GetListBooks() != null)
                {
                    booksUtil.BackupDatabase(EnumFileConstant.BOOKCONSTANT, FileConstant.BackUpDb);
                    // Backup by zipfile                
                    ZipBackupFiles.ZipFile(EnumFileConstant.BOOK);
                }
            });
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var book = new RememberUtility.Model.Books();

            // txtBookName
            if (txtBookName.Text != "")
            {
                book.BookName = txtBookName.Text.Trim();
            }
            else
            {
                AddBookResult.Foreground = Brushes.Red;
                AddBookResult.Content = "Book name cannot be null. Please choose a type";
            }

            // txtAuthor
            if (txtAuthor.Text != "")
            {
                book.Author = txtAuthor.Text.Trim();
            }
            else
            {
                AddBookResult.Foreground = Brushes.Red;
                AddBookResult.Content = "Author cannot be null. Please choose a type";
            }

            if (lstListCategory.SelectedIndex == -1)
            {
                AddBookResult.Foreground = Brushes.Red;
                AddBookResult.Content = "Category aren't choose. Please choose a type";
            }

            if (txtAuthor.Text != "" && txtBookName.Text != "" && lstListCategory.SelectedIndex >= 0)
            {
                // use dynamic as type to cast your anonymous object to
                dynamic categoryInList = (lstListCategory.SelectedItem);

                book.Category = categoryInList as string;

                var findBook = booksUtil.FindBookBy(txtBookName.Text);
                if (findBook != null)
                {
                    if (book.BookName.ToLower() != findBook.BookName.ToLower())
                    {
                        booksUtil.AddBook(book);
                        AddBookResult.Foreground = Brushes.Green;


                        if (book.BookName.Length <= 10)
                        {
                            AddBookResult.Content = $"Add '{book.BookName}' Successful";
                        }
                        else
                        {
                            AddBookResult.Content = "Add Book Successful";
                        }

                        txtBookName.Clear();
                        txtAuthor.Clear();
                        lstListCategory.Text = string.Empty;
                    }
                    else
                    {
                        AddBookResult.Foreground = Brushes.Red;
                        AddBookResult.Content = $"'{txtBookName.Text}' duplicate. Add failed.";
                    }
                }
                else
                {
                    booksUtil.AddBook(book);
                    AddBookResult.Foreground = Brushes.Green;
                    AddBookResult.Content = "Add Book Successful";

                    txtBookName.Clear();
                    txtAuthor.Clear();
                    lstListCategory.Text = string.Empty;
                }
            }
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            var find = new Find();
            find.ShowDialog();

            // get data                        
            var resultBook = booksUtil.FindBookBy(GetStringValue);
            if (resultBook != null)
            {
                txtBookName.Text = resultBook.BookName;
                txtAuthor.Text = resultBook.Author;
                lstListCategory.Text = resultBook.Category;
            }

            var resultAuthor = booksUtil.FindBookByBookAuthor(GetStringValue);
            if (resultAuthor != null)
            {
                txtBookName.Text = resultAuthor.BookName;
                txtAuthor.Text = resultAuthor.Author;
                lstListCategory.Text = resultAuthor.Category;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var book = new RememberUtility.Model.Books();

            // txtBookName
            if (txtBookName.Text != "")
            {
                book.BookName = txtBookName.Text;
            }
            else
            {
                AddBookResult.Foreground = Brushes.Red;
                AddBookResult.Content = "Book name cannot be null. Please choose a type";
            }

            if (txtBookName.Text == "")
            {
                AddBookResult.Foreground = Brushes.Red;
                AddBookResult.Content = "You must input book name";
            }
            else // has input
            {
                var findBook = booksUtil.FindBookBy(txtBookName.Text);
                if (findBook != null)
                {
                    if (book.BookName.ToLower() == findBook.BookName.ToLower())
                    {
                        if (MessageBox.Show($"Found '{txtBookName.Text}'." +
                            $" Do you wanna to delete '{txtBookName.Text}'?", "Confirm delete",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            booksUtil.DeleteBook(book.BookName);
                            AddBookResult.Foreground = Brushes.Green;
                            AddBookResult.Content = "Delete Book Successful";

                            txtBookName.Text = string.Empty;
                            txtAuthor.Text = string.Empty;
                            lstListCategory.Text = string.Empty;
                        }
                        else
                        {
                            AddBookResult.Foreground = Brushes.YellowGreen;
                            AddBookResult.Content = $"Delete operate was canceled.";
                        }
                    }
                } // input and not found
                else
                {
                    AddBookResult.Foreground = Brushes.Red;
                    AddBookResult.Content = $" Cannot find '{txtBookName.Text}'. Delete failed.";
                }
            }
        }

        public void GetName(string bookName)
        {
            GetStringValue = bookName;
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            new UpdateBooks().Show();
            Close();
        }

        private void btnListBook_Click(object sender, RoutedEventArgs e)
        {
            new ListBooks().Show();
            Close();
        }

        private void MenuBarItem_Click(object sender, RoutedEventArgs e)
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

        private void mnuItemExport_Click(object sender, RoutedEventArgs e)
        {
            var saveFile = new SaveFileDialog
            {
                Filter = "File type (*.xlsx)|*.xlsx",
                AddExtension = true
            };
            var tableName = FileConstant.Books;
            var filePath = "";

            if (saveFile.ShowDialog().Value == true)
            {
                filePath = Path.GetFullPath(saveFile.FileName);
                booksUtil.SaveBookToExcel(filePath, tableName);

                AddBookResult.Foreground = Brushes.Green;
                AddBookResult.Content = "Export successful.";
            }
            else
            {
                AddBookResult.Foreground = Brushes.Red;
                AddBookResult.Content = "Export failed.";
            }
        }

        private void Spacebar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e != null && e.Key == Key.Space)
            {
                lstListCategory.IsDropDownOpen = true;
            }
        }
    }
}
