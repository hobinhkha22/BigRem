using log4net;
using Microsoft.Win32;
using RememberUtility.Constant;
using RememberUtility.Enum;
using RememberUtility.Extension;
using RememberUtility.HandleUtil;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WPFBigRemGUI.Entertainment
{
    /// <summary>
    /// Interaction logic for EntertainemntMain.xaml
    /// </summary>
    public partial class EntertainemntMain : Window
    {
        private string GetStringValue;
        private EntertainmentUtil entertainmentUtil;
        private static readonly ILog Logs = LogManager.GetLogger(typeof(EntertainemntMain));
        public EntertainemntMain()
        {
            LoggerUtil.HandleLogPath();
            Logs.Info($"[WPFBigRemGUI.Entertainment] Starting Entertainment wpf gui.");
            InitializeComponent();
            entertainmentUtil = new EntertainmentUtil();
            txtEtName.Text = "";
            txtLink.Text = "";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            var listConstantValue = typeof(CategoriesEntertainmentConstant).GetAllPublicConstantValues<string>();
            listConstantValue.Sort();
            lstListEtCategory.ItemsSource = listConstantValue;

            // Disable resize
            ResizeMode = ResizeMode.CanMinimize;

            // Backup Database
            DoBackupAsync();
        }

        private async void DoBackupAsync()
        {
            await Task.Run(() =>
            {
                // Backup Database
                if (entertainmentUtil.GetListEntertainments() != null)
                {
                    entertainmentUtil.BackupDatabase(EnumFileConstant.ENTERTAINMENTCONSTAT, FileConstant.BackUpDb);
                }
            });
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var et = new RememberUtility.Model.Entertainment();

            // txtEtName
            if (txtEtName.Text != "")
            {
                et.EnterName = txtEtName.Text.Trim();
            }
            else
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "et name cannot be null. Please choose a type";
            }

            // txtLink
            if (txtLink.Text != "")
            {
                et.Links = txtLink.Text.Trim();
            }
            else
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "Links cannot be null. Please choose a type";
            }

            if (lstListEtCategory.SelectedIndex == -1)
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "Category aren't choose. Please choose a type";
            }

            if (txtLink.Text != "" && txtEtName.Text != "" && lstListEtCategory.SelectedIndex >= 0)
            {
                // use dynamic as type to cast your anonymous object to
                dynamic categoryInList = (lstListEtCategory.SelectedItem);

                et.Category = categoryInList as string;

                var findet = entertainmentUtil.FindEntertainmentBy(txtEtName.Text);
                if (findet != null)
                {
                    if (et.EnterName.ToLower() != findet.EnterName.ToLower())
                    {
                        et.Links.Trim();
                        entertainmentUtil.AddEntertainment(et);
                        AddEtResult.Foreground = Brushes.Green;

                        if (et.EnterName.Length <= 10)
                        {
                            AddEtResult.Content = $"Add '{et.EnterName}' Successful";
                        }
                        else
                        {
                            AddEtResult.Content = $"Add et Successful";
                        }

                        txtEtName.Text = string.Empty;
                        txtLink.Text = string.Empty;
                        lstListEtCategory.Text = string.Empty;
                    }
                    else
                    {
                        AddEtResult.Foreground = Brushes.Red;
                        AddEtResult.Content = $"'{txtEtName.Text}' duplicate. Add failed.";
                    }
                }
                else
                {
                    entertainmentUtil.AddEntertainment(et);
                    AddEtResult.Foreground = Brushes.Green;
                    AddEtResult.Content = "Add et Successful";

                    txtEtName.Text = string.Empty;
                    txtLink.Text = string.Empty;
                    lstListEtCategory.Text = string.Empty;
                }
            }
        }

        private void btnUpdateEt_Click(object sender, RoutedEventArgs e)
        {
            var updateEt = new UpdateEt();
            Close();
            updateEt.Show();
        }

        private void Find_Click(object sender, RoutedEventArgs e)
        {
            var find = new FindEt();
            find.ShowDialog();

            // get data                        
            var resultEt = entertainmentUtil.FindEntertainmentBy(GetStringValue);
            if (resultEt != null)
            {
                txtEtName.Text = resultEt.EnterName;
                txtLink.Text = resultEt.Links;
                lstListEtCategory.Text = resultEt.Category;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var et = new RememberUtility.Model.Entertainment();

            // txtEtName
            if (txtEtName.Text != "")
            {
                et.EnterName = txtEtName.Text;
            }
            else
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "Et name cannot be null. Please choose a type";
            }

            if (txtEtName.Text == "")
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "You must input Et name";
            }
            else // has input
            {
                var findEt = entertainmentUtil.FindEntertainmentBy(txtEtName.Text);
                if (findEt != null)
                {
                    if (et.EnterName.ToLower() == findEt.EnterName.ToLower())
                    {
                        if (MessageBox.Show($"Found '{txtEtName.Text}'." +
                            $" Do you wanna to delete '{txtEtName.Text}'", "Confirm delete",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            entertainmentUtil.DeleteEntertainment(et.EnterName);
                            AddEtResult.Foreground = Brushes.Green;
                            AddEtResult.Content = $"Delete '{txtEtName.Text}' Successful";

                            txtEtName.Text = string.Empty;
                            txtLink.Text = string.Empty;
                            lstListEtCategory.Text = string.Empty;
                        }
                        else
                        {
                            AddEtResult.Foreground = Brushes.YellowGreen;
                            AddEtResult.Content = $"Delete operate was canceled.";
                        }
                    }
                } // input and not found
                else
                {
                    AddEtResult.Foreground = Brushes.Red;
                    AddEtResult.Content = $" Cannot find '{txtEtName.Text}'. Delete failed.";
                }
            }
        }

        public void GetName(string EtName)
        {
            GetStringValue = EtName;
        }

        private void btnListEt_Click(object sender, RoutedEventArgs e)
        {
            new ListEntertainment().Show();
            Close();
        }

        private void btnBackMain_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

            if (mnuAlwaysOnTop.IsChecked)
            {
                mnuAlwaysOnTop.IsCheckable = true;
                Topmost = true;
                Activate();
            }
            else
            {
                Topmost = false;
                Activate();
            }
        }

        private void mnuItemExport_Click(object sender, RoutedEventArgs e)
        {
            var saveFile = new SaveFileDialog
            {
                Filter = "File type (*.xlsx)|*.xlsx",
                AddExtension = true
            };
            var tableName = FileConstant.Entertainment;
            var filePath = "";

            if (saveFile.ShowDialog().Value == true)
            {
                filePath = Path.GetFullPath(saveFile.FileName);
                entertainmentUtil.SaveFileTo(filePath, tableName);

                AddEtResult.Foreground = Brushes.Green;
                AddEtResult.Content = "Export successful.";
            }
            else
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "Export failed.";
            }
        }
    }
}
