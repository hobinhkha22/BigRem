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
    }
}
