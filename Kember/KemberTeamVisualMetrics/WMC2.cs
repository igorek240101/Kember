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
using System.Reflection;

namespace KemberTeamVisualMetrics
{
    /// <summary>
    /// Логика взаимодействия для UserControl1.xaml
    /// </summary>
    public partial class WMC : IMetric
    {
        List<Label> labels = new List<Label>();
        public WMC()
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
            while(labels.Count > 0)
            {
                grid.Children.Remove(labels[0]);
                labels.RemoveAt(0);
            }
            string[] strs = arg.Split('\0');
            for (int i = 0; i < strs.Length; i++)
            {
                Label label = new Label();
                label.Foreground = checkDelegate.Foreground;
                label.Margin = new Thickness(300, 30 + i * 20, 0, 0);
                grid.Children.Add(label);
                labels.Add(label);
                label.Content = strs[i];
            }
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
