using System;
using System.Windows;
using System.Windows.Controls;


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
            try
            {
                GeneralWindowControl.backInput.WriteLine("Registration");
                GeneralWindowControl.backInput.WriteLine(GeneralWindowControl.UserName);
                GeneralWindowControl.backInput.WriteLine(tbKey.Text);
                if (GeneralWindowControl.backOutput.ReadLine() == "True")
                {
                    winControl.MainFrame.Content = new MainPage();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.GetType().Name + " " + ex.Message); }
        }

    }
}
