using ConnectionSampleCode.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for Find.xaml
    /// </summary>
    public partial class Find : Window
    {
        private BooksUtil booksUtil;
              
        public Find()
        {
            InitializeComponent();
            booksUtil = new BooksUtil();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            var listConstantValue = typeof(TypeBooksConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            comboboxNameAuthor.ItemsSource = listConstantValue;
            comboboxNameAuthor.SelectedIndex = 0;
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            base.OnPreviewKeyDown(e);

            if (e.Key == Key.Enter)
            {
                btnFind.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            if (txtFind.Text != "")
            {
                var selectItem = comboboxNameAuthor.SelectedValue as string;

                if (selectItem == TypeBooksConstant.BookName)
                {
                    var result = booksUtil.FindBookBy(txtFind.Text);
                    if (result != null)
                    {
                        lblFindResult.Foreground = Brushes.Green;
                        lblFindResult.Content = $"Found '{txtFind.Text}'";

                        // Send value to Books                    
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is Books)
                            {
                                ((Books)item).GetName(txtFind.Text);
                            }
                        }

                        if (cbxAutoClose.IsChecked.Value) // if true
                        {
                            Close();
                        }
                    }
                    else
                    {
                        lblFindResult.Foreground = Brushes.Red;
                        lblFindResult.Content = "Nothing found";
                    }
                }
                else if (selectItem == TypeBooksConstant.Author)
                {
                    var result = booksUtil.FindBookByBookAuthor(txtFind.Text);
                    if (result != null)
                    {
                        lblFindResult.Foreground = Brushes.Green;
                        lblFindResult.Content = $"Found '{txtFind.Text}'";

                        // Send value to Books                    
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is Books)
                            {
                                ((Books)item).GetName(txtFind.Text);
                            }
                        }

                        if (cbxAutoClose.IsChecked.Value) // if true
                        {
                            Close();
                        }
                    }
                    else
                    {
                        lblFindResult.Foreground = Brushes.Red;
                        lblFindResult.Content = "Nothing found";
                    }
                }
                else
                {
                    lblFindResult.Foreground = Brushes.Red;
                    lblFindResult.Content = "Nothing found";
                }
            }
            else
            {
                lblFindResult.Foreground = Brushes.Red;
                lblFindResult.Content = "You must type a book name";
            }
        }

        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = booksUtil.GetListBooks();

            string query = (sender as TextBox).Text;

            if (query.Length == 0)
            {
                // Clear
                resultStack.Children.Clear();
                border.Visibility = Visibility.Collapsed;
            }
            else
            {
                border.Visibility = Visibility.Visible;
            }

            // Clear the list
            resultStack.Children.Clear();

            // Add the result
            foreach (var book in data)
            {
                if (book.bookName.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work
                    AddItem(book.bookName);
                    found = true;
                }                
            }

            if (!found)
            {
                resultStack.Children.Add(new TextBlock() { Text = "No results found." });
            }
        }


        private void AddItem(string text)
        {
            TextBlock block = new TextBlock
            {

                // Add the text
                Text = text,

                // A little style...
                Margin = new Thickness(2, 3, 2, 3),
                Cursor = Cursors.Hand
            };

            // Mouse events
            block.MouseLeftButtonUp += (sender, e) =>
            {
                txtFind.Text = (sender as TextBlock).Text;
            };

            block.MouseEnter += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.PeachPuff;
            };

            block.MouseLeave += (sender, e) =>
            {
                TextBlock b = sender as TextBlock;
                b.Background = Brushes.Transparent;
            };

            // Add to the panel
            resultStack.Children.Add(block);
        }

    }
}
