using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
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

        //ListBox listBox;

        //Button play;

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
            string template;
            template = "<ControlTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:x=\"http://schemas.microsoft.com/winfx/2006/xaml\" TargetType=\"Button\">" +
            "<Grid Name=\"Btn\" Height=\"45\">" +
                "<Border>" +
                    "<StackPanel Orientation=\"Horizontal\">" +
                        "<Image Height=\"30\" Width=\"30\" Source=\"D:\\Kember\\Resourse\\slideMenuIcon.png\" Margin=\"20,0,0,0\"/>" +
                        "<Label Content=\"{TemplateBinding Content}\" Margin=\"10,0,0,0\" Background=\"Transparent\" FontSize=\"14\" VerticalAlignment=\"Center\">" +
                            "<Label.Style>" +
                                "<Style TargetType=\"Label\">" +
                                    "<Setter Property=\"Foreground\" Value=\"#FF9D9999\"/>" +
                                    "<Style.Triggers>" +
                                        "<DataTrigger Binding=\"{Binding Path=IsMouseOver, ElementName=Btn}\" Value =\"True\">" +
                                            "<Setter Property=\"Foreground\" Value=\"White\"/>" +
                                        "</DataTrigger>" +

                                        "<DataTrigger Binding=\"{Binding RelativeSource={RelativeSource Mode =FindAncestor, AncestorType ={x:Type Button}}, Path = IsFocused}\" Value=\"True\">" +
                                            "<Setter Property=\"Foreground\" Value=\"White\"/>" +
                                        "</DataTrigger>" +
                                    "</Style.Triggers>" +
                                "</Style>" +
                            "</Label.Style>" +
                        "</Label>" +
                    "</StackPanel>" +
                "</Border>" +
            "<Border Name=\"MouseOverBorder\" Background=\"#FF8D8D8D\">" +
                    "<Border.Style>" +
                        "<Style TargetType=\"Border\">" +
                            "<Setter Property=\"Opacity\" Value=\"0\"/>" +
                            "<Style.Triggers>" +
                                "<DataTrigger Binding=\"{Binding Path=IsMouseOver, ElementName=Btn}\" Value=\"True\">" +
                                    "<Setter Property=\"Opacity\" Value=\"0.1\"/>" +
                                "</DataTrigger>" +
                            "</Style.Triggers>" +
                        "</Style>" +
                    "</Border.Style>" +
                "</Border>" +
                "<Border Name=\"IsSelectedBorder\" Background=\"#FF8D8D8D\">" +
                    "<Border.Style>" +
                        "<Style TargetType=\"Border\">" +
                            "<Setter Property=\"Opacity\" Value=\"0\"/>" +
                            "<Style.Triggers>" +
                                "<DataTrigger Binding=\"{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type Button}}, Path = IsFocused}\" Value=\"True\">" +
                                    "<Setter Property=\"Opacity\" Value=\"0.1\"/>" +
                                "</DataTrigger>" +
                            "</Style.Triggers>" +
                        "</Style>" +
                    "</Border.Style>" +
                "</Border>" +
                "<Border Name=\"IsSelectedBorder2\" Background=\"Transparent\">" +
                    "<Border.Style>" +
                        "<Style TargetType=\"Border\">" +
                            "<Setter Property=\"Visibility\" Value=\"Hidden\"/>" +
                            "<Setter Property=\"BorderThickness\" Value=\"3,0,0,0\"/>" +
                            "<Setter Property=\"BorderBrush\" Value=\"#FF00AEFF\"/>" +
                            "<Style.Triggers>" +
                                "<DataTrigger Binding=\"{Binding RelativeSource={RelativeSource Mode=FindAncestor, AncestorType={x:Type Button}}, Path = IsFocused}\" Value=\"True\">" +
                                    "<Setter Property=\"Visibility\" Value=\"Visible\"/>" +
                                "</DataTrigger>" +
                            "</Style.Triggers>" +
                        "</Style>" +
                    "</Border.Style>" +
                "</Border>" +
                "</Grid>" +
        "</ControlTemplate>";
            foreach (var value in metrics)
            {
                Button button = new Button();
                button.Template = (ControlTemplate)XamlReader.Parse(template);
                button.Content = value.Key;
                button.Click += new RoutedEventHandler(MetricCheck);
                stackPanel.Children.Add(button);
            }
            
            AddBtn.Click += new RoutedEventHandler(Add_Click);
            PlayBtn.Click += new RoutedEventHandler(Button_Click);
            RemoveBtn.Click += new RoutedEventHandler(Remove_Click);
            SaveBtn.Click += new RoutedEventHandler(Save_Click);
            LoadBtn.Click += new RoutedEventHandler(Load_Click);

            //listBox = new ListBox();
            //ListOfFiles.SelectionMode = SelectionMode.Multiple;
            
            
            
            
           
            
            
            
            
            

        }

        public void MetricCheck(object sender, RoutedEventArgs e)
        {
            if (now != null)
            {
                //mainPanel.Children.RemoveAt(1);
            }
            now = metrics[(sender as Button).Content.ToString()];
            //mainPanel.Children.Insert(1, now as UserControl);
            //play.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                GeneralWindowControl.backInput.WriteLine("Invoke");
                string assemblies = "";
                for(int i = 0; i < ListOfFiles.Items.Count-1; i++)
                {
                    assemblies += ListOfFiles.Items[i].ToString() + ((char)0);
                }
                assemblies += ListOfFiles.Items[ListOfFiles.Items.Count - 1];
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
            while (ListOfFiles.SelectedIndex != -1)
            {
                ListOfFiles.Items.RemoveAt(ListOfFiles.SelectedIndex);
            }
        }

        private void FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            foreach (var value in (sender as OpenFileDialog).FileNames)
            {
                ListOfFiles.Items.Add(value);
            }
        }
    }
}
