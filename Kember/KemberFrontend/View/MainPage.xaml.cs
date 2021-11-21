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
using System.IO;
using System.Reflection;

namespace KemberFrontend.View
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : UserControl
    {

        static Dictionary<string, IMetric> metrics = new Dictionary<string, IMetric>();

        IMetric now;


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

        public MainPage()
        {
            InitializeComponent();

            foreach (var value in metrics)
            {
                Button button = new Button();
                button.Content = value.Key;
                button.Click += new RoutedEventHandler(MetricCheck);
                stackPanel.Children.Add(button);
                mainPanel.Children.Add(value.Value as UserControl);
                (value.Value as UserControl).Visibility = Visibility.Hidden;
            }
        }

        public void MetricCheck(object sender, RoutedEventArgs e)
        {
            if(now != null)
            {
                (now as UserControl).Visibility = Visibility.Hidden;
            }
            now = metrics[(sender as Button).Content.ToString()];
            (now as UserControl).Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (now != null)
            {
                (now as UserControl).Visibility = Visibility.Hidden;
                now = null;
            }
        }
    }
}
