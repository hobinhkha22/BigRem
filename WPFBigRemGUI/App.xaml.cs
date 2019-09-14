using System;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Navigation;

namespace WPFBigRemGUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var proc = Process.GetCurrentProcess();
            var count = Process.GetProcesses().Where(p => p.ProcessName
                            == proc.ProcessName).Count();
            if (count > 1)
            {
                MessageBox.Show("An already instance is running...", "Warning",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                Current.Shutdown();
            }
        }
    }
}