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

        public WMC()
        {
            InitializeComponent();
        }

        public object Invoke()
        {
            Flags flags = 0;
            if (checkDelegate.IsChecked == null) flags = flags | Flags.Delegate;
            if (checkEnum.IsChecked == null) flags = flags | Flags.Enum;
            if (checkInterface.IsChecked == null) flags = flags | Flags.Interface;
            if (checkNested.IsChecked == null) flags = flags | Flags.Nested;
            if (checkStatic.IsChecked == null) flags = flags | Flags.StaticMethods;
            if (checkStruct.IsChecked == null) flags = flags | Flags.Struct;
            if (checkPrivate.IsChecked == null) flags = flags | Flags.PrivateMethods;
            return flags;
        }

        public void SetResult(object arg)
        {
            Label label = new Label();
            label.Foreground = checkDelegate.Foreground;
            label.Content = "Result";
            label.Margin = new Thickness(500, 30, 0, 0);
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
