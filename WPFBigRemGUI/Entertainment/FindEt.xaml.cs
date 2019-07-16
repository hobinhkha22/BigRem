using ConnectionSampleCode.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFBigRemGUI.Entertainment
{
    /// <summary>
    /// Interaction logic for FindEt.xaml
    /// </summary>
    public partial class FindEt : Window
    {
        private EntertainmentUtil entertainmentUtil;
        public FindEt()
        {
            InitializeComponent();
            entertainmentUtil = new EntertainmentUtil();

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            var listConstantValue = typeof(TypeEntertainmentsConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            comboxBoxEnterLink.ItemsSource = listConstantValue;
            comboxBoxEnterLink.SelectedIndex = 0;
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
                var selectedValue = comboxBoxEnterLink.SelectedValue as string;
                if (selectedValue == TypeEntertainmentsConstant.EnterName)
                {
                    var result = entertainmentUtil.FindEntertainmentBy(txtFind.Text);
                    if (result != null)
                    {
                        lblFindResult.Foreground = Brushes.Green;
                        lblFindResult.Content = $"Found '{txtFind.Text}'";

                        // Send value to EntertainemntMain                    
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is EntertainemntMain)
                            {
                                ((EntertainemntMain)item).GetName(txtFind.Text);
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
                        lblFindResult.Content = $"Nothing found";
                    }
                }
                else if (selectedValue == TypeEntertainmentsConstant.Link)
                {
                    var result = entertainmentUtil.FindEntertainmentByLink(txtFind.Text);
                    if (result != null)
                    {
                        lblFindResult.Foreground = Brushes.Green;
                        lblFindResult.Content = $"Found '{txtFind.Text}'";

                        // Send value to EntertainemntMain                    
                        foreach (Window item in Application.Current.Windows)
                        {
                            if (item is EntertainemntMain)
                            {
                                ((EntertainemntMain)item).GetName(txtFind.Text);
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
                        lblFindResult.Content = $"Nothing found";
                    }
                }
                else
                {
                    lblFindResult.Foreground = Brushes.Red;
                    lblFindResult.Content = $"Nothing found";
                }
            }
            else
            {
                lblFindResult.Foreground = Brushes.Red;
                lblFindResult.Content = $"You must type a Et name";
            }
        }


        private void TextBox_KeyUp(object sender, KeyEventArgs e)
        {
            bool found = false;
            var border = (resultStack.Parent as ScrollViewer).Parent as Border;
            var data = entertainmentUtil.GetListEntertainments();

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
            foreach (var entertainment in data)
            {
                if (entertainment.EnterName.ToLower().StartsWith(query.ToLower()))
                {
                    // The word starts with this... Autocomplete must work
                    AddItem(entertainment.EnterName);
                    found = true;
                }

                var links = HandleRandom.RemoveHttpString(entertainment.Links);
                if (links.ToLower().StartsWith(query.ToLower()))
                {
                    AddItem(entertainment.Links);
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

        private void resultStack_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            var getStringByClick = e.Source as TextBlock;

            if (getStringByClick.Text != "No results found.")
            {
                ContextMenu cm = new ContextMenu
                {
                    PlacementTarget = sender as Button,
                    IsOpen = true
                };

                cm.PlacementTarget = resultStack; // right click on beside cursor            

                MenuItem findMenuItem = new MenuItem
                {
                    Header = "_Copy link"
                };

                findMenuItem.Click += (o, args) =>
                {
                    if (getStringByClick != null)
                    {
                        Clipboard.SetText(getStringByClick.Text);
                    }
                };

                cm.Items.Add(findMenuItem);
            }
        }

        private static Label FindClickedItem(object sender)
        {
            if (!(sender is MenuItem mi))
            {
                return null;
            }

            if (!(mi.CommandParameter is ContextMenu cm))
            {
                return null;
            }

            return cm.PlacementTarget as Label;
        }
    }
}
