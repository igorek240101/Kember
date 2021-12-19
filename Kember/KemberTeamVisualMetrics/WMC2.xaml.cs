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
    /// Логика взаимодействия для WMC2.xaml
    /// </summary>
    public partial class WMC2 : IMetric
    {

        const int min = 6, max = 24;

        List<Label> labels = new List<Label>();
        public WMC2()
        {
            InitializeComponent();
        }

        public string Invoke()
        {
            Flags flags = 0;
            if (checkDelegate.IsChecked == true) flags = flags | Flags.Delegate;
            if (checkEnum.IsChecked == true) flags = flags | Flags.Enum;
            if (checkInterface.IsChecked == true) flags = flags | Flags.Interface;
            if (checkNested.IsChecked == true) flags = flags | Flags.Nested;
            if (checkStatic.IsChecked == true) flags = flags | Flags.StaticMethods;
            if (checkStruct.IsChecked == true) flags = flags | Flags.Struct;
            if (checkPrivate.IsChecked == true) flags = flags | Flags.PrivateMethods;
            return ((int)flags).ToString();
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

        private enum Flags
        {
            StaticClass = 0b00000000001, // NotImplementation
            Delegate = 0b00000000010,
            AnonymousType = 0b00000000100, // NotImplementation
            Struct = 0b00000001000,
            Nested = 0b00000010000,
            Enum = 0b00000100000,
            Interface = 0b00001000000,
            PrivateMethods = 0b00010000000,
            StaticMethods = 0b00100000000,
            Property = 0b01000000000, // NotImplementation
            RegisterAccsessors = 0b10000000000 // NotImplementation
        }
    }
}
