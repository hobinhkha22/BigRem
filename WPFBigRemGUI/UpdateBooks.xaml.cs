using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using log4net;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for UpdateBooks.xaml
    /// </summary>
    public partial class UpdateBooks : Window
    {
        private BooksUtil booksUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(UpdateBooks));
        private RememberUtility.Model.Books books;

        public UpdateBooks()
        {
            LoggerUtil.HandleLogPath();
            InitializeComponent();
            booksUtil = new BooksUtil();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;
            var listConstantValue = typeof(CategoriesBookConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            cbbListBookUpdateCategory.ItemsSource = listConstantValue;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            txtUpdateBookName.IsEnabled = true;
            txtBookAuthor.IsEnabled = true;
            btnUpadte.IsEnabled = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                var books = new Books();                
                books.Show();
            }
        }

        private void btnUpadte_Click(object sender, RoutedEventArgs e)
        {
            if (books != null)
            {
                if (txtUpdateBookName.Text != "")
                {
                    if (txtBookAuthor.Text != "")
                    {
                        if (cbbListBookUpdateCategory.Text != "")
                        {
                            booksUtil.UpdateBook(books.BookName, txtUpdateBookName.Text,
                                                txtBookAuthor.Text, cbbListBookUpdateCategory.Text);
                            lblResult.Foreground = Brushes.Green;
                            lblResult.Content = $"Update '{txtUpdateBookName.Text}' successful.";
                        }
                    }
                    else // else for empty Author
                    {
                        lblResult.Foreground = Brushes.Red;
                        lblResult.Content = "Something does not right with Author.";
                    }
                }
                else // else for book name
                {
                    lblResult.Foreground = Brushes.Red;
                    lblResult.Content = "Something does not right with book name.";
                }
            }
            else
            {
                lblResult.Foreground = Brushes.Red;
                lblResult.Content = "Something does not right with book name.";
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            books = booksUtil.FindBookBy(txtUpdateBookName.Text);
            if (books != null)
            {
                lblResult.Foreground = Brushes.Green;
                lblResult.Content = $"Found '{books.BookName}'.";

                txtUpdateBookName.Text = books.BookName;
                txtBookAuthor.Text = books.Author;
                cbbListBookUpdateCategory.Text = books.Category;
                btnUpadte.IsEnabled = true;
            }
            else
            {
                lblResult.Foreground = Brushes.Red;
                lblResult.Content = $"Nothing found.";
            }
        }
    }
}
