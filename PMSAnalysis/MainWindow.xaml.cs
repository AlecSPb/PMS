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
using PMSAnalysis.Models;
using LiveCharts.Wpf;
using LiveCharts;

namespace PMSAnalysis
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private double rectSize = 20;

        private double EffectiveVHPDay = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DateTime start = DateTime.Parse("2020-7-1");
            DateTime end = DateTime.Parse("2021-5-10");


            //check cache file

            var h = new Services.AnalysisHelper();
            var rr = h.GetAnalysis(start, end);

            //save cache file

            double currenttop = 30;

            double left = 0;
            double top = currenttop;
            double step = 22;
            int count = 0;

            A = rr.Count(i => i.A != PlanResult.Empty);
            B = rr.Count(i => i.B != PlanResult.Empty);
            C = rr.Count(i => i.C != PlanResult.Empty);
            D = rr.Count(i => i.D != PlanResult.Empty);
            E = rr.Count(i => i.E != PlanResult.Empty);
            F = rr.Count(i => i.F != PlanResult.Empty);



            List<int> vhp_count = new List<int>();

            vhp_count.Add(A);
            vhp_count.Add(B);
            vhp_count.Add(C);
            vhp_count.Add(D);
            vhp_count.Add(E);
            vhp_count.Add(F);



            foreach (var item in rr)
            {
                if (item.A != PlanResult.Empty || item.B != PlanResult.Empty || item.C != PlanResult.Empty ||
                    item.D != PlanResult.Empty || item.E != PlanResult.Empty || item.F != PlanResult.Empty)
                {
                    EffectiveVHPDay++;
                }

                top = currenttop;

                if (item.PlanDate.DayOfWeek == DayOfWeek.Saturday
                    || item.PlanDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    Rectangle r = new Rectangle();
                    r.Height = 2;
                    r.Width = rectSize;
                    r.Stroke = Brushes.Blue;
                    r.StrokeThickness = 0.5;
                    r.Fill = Brushes.Red;

                    r.SetValue(Canvas.LeftProperty, left);
                    r.SetValue(Canvas.TopProperty, top - 4);
                    mainCanvas.Children.Add(r);


                }


                AddRect(left, top, item.A);
                top += step;
                AddRect(left, top, item.B);
                top += step;
                AddRect(left, top, item.C);
                top += step;
                AddRect(left, top, item.D);
                top += step;
                AddRect(left, top, item.E);
                top += step;
                AddRect(left, top, item.F);

                count++;

                if (count % 60 != 0)
                {
                    left += step;
                    top = currenttop;
                }
                else
                {
                    currenttop += rectSize * 6 + 20;
                    left = 0;
                }
            }


            int max_VHP = vhp_count.Max();

            TextBlock txt = new TextBlock();
            txt.Text = $"Start Date={start.ToShortDateString()} End Date={end.ToShortDateString()} " +
                $"W1={W1}-{((double)W1 / (W1 + W2_Sucess + W2_Failed)).ToString("P")} W1/All," +
                $"W2_Sucess={W2_Sucess}-{((double)W2_Sucess / (W1 + W2_Sucess + W2_Failed)).ToString("P")} W2_Sucess/All," +
                $"W2_Failed={W2_Failed}-{((double)W2_Failed / (W2_Sucess + W2_Failed)).ToString("P")} W2_Sucess/W2_All" +
                $" [A={A} {((double)A / max_VHP).ToString("P")},B={B} {((double)B / max_VHP).ToString("P")},C={C} {((double)C / max_VHP).ToString("P")}," +
                $"D={D} {((double)D / max_VHP).ToString("P")},E={E} {((double)E / max_VHP).ToString("P")},F={F} {((double)F / max_VHP).ToString("P")}]";
            txt.SetValue(Canvas.LeftProperty, 0.0);
            txt.SetValue(Canvas.TopProperty, 0.0);
            mainCanvas.Children.Add(txt);


            //livechart

            Chart_VHP_Count.Values = new ChartValues<int>(vhp_count);

            List<double> vhp_count_percent = new List<double>();
            vhp_count_percent.Add((double)A / max_VHP * 100);
            vhp_count_percent.Add((double)B / max_VHP * 100);
            vhp_count_percent.Add((double)C / max_VHP * 100);
            vhp_count_percent.Add((double)D / max_VHP * 100);
            vhp_count_percent.Add((double)E / max_VHP * 100);
            vhp_count_percent.Add((double)F / max_VHP * 100);


            Chart_VHP_Percent_Count.Values = new ChartValues<double>(vhp_count_percent);


            List<double> w1w2_percent = new List<double>();
            w1w2_percent.Add((double)W1 / (W1 + W2_Sucess + W2_Failed) * 100);
            w1w2_percent.Add((double)W2_Sucess / (W1 + W2_Sucess + W2_Failed) * 100);
            w1w2_percent.Add((double)W2_Failed / (W1 + W2_Sucess + W2_Failed) * 100);


            Chart_W1.Values = new ChartValues<double>(new List<double> { w1w2_percent[0] });
            Chart_W2_Sucess.Values = new ChartValues<double>(new List<double> { w1w2_percent[1] });
            Chart_W2_Failed.Values = new ChartValues<double>(new List<double> { w1w2_percent[2] });

            List<double> w1w2_all_percent = new List<double>();
            w1w2_all_percent.Add((double)(EffectiveVHPDay * 6 - W1 - W2_Sucess - W2_Failed) / (EffectiveVHPDay * 6) * 100);
            w1w2_all_percent.Add((double)W1 / (EffectiveVHPDay * 6) * 100);
            w1w2_all_percent.Add((double)W2_Sucess / (EffectiveVHPDay * 6) * 100);
            w1w2_all_percent.Add((double)W2_Failed / (EffectiveVHPDay * 6) * 100);

            Chart_All_W0.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[0] });
            Chart_All_W1.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[1] });
            Chart_All_W2_Sucess.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[2] });
            Chart_All_W2_Failed.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[3] });

        }

        private int W1 = 0;
        private int W2_Sucess = 0;
        private int W2_Failed = 0;


        private int A = 0, B = 0, C = 0, D = 0, E = 0, F = 0;

        private void AddRect(double l, double t, PlanResult ptype)
        {
            Rectangle r = new Rectangle();
            r.Height = rectSize;
            r.Width = rectSize;
            r.Stroke = Brushes.Blue;
            r.StrokeThickness = 1.0;

            switch (ptype)
            {
                case PlanResult.Empty:
                    r.Fill = Brushes.White;
                    break;
                case PlanResult.W1:
                    r.Fill = Brushes.Gray;
                    W1++;
                    break;
                case PlanResult.W2_Success:
                    r.Fill = Brushes.Yellow;
                    W2_Sucess++;
                    break;
                case PlanResult.W2_Fail:
                    r.Fill = Brushes.Black;
                    W2_Failed++;
                    break;
                default:
                    break;
            }

            r.SetValue(Canvas.LeftProperty, l);
            r.SetValue(Canvas.TopProperty, t);
            mainCanvas.Children.Add(r);
        }



    }
}
