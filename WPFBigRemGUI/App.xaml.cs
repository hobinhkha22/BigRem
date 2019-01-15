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


            //    try
            //    {
            //        //First get the 'user-scoped' storage information location reference in the assembly
            //        var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
            //        //create a stream reader object to read content from the created isolated location
            //        var srReader = new StreamReader(new IsolatedStorageFileStream("isotest", FileMode.OpenOrCreate, isolatedStorage));

            //        //Open the isolated storage
            //        if (srReader == null)
            //        {
            //            MessageBox.Show("No Data stored!");
            //        }
            //        else
            //        {                    
            //            while (!srReader.EndOfStream)
            //            {
            //                string item = srReader.ReadLine();
            //                MessageBox.Show(item);
            //            }
            //        }
            //        //close reader
            //        srReader.Close();
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show(ex.Message);
            //        throw;
            //    }
        }

        //protected override void OnExit(ExitEventArgs e)
        //{
        //    try
        //    {
        //        //First get the 'user-scoped' storage information location reference in the assembly
        //        var isolatedStorage = IsolatedStorageFile.GetUserStoreForAssembly();
        //        //create a stream writer object to write content in the location
        //        var srWriter = new StreamWriter(new IsolatedStorageFileStream("isotest", FileMode.Create, isolatedStorage));
        //        //check the Application property collection contains any values.

        //        if (App.Current.Properties[0] != null)
        //        {
        //            //wriet to the isolated storage created in the above code section.
        //            srWriter.WriteLine(App.Current.Properties[0].ToString()); // username
        //        }

        //        if (App.Current.Properties[1] != null)
        //        {
        //            srWriter.WriteLine(App.Current.Properties[1].ToString()); // checkbox remember me
        //        }

        //        srWriter.Flush();
        //        srWriter.Close();
        //    }
        //    catch (System.Security.SecurityException sx)
        //    {
        //        MessageBox.Show(sx.Message);                
        //    }
        //}


    }
}
