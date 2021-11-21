using System.Diagnostics.CodeAnalysis;
using System.Windows;
using System.Windows.Controls;
using System.Net;
using System.IO;
using System;
using System.Diagnostics;
using System.Collections.Generic;

namespace KemberFrontend.View
{
    /// <summary>
    /// Interaction logic for GeneralWindowControl.
    /// </summary>
    public partial class GeneralWindowControl : UserControl
    {
        public static string Token { get; private set; }

#if DEBUG
        public static readonly string UserName = "Igor";
#else
        public static readonly string UserName = Environment.MachineName + Environment.UserName;
#endif

        public const string PATH = "D:\\Kember";

        public string uri;

        Process server;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralWindowControl"/> class.
        /// </summary>
        public GeneralWindowControl()
        {
            InitializeComponent();

            string[] file = Directory.GetFiles(PATH, "KemberServer.exe");
            if(file.Length == 1)
            {
                file = Directory.GetFiles(PATH, "LogDB.db");
                if (file.Length == 1)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = PATH + "\\KemberServer.exe";
                    //process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    StreamReader reader = process.StandardOutput;
                    List<string> s = new List<string>();
                    for(int i = 0; i < 10; i++) reader.ReadLine();
                    uri = "https://localhost:5001";
                }
            }
            else
            {
                file = Directory.GetFiles(PATH, "uri.txt");
            }

            WebRequest req = WebRequest.CreateHttp(uri + "/KemberBackModule/Login/" + UserName);
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