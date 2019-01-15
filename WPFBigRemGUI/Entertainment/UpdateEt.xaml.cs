using log4net;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System.ComponentModel;
using System.Windows;
using System.Windows.Media;

namespace WPFBigRemGUI.Entertainment
{
    /// <summary>
    /// Interaction logic for UpdateEt.xaml
    /// </summary>
    public partial class UpdateEt : Window
    {
        private EntertainmentUtil EntertainmentUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(UpdateEt));
        private RememberUtility.Model.Entertainment entertainment;
        public UpdateEt()
        {
            LoggerUtil.HandleLogPath();
            InitializeComponent();
            EntertainmentUtil = new EntertainmentUtil();
            entertainment = new RememberUtility.Model.Entertainment();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            var listConstantValue = typeof(CategoriesEntertainmentConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            cbbListEtUpdateCategory.ItemsSource = listConstantValue;

            txtUpdateEtName.IsEnabled = true;
            txtLink.IsEnabled = true;
            btnUpadte.IsEnabled = false;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (!e.Cancel)
            {
                var etMain = new EntertainemntMain();
                etMain.Show();
            }
        }

        private void btnUpadte_Click(object sender, RoutedEventArgs e)
        {
            if (entertainment != null)
            {
                if (txtUpdateEtName.Text != "")
                {
                    if (txtLink.Text != "")
                    {
                        if (cbbListEtUpdateCategory.Text != "")
                        {
                            EntertainmentUtil.UpdateEntertainment(entertainment.EnterName,
                                txtUpdateEtName.Text,
                                                txtLink.Text, cbbListEtUpdateCategory.Text);
                            lblResult.Foreground = Brushes.Green;
                            lblResult.Content = $"Update '{txtUpdateEtName.Text}' successful.";
                        }
                    }
                    else // else for empty Link
                    {
                        lblResult.Foreground = Brushes.Red;
                        lblResult.Content = "Something does not right with Author.";
                    }
                }
                else // else for Et name
                {
                    lblResult.Foreground = Brushes.Red;
                    lblResult.Content = "Something does not right with Et name.";
                }
            }
            else
            {
                lblResult.Foreground = Brushes.Red;
                lblResult.Content = "Something does not right with Et name.";
            }
        }

        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            entertainment = EntertainmentUtil.FindEntertainmentBy(txtUpdateEtName.Text);
            if (entertainment != null)
            {
                lblResult.Foreground = Brushes.Green;
                lblResult.Content = $"Found '{entertainment.EnterName}'.";

                txtUpdateEtName.Text = entertainment.EnterName;
                txtLink.Text = entertainment.Links;
                cbbListEtUpdateCategory.Text = entertainment.Category;
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
