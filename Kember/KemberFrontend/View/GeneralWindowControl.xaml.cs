using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System;

namespace KemberFrontend.View
{
    /// <summary>
    /// Interaction logic for GeneralWindowControl.
    /// </summary>
    public partial class GeneralWindowControl : UserControl
    {
        public static string Token { get; private set; }

#if DEBUG
        public static readonly string UserName = "Max";
#else
        public static readonly string UserName = Environment.MachineName + Environment.UserName;
#endif

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralWindowControl"/> class.
        /// </summary>
        /// 
        public GeneralWindowControl()
        {
            InitializeComponent();
            WebRequest req = WebRequest.CreateHttp("https://localhost:5001/KemberBackModule/Login/" + UserName);
            req.ContentType = "application/json";
            req.Method = "GET";
            try
            {
                WebResponse resp = req.GetResponse();
                using (var streamWriter = new StreamReader(resp.GetResponseStream()))
                {
                    string s = streamWriter.ReadToEnd();
                    if(s == "")
                    {
                        MainFrame.Content = new AutorisationPage(this);
                    }
                    else
                    {
                        Token = s;
                        MainFrame.Content = new MainPage();
                    }
                }
            }
            catch (Exception e) { Console.WriteLine(e.GetType().Name + " " + e.Message); }
        }

        
      
    }
}