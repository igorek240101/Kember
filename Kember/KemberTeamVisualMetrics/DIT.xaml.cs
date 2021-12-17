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
using KemberFrontend.View;

namespace KemberTeamVisualMetrics
{
    /// <summary>
    /// Логика взаимодействия для DIT.xaml
    /// </summary>
    public partial class DIT : IMetric
    {
        List<Label> labels = new List<Label>();

        const int min = 3, max = 7;

        public DIT()
        {
            InitializeComponent();
        }

        public string Invoke()
        {
            return "";
        }

        public void SetResult(string arg)
        {
            treeView.Items.Clear();
            string[] strs = arg.Split((char)1);
            TreeViewItem metric = new TreeViewItem();
            metric.Foreground = new SolidColorBrush(Colors.White);
            metric.Header = GetType().Name;
            treeView.Items.Add(metric);
            for (int i = 0; i < strs.Length; i++)
            {
                TreeViewItem assembly = new TreeViewItem();
                assembly.Foreground = new SolidColorBrush(Colors.White);
                string[] ass = strs[i].Split('\0');
                assembly.Header = ass[0];
                metric.Items.Add(assembly);
                for (int j = 1; j < ass.Length; j++)
                {
                    TreeViewItem type = new TreeViewItem();
                    int num = int.Parse(ass[j].Split('-')[1]);
                    if (num >= min && num <= max)
                    {
                        type.Foreground = new SolidColorBrush(Colors.White);
                    }
                    else
                    {
                        type.Foreground = new SolidColorBrush(Colors.Red);
                    }
                    type.Header = ass[j];
                    assembly.Items.Add(type);
                }
            }
            comboBox.IsEnabled = true;
            comboBox.SelectedIndex = 0;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StaticMetric.TreeAgrigate(treeView.Items[0] as TreeViewItem, (comboBox.SelectedItem as ComboBoxItem).Content as string, min, max);
        }
    }
}
