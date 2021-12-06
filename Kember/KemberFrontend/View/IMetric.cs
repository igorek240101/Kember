using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace KemberFrontend.View
{
    public interface IMetric
    {
        string Invoke();

        void SetResult(string arg);
    }

    public static class StaticMetric
    {
        public static void TreeAgrigate(TreeViewItem tree, string s, int minValue, int maxValue)
        {
            switch (s)
            {
                case "Мин":
                    {
                        double mainMin = -1;
                        foreach (TreeViewItem value in tree.Items)
                        {
                            double min = -1;
                            foreach (TreeViewItem type in value.Items)
                            {
                                double num = double.Parse((type.Header as string).Substring((type.Header as string).IndexOf('-')+1));
                                if (min == -1 || min > num) min = num;
                            }
                            value.Header = (value.Header as string).Split('-')[0] + $"-{min}";
                            if (min >= minValue && min <= maxValue)
                            {
                                value.Foreground = new SolidColorBrush(Colors.White);
                            }
                            else
                            {
                                value.Foreground = new SolidColorBrush(Colors.Red);
                            }
                            if (mainMin == -1 || mainMin > min) mainMin = min;
                        }
                        tree.Header = (tree.Header as string).Split('-')[0] + $"-{mainMin}";
                        if (mainMin >= minValue && mainMin <= maxValue)
                        {
                            tree.Foreground = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            tree.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        break;
                    }
                case "Макс":
                    {
                        double mainMax = -1;
                        foreach (TreeViewItem value in tree.Items)
                        {
                            double max = -1;
                            foreach (TreeViewItem type in value.Items)
                            {
                                double num = double.Parse((type.Header as string).Substring((type.Header as string).IndexOf('-') + 1));
                                if (max == -1 || max < num) max = num;
                            }
                            value.Header = (value.Header as string).Split('-')[0] + $"-{max}";
                            if (max >= minValue || max <= maxValue)
                            {
                                value.Foreground = new SolidColorBrush(Colors.White);
                            }
                            else
                            {
                                value.Foreground = new SolidColorBrush(Colors.Red);
                            }
                            if (mainMax == -1 || mainMax < max) mainMax = max;
                        }
                        tree.Header = (tree.Header as string).Split('-')[0] + $"-{mainMax}";
                        if (mainMax >= minValue || mainMax <= maxValue)
                        {
                            tree.Foreground = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            tree.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        break;
                    }
                case "Среднее":
                    {
                        double mainAvg = 0;
                        foreach (TreeViewItem value in tree.Items)
                        {
                            double avg = 0;
                            foreach (TreeViewItem type in value.Items)
                            {
                                double num = double.Parse((type.Header as string).Substring((type.Header as string).IndexOf('-') + 1));
                                avg += num;
                            }
                            value.Header = (value.Header as string).Split('-')[0] + $"-{avg / value.Items.Count}";
                            if (avg / value.Items.Count >= minValue || avg / value.Items.Count <= maxValue)
                            {
                                value.Foreground = new SolidColorBrush(Colors.White);
                            }
                            else
                            {
                                value.Foreground = new SolidColorBrush(Colors.Red);
                            }
                            mainAvg += avg / value.Items.Count;
                        }
                        tree.Header = (tree.Header as string).Split('-')[0] + $"-{mainAvg / tree.Items.Count}";
                        if (mainAvg / tree.Items.Count >= minValue || mainAvg / tree.Items.Count <= maxValue)
                        {
                            tree.Foreground = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            tree.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        break;
                    }
                case "Мат Ожидание":
                    {
                        double mainM = 0;
                        int count = 0;
                        foreach (TreeViewItem value in tree.Items)
                        {
                            double M = 0;
                            foreach (TreeViewItem type in value.Items)
                            {
                                double num = double.Parse((type.Header as string).Substring((type.Header as string).IndexOf('-') + 1));
                                M += num;
                                mainM += num;
                                count++;
                            }
                            value.Header = (value.Header as string).Split('-')[0] + $"-{M / value.Items.Count}";
                            if (M / value.Items.Count >= minValue || M / value.Items.Count <= maxValue)
                            {
                                value.Foreground = new SolidColorBrush(Colors.White);
                            }
                            else
                            {
                                value.Foreground = new SolidColorBrush(Colors.Red);
                            }
                        }
                        tree.Header = (tree.Header as string).Split('-')[0] + $"-{mainM / count}";
                        if (mainM / count >= minValue || mainM / count <= maxValue)
                        {
                            tree.Foreground = new SolidColorBrush(Colors.White);
                        }
                        else
                        {
                            tree.Foreground = new SolidColorBrush(Colors.Red);
                        }
                        break;
                    }
                case "Дисперсия":
                    {
                        double mainM = 0;
                        int count = 0;
                        double mainSM = 0;
                        foreach (TreeViewItem value in tree.Items)
                        {
                            double M = 0;
                            double SM = 0;
                            foreach (TreeViewItem type in value.Items)
                            {
                                double num = double.Parse((type.Header as string).Substring((type.Header as string).IndexOf('-') + 1));
                                M += num * num;
                                mainM += num * num;
                                mainSM += num;
                                SM += num;
                                count++;
                            }
                            double D = (M / value.Items.Count) - ((SM / value.Items.Count) * (SM / value.Items.Count));
                            value.Header = (value.Header as string).Split('-')[0] + $"-{D}";
                            if (D >= minValue || D <= maxValue)
                            {
                                value.Foreground = new SolidColorBrush(Colors.White);
                            }
                            else
                            {
                                value.Foreground = new SolidColorBrush(Colors.Red);
                            }
                        }
                        double Dis = (mainM / count) - ((mainSM / count) * (mainSM / count));
                        tree.Header = (tree.Header as string).Split('-')[0] + $"-{Dis}";
                        tree.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    }
                case "СКО":
                    {
                        double mainM = 0;
                        int count = 0;
                        double mainSM = 0;
                        foreach (TreeViewItem value in tree.Items)
                        {
                            double M = 0;
                            double SM = 0;
                            foreach (TreeViewItem type in value.Items)
                            {
                                double num = double.Parse((type.Header as string).Substring((type.Header as string).IndexOf('-') + 1));
                                M += num * num;
                                mainM += num * num;
                                mainSM += num;
                                SM += num;
                                count++;
                            }
                            double D = (M / value.Items.Count) - ((SM / value.Items.Count) * (SM / value.Items.Count));
                            value.Header = (value.Header as string).Split('-')[0] + $"-{Math.Sqrt(D)}";
                            value.Foreground = new SolidColorBrush(Colors.White);
                        }
                        double Dis = (mainM / count) - ((mainSM / count) * (mainSM / count));
                        tree.Header = (tree.Header as string).Split('-')[0] + $"-{Math.Sqrt(Dis)}";
                        tree.Foreground = new SolidColorBrush(Colors.White);
                        break;
                    }
            }
        }
    }
}
