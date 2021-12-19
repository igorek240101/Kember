using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Text;

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

        public static GeneralWindowControl winControl;

        /// <summary>
        /// Initializes a new instance of the <see cref="GeneralWindowControl"/> class.
        /// </summary>
        public GeneralWindowControl()
        {
            winControl = this;

            InitializeComponent();

            string[] file = Directory.GetFiles(PATH, "KemberBackend.exe");
            if (file.Length == 1)
            {
                file = Directory.GetFiles(PATH, "LogDB.db");
                if (file.Length == 1)
                {
                    Process process = new Process();
                    process.StartInfo.FileName = PATH + "\\KemberBackend.exe";
#if DEBUG
                    process.StartInfo.CreateNoWindow = false;
#else
                    process.StartInfo.CreateNoWindow = true;
#endif
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardInput = true;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.StandardOutputEncoding = Encoding.GetEncoding("CP866");
                    process.Start();
                    backOutput = process.StandardOutput;
                    backInput = process.StandardInput;
                }
            }
            else
            {
                MessageBox.Show("Ошибка");
                throw new Exception();
            }

            backInput.WriteLine("Login");
            backInput.WriteLine(UserName);
            if (backOutput.ReadLine() == "False")
            {
                MainFrame.Content = new AutorisationPage();
            }
            else
            {
                MainFrame.Content = new MainPage();
            }

        }

    }
}