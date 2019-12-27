using ConnectionSampleCode.Constant;
using RememberUtility.Constant;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ShowOutputET.xaml
    /// </summary>
    public partial class ShowOutputET : Window
    {
        private EntertainmentUtil entertainmentUtil;
        public ShowOutputET()
        {
            InitializeComponent();
            entertainmentUtil = new EntertainmentUtil();
            showOutputSSH.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
            showOutputSSH.IsReadOnly = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        public async void GetCriteriaAsync(string[] multiCommand)
        {
            using (var client = new SshClient(SSHRemoteConstant.Server, SSHRemoteConstant.UserName, SSHRemoteConstant.Password))
            {
                if (!HandleRandom.IsNullOrEmpty(multiCommand))
                {
                    showOutputSSH.Text = "Connecting...";
                    Thread.Sleep(1200);

                    // 1. Connection
                    await Task.Run(() => client.Connect());
                    if (client.IsConnected)
                    {
                        Thread.Sleep(1200);
                        showOutputSSH.Text += "\n";
                        showOutputSSH.Text += "Connected...";

                        // 2. Write file
                        //var getFullList = entertainmentUtil.GetListEntertainments();
                        //HandleRandom.ExportExcel(
                        //getFullList as List<RememberUtility.Model.Entertainment>, FileConstant.Entertainment, "/home/pi/Documents/file.xlsx");

                        foreach (var subCommand in multiCommand)
                        {
                            if (!string.IsNullOrEmpty(subCommand))
                            {
                                await Task.Run(() => client.RunCommand(subCommand));
                                Thread.Sleep(900);
                                showOutputSSH.Text += "\n";
                                showOutputSSH.Text += $"Running Command: { subCommand }";
                            }
                        }
                    }

                    showOutputSSH.Text += "\n";
                    showOutputSSH.Text += "Disconnecting...";
                    Thread.Sleep(1200);
                    await Task.Run(() => client.Disconnect());
                    if (!client.IsConnected)
                    {
                        Thread.Sleep(1200);
                        showOutputSSH.Text += "\n";
                        showOutputSSH.Text += "Disconnected...";
                    }
                }
                else
                {
                    showOutputSSH.Text += "The command not found...";
                    showOutputSSH.Text += "\n";
                }
            }
        }
    }
}
