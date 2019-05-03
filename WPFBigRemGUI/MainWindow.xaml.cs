using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPFBigRemGUI.Entertainment;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            var bookmain = new ListBooks();
            Close();
            bookmain.Show();
        }

        private void btnEntertainment_Click(object sender, RoutedEventArgs e)
        {
            var etmain = new ListEntertainment();
            Close();
            etmain.Show();
        }

        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            Close();
        }
    }
}
