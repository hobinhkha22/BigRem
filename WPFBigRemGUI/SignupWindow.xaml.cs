using ConnectionSampleCode.Extension;
using RememberUtility.Constant;
using RememberUtility.Enum;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using RememberUtility.Model;
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

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for SignupWindow.xaml
    /// </summary>
    public partial class SignupWindow : Window
    {
        private UserUtil userLogin;

        public SignupWindow()
        {
            InitializeComponent();
            userLogin = new UserUtil();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;
        }

        private void btnSignUp_Click(object sender, RoutedEventArgs e)
        {
            if (txtUsernameSignup.Text != "")
            {
                var _checkUser = userLogin.CheckUser(txtUsernameSignup.Text);
                if (_checkUser == null)
                {
                    if (txtPasswordSignup.Password != "" && txtConfirmPasswordSignup.Password != "")
                    {
                        if (txtPasswordSignup.Password == txtConfirmPasswordSignup.Password)
                        {
                            userLogin.AddUser(new UserLogin()
                            {
                                Username = txtUsernameSignup.Text,
                                PasswordEncrypt = Encrypter.Encrypt(txtPasswordSignup.Password, UserConstant.KeyEncrypt)
                            });

                            lblResultSignup.Foreground = Brushes.Green;
                            lblResultSignup.Content = "Sign up successful";
                        }
                    }
                }
                else // user doesn't exist.
                {
                    lblResultSignup.Foreground = Brushes.Red;
                    lblResultSignup.Content = $"The user '{txtUsernameSignup.Text}' exist.";
                }
            }
            else
            {
                lblResultSignup.Foreground = Brushes.Red;
                lblResultSignup.Content = "Sign up failed.";
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            // Reload db when closed signup
            
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnSignUp.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
                //SignInClick(new object(), e);
            }
        }
    }
}
