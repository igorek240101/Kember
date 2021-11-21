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
    /// Логика взаимодействия для IMetric.xaml
    /// </summary>
    public abstract partial class IMetric : UserControl
    {
        public IMetric()
        {
            InitializeComponent();
            play.Click += new RoutedEventHandler(Invoke);
        }



        public abstract void Invoke(object sender, RoutedEventArgs e);
    }
}
