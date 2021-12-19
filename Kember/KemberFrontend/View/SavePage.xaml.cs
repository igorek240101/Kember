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

namespace KemberFrontend.View
{
    /// <summary>
    /// Логика взаимодействия для SavePage.xaml
    /// </summary>
    public partial class SavePage : UserControl
    {

        IAutorization autorization;
        public SavePage(IAutorization sender)
        {
            InitializeComponent();
            autorization = sender;
        }

        private void auBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
            GeneralWindowControl.backInput.WriteLine("Save");
            GeneralWindowControl.backInput.WriteLine(tbKey.Text);
            if(GeneralWindowControl.backOutput.ReadLine() == "True")
            {
                MainPage.key = tbKey.Text;
                GeneralWindowControl.winControl.MainFrame.Content = MainPage.page;
            }
            */
            GeneralWindowControl.winControl.MainFrame.Content = autorization;
            autorization.AutorizationResult(tbKey.Text);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GeneralWindowControl.winControl.MainFrame.Content = autorization;
        }
    }
}
