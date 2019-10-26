using ConnectionSampleCode.HandleUtil;
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
using System.Windows.Input;
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
            txtAuthorEnter.Text = "";
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            Application.Current.ShutdownMode = ShutdownMode.OnLastWindowClose;

            lstListEtCategory.Visibility = Visibility.Visible;
            txtBtnCustomTextBox.Visibility = Visibility.Hidden;
            btnCancelCustom.Visibility = Visibility.Hidden;

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
                    entertainmentUtil.BackupDatabase(EnumFileConstant.ENTERTAINMENTCONSTANT, FileConstant.BackUpDb);
                    // Backup by zipfile                
                    ZipBackupFiles.ZipFile(EnumFileConstant.ENTERTAINMENT);
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

            // txtAuthorEnter
            if (txtAuthorEnter.Text != "")
            {
                et.AuthorEnter = txtAuthorEnter.Text.Trim();
            }
            else
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "Author Enter cannot be null. Please choose a type";
            }

            if (lstListEtCategory.SelectedIndex == -1)
            {
                AddEtResult.Foreground = Brushes.Red;
                AddEtResult.Content = "Category aren't choose. Please choose a type";
            }

            if (txtLink.Text != "" && txtEtName.Text != "" && txtAuthorEnter.Text != "" && lstListEtCategory.SelectedIndex >= 0)
            {
                dynamic categoryInList = (lstListEtCategory.SelectedItem);
                string catchCustom = categoryInList as string;
                if (catchCustom.ToString() == CategoriesEntertainmentConstant.Custom)
                {
                    if (txtBtnCustomTextBox.Text == "")
                    {
                        AddEtResult.Foreground = Brushes.Red;
                        AddEtResult.Content = "Category custom is emtpy. Please pick one.";
                        return;
                    }
                    else
                    {
                        et.Category = txtBtnCustomTextBox.Text;
                    }
                }
                else
                {
                    // use dynamic as type to cast your anonymous object to
                    categoryInList = (lstListEtCategory.SelectedItem);

                    // add category here
                    et.Category = categoryInList as string;
                }

                var findet = entertainmentUtil.FindEntertainmentBy(txtEtName.Text);
                if (findet != null)
                {
                    if (et.EnterName.ToLower() != findet.EnterName.ToLower())
                    {
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
                        txtAuthorEnter.Text = string.Empty;
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
                    txtAuthorEnter.Text = string.Empty;
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
                txtAuthorEnter.Text = resultEt.AuthorEnter;
                lstListEtCategory.Text = resultEt.Category;
            }

            var resultLink = entertainmentUtil.FindEntertainmentByLink(GetStringValue);
            if (resultLink != null)
            {
                txtEtName.Text = resultLink.EnterName;
                txtLink.Text = resultLink.Links;
                txtAuthorEnter.Text = resultLink.AuthorEnter;
                lstListEtCategory.Text = resultLink.Category;
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
                            $" Do you wanna delete '{txtEtName.Text}'", "Confirm delete",
                            MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                        {
                            entertainmentUtil.DeleteEntertainment(et.EnterName);
                            AddEtResult.Foreground = Brushes.Green;
                            AddEtResult.Content = $"Delete '{txtEtName.Text}' Successful";

                            txtEtName.Text = string.Empty;
                            txtLink.Text = string.Empty;
                            txtAuthorEnter.Text = string.Empty;
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

        private void Spacebar_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e != null && e.Key == Key.Space)
            {
                lstListEtCategory.IsDropDownOpen = true;
            }
        }

        private void btnCancelCustom_Click(object sender, RoutedEventArgs e)
        {
            btnCancelCustom.Visibility = Visibility.Hidden;
            txtBtnCustomTextBox.Visibility = Visibility.Hidden;

            lstListEtCategory.Visibility = Visibility.Visible;
            lstListEtCategory.Text = string.Empty;
        }

        private void lstListEtCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // use dynamic as type to cast your anonymous object to
            dynamic categoryInList = (lstListEtCategory.SelectedItem);

            btnCancelCustom.Visibility = Visibility.Hidden;
            txtBtnCustomTextBox.Visibility = Visibility.Hidden;
            string catchCustom = categoryInList as string;

            if (catchCustom == null)
            {
                return;
            }

            if (catchCustom.ToString() == CategoriesEntertainmentConstant.Custom)
            {
                lstListEtCategory.Visibility = Visibility.Hidden;

                btnCancelCustom.Visibility = Visibility.Visible;
                txtBtnCustomTextBox.Visibility = Visibility.Visible;
                txtBtnCustomTextBox.Text = string.Empty;
            }            
        }
    }
}
