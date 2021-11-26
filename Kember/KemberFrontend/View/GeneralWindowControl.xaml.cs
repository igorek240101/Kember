using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace KemberFrontend.View
{
    /// <summary>
    /// Interaction logic for GeneralWindowControl.
    /// </summary>
    public partial class GeneralWindowControl : UserControl
    { 

#if DEBUG
        public static readonly string UserName = "Alex";
#else
        public static readonly string UserName = Environment.UserName;
#endif

        public const string PATH = "D:\\Kember";

        public static StreamWriter backInput;

        public static StreamReader backOutput;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralWindowControl"/> class.
        /// </summary>
        public GeneralWindowControl()
        {
            InitializeComponent();

            string[] file = Directory.GetFiles(PATH, "KemberBackend.exe");
            if (file.Length == 1)
            {
                file = Directory.GetFiles(PATH, "LogDB.db");
                if (file.Length == 1)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = PATH + "\\KemberBackend.exe";
                    process.StartInfo.CreateNoWindow = true;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.Start();
                    backOutput = process.StandardOutput;
                    backInput = process.StandardInput;
                }
            }
            else
            {
                MessageBox.Show("Оишбка");
                throw new Exception();
            }

            backInput.WriteLine("Login");
            backInput.WriteLine(UserName);
            if (backOutput.ReadLine() == "False")
            {
                MainFrame.Content = new AutorisationPage(this);
            }
            else
            {
                MainFrame.Content = new MainPage();
            }

        }

    }
}