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
    /// Логика взаимодействия для NOC.xaml
    /// </summary>
    public partial class NOC : IMetric
    {
        List<Label> labels = new List<Label>();
        public NOC()
        {
            InitializeComponent();
        }

        public string Invoke()
        {
            return "";
        }

        public void SetResult(string arg)
        {
            while (labels.Count > 0)
            {
                grid.Children.Remove(labels[0]);
                labels.RemoveAt(0);
            }
            string[] strs = arg.Split('\0');
            for (int i = 0; i < strs.Length; i++)
            {
                Label label = new Label();
                label.Margin = new Thickness(300, 30 + i * 20, 0, 0);
                grid.Children.Add(label);
                labels.Add(label);
                label.Content = strs[i];
            }
        }
    }
}
