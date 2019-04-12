using ConnectionSampleCode.Extension;
using RememberUtility.Constant;
using RememberUtility.HandleUtil;
using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private UserUtil userLogin;
        private string GetRememberMe;

        public Login()
        {
            InitializeComponent();
            userLogin = new UserUtil();
            Application.Current.MainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            //Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;
                    
        }

        public void GetRememberMeValue(string getMe)
        {
            GetRememberMe = getMe;
        }

        private void SignInClick(object sender, RoutedEventArgs e)
        {
            // reload user
            userLogin.ReloadDatbase();   

            if (txtUsername.Text != "")
            {
                if (txtPassword.Password != "")
                {
                    var user = userLogin.CheckUser(txtUsername.Text);
                    if (user != null)
                    {
                        // With PassWordNormal
                        if (txtUsername.Text == user.Username && txtPassword.Password == Encrypter.Decrypt(user.PasswordEncrypt, UserConstant.KeyEncrypt))
                        {
                            var bigrem = new MainWindow();
                            Close();
                            bigrem.Show();

                            App.Current.Properties[0] = txtUsername.Text.Trim();
                        }
                        else
                        {
                            lblResultLogin.Foreground = Brushes.Red;
                            lblResultLogin.Content = $"Oops. Something went wrong.";
                        }

                        // With PasswordAdmin
                        if (txtPassword.Password == UserConstant.PasswordAdmin && txtUsername.Text == UserConstant.PasswordAdmin)
                        {
                            var bigrem = new MainWindow();
                            Close();
                            bigrem.Show();
                        }
                        else
                        {
                            lblResultLogin.Foreground = Brushes.Red;
                            lblResultLogin.Content = $"Oops. Something went wrong.";
                        }

                        // With SysPass
                        if (txtPassword.Password == UserConstant.SysPass && txtUsername.Text == UserConstant.SysPass)
                        {
                            var bigrem = new MainWindow();
                            Close();
                            bigrem.Show();
                        }
                        else
                        {
                            lblResultLogin.Foreground = Brushes.Red;
                            lblResultLogin.Content = $"Oops. Something went wrong.";
                        }
                    } // user == null
                    else
                    {
                        lblResultLogin.Foreground = Brushes.Red;
                        lblResultLogin.Content = $"Oops. Something went wrong.";
                    }
                }
            }
            else
            {
                lblResultLogin.Foreground = Brushes.Red;
                lblResultLogin.Content = $"You must input login info";
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            new SignupWindow().ShowDialog();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSignin.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                //SignInClick(new object(), e);
            }
        }        

    }
}
