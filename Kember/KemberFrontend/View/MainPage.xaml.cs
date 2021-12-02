using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace KemberFrontend.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {
        static Dictionary<string, IMetric> metrics = new Dictionary<string, IMetric>();

        IMetric now;

        ListBox listBox;

        Button play;

        public static MainPage page;

        static MainPage()
        {
            string modulePath = GeneralWindowControl.PATH + @"\Module";
            try
            {
                string[] dlls = Directory.GetFiles(modulePath, "*.dll");
                for (int i = 0; i < dlls.Length; i++)
                {
                    Type[] types = null;
                    Assembly assembly = Assembly.LoadFrom(dlls[i]);
                    try
                    {
                        types = assembly.GetTypes();
                    }
                    catch (ReflectionTypeLoadException e) { types = e.Types.Where(t => t != null).ToArray(); }
                    for (int j = 0; j < types.Length; j++)
                    {
                        if (!types[j].IsAbstract && !types[j].IsEnum && types[j].IsClass && types[j].IsSubclassOf(typeof(UserControl)) && types[j].GetInterface(typeof(IMetric).Name) != null)
                        {
                            metrics.Add(types[j].Name, types[j].GetConstructor(new Type[0]).Invoke(new object[0]) as IMetric);
                        }
                    }
                }
            }
            catch { }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="winControl"></param>
        public MainPage()
        {

            InitializeComponent();

            foreach (var value in metrics)
            {
                OurButton button = new OurButton();
                button.Content = value.Key;
                button.Click += new RoutedEventHandler(MetricCheck);
                stackPanel.Children.Add(button);
            }
            play = new Button();
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("D:\\Kember\\Resourse\\play.png"));
            play.Content = image;
            play.Height = 50;
            play.Width = 50;
            play.Margin = new Thickness(25, 300, 25, 0);
            play.Click += new RoutedEventHandler(Button_Click);
            play.Visibility = Visibility.Hidden;
            mainPanel.Children.Add(play);
            listBox = new ListBox();
            listBox.SelectionMode = SelectionMode.Multiple;
            mainPanel.Children.Add(listBox);
            Button add = new Button();
            image = new Image();
            image.Source = new BitmapImage(new Uri("D:\\Kember\\Resourse\\add.png"));
            add.Content = image;
            add.Height = 50;
            add.Width = 50;
            add.Margin = new Thickness(25, 300, 25, 0);
            add.Click += new RoutedEventHandler(Add_Click);
            mainPanel.Children.Add(add);
            Button remove = new Button();
            image = new Image();
            image.Source = new BitmapImage(new Uri("D:\\Kember\\Resourse\\remove.png"));
            remove.Content = image;
            remove.Height = 50;
            remove.Width = 50;
            remove.Margin = new Thickness(25, 400, 25, 0);
            remove.Click += new RoutedEventHandler(Remove_Click);
            mainPanel.Children.Add(remove);
            Button save = new Button();
            image = new Image();
            image.Source = new BitmapImage(new Uri("D:\\Kember\\Resourse\\save.png"));
            save.Content = image;
            save.Height = 50;
            save.Width = 50;
            save.Margin = new Thickness(25,500, 25, 0);
            save.Click += new RoutedEventHandler(Save_Click);
            mainPanel.Children.Add(save);
            Button load = new Button();
            image = new Image();
            image.Source = new BitmapImage(new Uri("D:\\Kember\\Resourse\\load.png"));
            load.Content = image;
            load.Height = 50;
            load.Width = 50;
            load.Margin = new Thickness(25, 600, 25, 0);
            load.Click += new RoutedEventHandler(Load_Click);
            mainPanel.Children.Add(load);
        }

        public void MetricCheck(object sender, RoutedEventArgs e)
        {
            if (now != null)
            {
                mainPanel.Children.RemoveAt(1);
            }
            now = metrics[(sender as Button).Content.ToString()];
            mainPanel.Children.Insert(1, now as UserControl);
            play.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GeneralWindowControl.backInput.WriteLine("Invoke");
                string assemblies = "";
                for(int i = 0; i < listBox.Items.Count-1; i++)
                {
                    assemblies += listBox.Items[i].ToString() + ((char)0);
                }
                assemblies += listBox.Items[listBox.Items.Count - 1];
                GeneralWindowControl.backInput.WriteLine(assemblies);
                GeneralWindowControl.backInput.WriteLine(now.Invoke());
                GeneralWindowControl.backInput.WriteLine(now.GetType().Name);
                string s = GeneralWindowControl.backOutput.ReadLine();
                now.SetResult(s);
            }
            catch (Exception ex) { Console.WriteLine(ex.GetType().Name + " " + ex.Message); }
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.DefaultExt = "dll";
            fileDialog.Multiselect = true;
            fileDialog.FileOk += new System.ComponentModel.CancelEventHandler(FileOk);
            fileDialog.ShowDialog();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            GeneralWindowControl.winControl.MainFrame.Content = new SavePage();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            while (listBox.SelectedIndex != -1)
            {
                listBox.Items.RemoveAt(listBox.SelectedIndex);
            }
        }

        private void FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var value in (sender as OpenFileDialog).FileNames)
            {
                listBox.Items.Add(value);
            }
        }
    }
}
