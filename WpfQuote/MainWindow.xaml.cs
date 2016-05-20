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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;
using BasicallyMe.RobinhoodNet;

namespace WpfApplication1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();


            TaskbarItemInfo taskbarItemInfo = new TaskbarItemInfo();
            taskbarItemInfo.ProgressValue = .5;
            taskbarItemInfo.ProgressState = TaskbarItemProgressState.Normal;
        }

        private async void Speak(object sender, System.Windows.RoutedEventArgs e)
        {

            TaskbarItemInfo taskbarItemInfo = new TaskbarItemInfo();
            taskbarItemInfo.ProgressValue = .5;
            taskbarItemInfo.ProgressState = TaskbarItemProgressState.Paused;

            var args = new[] { "RgQuote", "F" }; // string[]

            var rh = new RobinhoodClient();

            await authenticate(rh);

            while (1 < 2)
            {
                var quotes = await rh.DownloadQuote(args);

                //Console.WriteLine(DateTime.Now);  
                foreach (var q in quotes)
                {
                   
               
                if (q != null)
                    {
                        textToSpeech.Text = q.LastTradePrice.ToString();
                    }

                 }
            }

        }
        private void PART_CLOSE_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PART_MINIMIZE_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = System.Windows.WindowState.Minimized;
        }
        private void PART_MAXIMIZE_RESTORE_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == System.Windows.WindowState.Normal)
            {
                this.WindowState = System.Windows.WindowState.Maximized;
            }
            else
            {
                this.WindowState = System.Windows.WindowState.Normal;
            }
        }
        private void PART_TITLEBAR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }


        static readonly string __tokenFile = System.IO.Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            "RobinhoodNet",
            "token");



        static async Task authenticate(RobinhoodClient client)
        {
            if (System.IO.File.Exists(__tokenFile))
            {
                var token = System.IO.File.ReadAllText(__tokenFile);
                await client.Authenticate(token);
            }
            else
            {
                //Console.Write("username: ");
                //string userName = Console.ReadLine();

                //Console.Write("password: ");
                //string password = getConsolePassword();

                //await client.Authenticate(userName, password);

                //System.IO.Directory.CreateDirectory(
                //    System.IO.Path.GetDirectoryName(__tokenFile));

                //System.IO.File.WriteAllText(__tokenFile, client.AuthToken);
            }
        }
    }
}
