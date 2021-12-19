using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace KemberFrontend.View
{
    /// <summary>
    /// Логика взаимодействия для LoadPage.xaml
    /// </summary>
    public partial class LoadPage : UserControl, IAutorization
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        public LoadPage()
        {
            InitializeComponent();
        }

        public void Invoke()
        {
            if (MainPage.key == null)
            {
                GeneralWindowControl.winControl.MainFrame.Content = new SavePage(this);
            }
            else
            {
                GeneralWindowControl.backInput.WriteLine("Loading");
                GeneralWindowControl.backInput.WriteLine(MainPage.key);
                string res = GeneralWindowControl.backOutput.ReadLine();
                if (res != "")
                {
                    string[] s = res.Split((char)2);
                    foreach (var value in s)
                    {
                        string[] subs = value.Split('\0');
                        try
                        {
                            dictionary.Add(subs[1], int.Parse(subs[0]));
                            listbox.Items.Add(subs[1]);
                        }
                        catch { }
                    }
                }
            }
        }

        public void AutorizationResult(string key)
        {
            MainPage.key = key;
            GeneralWindowControl.backInput.WriteLine("Loading");
            GeneralWindowControl.backInput.WriteLine(MainPage.key);
            string res = GeneralWindowControl.backOutput.ReadLine();
            if (res != "")
            {
                string[] s = res.Split((char)2);
                foreach (var value in s)
                {
                    string[] subs = value.Split('\0');
                    try
                    {
                        dictionary.Add(subs[1], int.Parse(subs[0]));
                        listbox.Items.Add(subs[1]);
                    }
                    catch { }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (listbox.SelectedItem != null)
            {
                GeneralWindowControl.backInput.WriteLine("Load");
                GeneralWindowControl.backInput.WriteLine(MainPage.key);
                GeneralWindowControl.backInput.WriteLine(dictionary[listbox.SelectedItem as string]);
                string s = GeneralWindowControl.backOutput.ReadLine();
                GeneralWindowControl.winControl.MainFrame.Content = MainPage.page;
                MainPage.page.Loading((listbox.SelectedItem as string).Split('-')[0], s);
            }
        }

        private void backBtn_Click(object sender, RoutedEventArgs e)
        {
            GeneralWindowControl.winControl.MainFrame.Content = MainPage.page;

        }
    }
}
