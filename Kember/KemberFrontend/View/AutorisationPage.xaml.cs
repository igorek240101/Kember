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
using System.Text.Json;
using System.Net;
using System.IO;


namespace KemberFrontend.View
{
    /// <summary>
    /// Логика взаимодействия для AutorisationPage.xaml
    /// </summary>
    public partial class AutorisationPage : UserControl
    {
        private GeneralWindowControl winControl;
        public AutorisationPage(GeneralWindowControl winControl)
        {
            InitializeComponent();
            this.winControl = winControl;
        }


        private void auBtn_Click(object sender, RoutedEventArgs e)
        {
            WebRequest req = WebRequest.CreateHttp("https://localhost:5001/KemberBackModule/Registration/");
            req.ContentType = "application/json";
            req.Method = "POST";
            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(new { Name = GeneralWindowControl.UserName, Key = tbKey.Text}));
            }
            try
            {
                WebResponse resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string s = streamWriter.ReadToEnd();
                    if (s != "")
                    {
                        winControl.MainFrame.Content = new MainPage();
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.GetType().Name + " " + ex.Message); }
        }

    }
}
