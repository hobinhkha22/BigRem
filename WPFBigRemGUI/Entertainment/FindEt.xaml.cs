using RememberUtility.HandleUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

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
                var result = entertainmentUtil.FindEntertainmentBy(txtFind.Text);
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
                    lblFindResult.Content = $"Nothing found";
                }
            }
            else
            {
                lblFindResult.Foreground = Brushes.Red;
                lblFindResult.Content = $"You must type a Et name";
            }
        }
    }
}
