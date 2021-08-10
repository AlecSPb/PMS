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
using PMSAnalysis.VHPPlan.Models;
using LiveCharts.Wpf;
using LiveCharts;
using Newtonsoft.Json;
using System.IO;
using CommonHelper;

namespace PMSAnalysis
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VHPPlanAnalysisWindow : Window
    {
        public VHPPlanAnalysisWindow()
        {
            InitializeComponent();
            DpStart.SelectedDate = DateTime.Today.AddMonths(-1);
            DpEnd.SelectedDate = DateTime.Today;
        }

        private void BtnFetch_Click(object sender, RoutedEventArgs e)
        {
            FetchData(DpStart.SelectedDate ?? DateTime.Parse("2021-1-1"), DpEnd.SelectedDate ?? DateTime.Parse("2021-6-8"));
        }


        private void BtnOneYear_Click(object sender, RoutedEventArgs e)
        {
            DpStart.SelectedDate = DateTime.Today.AddYears(-1);
            DpEnd.SelectedDate = DateTime.Today;
        }

        private void BtnOneMonth_Click(object sender, RoutedEventArgs e)
        {
            DpStart.SelectedDate = DateTime.Today.AddMonths(-1);
            DpEnd.SelectedDate = DateTime.Today;
        }
        private void BtnTwoMonth_Click(object sender, RoutedEventArgs e)
        {
            DpStart.SelectedDate = DateTime.Today.AddMonths(-2);
            DpEnd.SelectedDate = DateTime.Today;
        }
        private void BtnThreeMonth_Click(object sender, RoutedEventArgs e)
        {
            DpStart.SelectedDate = DateTime.Today.AddMonths(-3);
            DpEnd.SelectedDate = DateTime.Today;
        }
        private double rectSize = 16;

        private double EffectiveVHPDay = 0;
        private double ShouldVHPSum = 0;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Load();
        }

        private void Load()
        {
            try
            {
                string jsonStr = File.ReadAllText("data.json");
                FileInfo info = new FileInfo("data.json");
                DateTime fileUpdateTime = info.LastWriteTime;

                var dataModel = JsonConvert.DeserializeObject<VHPPlanDataModel>(jsonStr);
                DrawGraph(dataModel);
                DpStart.SelectedDate = dataModel.Start;
                DpEnd.SelectedDate = dataModel.End;
                TxtUsedCache.Text = $"Cached at {fileUpdateTime.ToString()}";
            }
            catch (Exception)
            {
                FetchData(DpStart.SelectedDate ?? DateTime.Parse("2021-1-1"), DpEnd.SelectedDate ?? DateTime.Parse("2021-6-8"));
            }

        }
        private void SaveJson(VHPPlanDataModel model)
        {
            string json = JsonConvert.SerializeObject(model);
            File.WriteAllText("data.json", json);
            //XSHelper.MessageHelper.ShowInfo("保存成功");
        }

        private void FetchData(DateTime start, DateTime end)
        {
            try
            {
                //check cache file
                var h = new Services.VHPPlanAnalysisHelper();
                var rr = h.GetAnalysis(start, end);
                var dataModel = new VHPPlanDataModel();
                dataModel.Models = rr;
                dataModel.Start = start;
                dataModel.End = end;

                SaveJson(dataModel);
                //save cache file
                DrawGraph(dataModel);
                TxtUsedCache.Text = $"Lastest at {DateTime.Now.ToString()}";
            }
            catch (Exception)
            {

            }

        }

        private void DrawGraph(VHPPlanDataModel model)
        {
            var rr = model.Models;

            double currenttop = 20;

            double left = 0;
            double top = currenttop;
            double step = 18;
            int count = 0;
            EffectiveVHPDay = 0;
            ShouldVHPSum = 0;
            W1 = 0;
            W2_Sucess = 0;
            W2_Failed = 0;


            A = B = C = D = E = F = G = 0;

            A = rr.Count(i => i.A != PlanResult.Empty);
            B = rr.Count(i => i.B != PlanResult.Empty);
            C = rr.Count(i => i.C != PlanResult.Empty);
            D = rr.Count(i => i.D != PlanResult.Empty);
            E = rr.Count(i => i.E != PlanResult.Empty);
            F = rr.Count(i => i.F != PlanResult.Empty);
            G = rr.Count(i => i.G != PlanResult.Empty);

            List<int> vhp_count = new List<int>();

            vhp_count.Add(A);
            vhp_count.Add(B);
            vhp_count.Add(C);
            vhp_count.Add(D);
            vhp_count.Add(E);
            vhp_count.Add(F);
            vhp_count.Add(G);

            mainCanvas.Children.Clear();

            foreach (var item in rr)
            {
                if (item.A != PlanResult.Empty || item.B != PlanResult.Empty || item.C != PlanResult.Empty ||
                    item.D != PlanResult.Empty || item.E != PlanResult.Empty || item.F != PlanResult.Empty || item.G != PlanResult.Empty)
                {
                    EffectiveVHPDay++;
                    if (item.PlanDate > new DateTime(2021, 7, 8))
                    {
                        ShouldVHPSum += 7;
                    }
                    else
                    {
                        ShouldVHPSum += 6;
                    }
                }

                top = currenttop;

                if (item.PlanDate.DayOfWeek == DayOfWeek.Saturday
                    || item.PlanDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    Rectangle r = new Rectangle();
                    r.Height = 4;
                    r.Width = rectSize;
                    r.Stroke = Brushes.Blue;
                    r.StrokeThickness = 0.5;
                    r.Fill = Brushes.Red;

                    r.SetValue(Canvas.LeftProperty, left);
                    r.SetValue(Canvas.TopProperty, top - 6);
                    mainCanvas.Children.Add(r);
                }


                AddRect(left, top, item.A, item.PlanDate, "A");
                top += step;
                AddRect(left, top, item.B, item.PlanDate, "B");
                top += step;
                AddRect(left, top, item.C, item.PlanDate, "C");
                top += step;
                AddRect(left, top, item.D, item.PlanDate, "D");
                top += step;
                AddRect(left, top, item.E, item.PlanDate, "E");
                top += step;
                AddRect(left, top, item.F, item.PlanDate, "F");

                if (item.PlanDate > new DateTime(2021, 7, 8))
                {
                    top += step;
                    AddRect(left, top, item.G, item.PlanDate,"G");
                }

                count++;

                if (count % 68 != 0)
                {
                    left += step;
                    top = currenttop;
                }
                else
                {
                    if (item.PlanDate > new DateTime(2021, 7, 8))
                    {
                        currenttop += rectSize * 7 + 20;
                    }
                    else
                    {
                        currenttop += rectSize * 6 + 20;

                    }
                    left = 0;
                }
            }


            int max_VHP = vhp_count.Max();

            TxtResult.Text = $"Start Date={model.Start.ToShortDateString()} End Date={model.End.ToShortDateString()} " +
                $"W1={W1}-{((double)W1 / (W1 + W2_Sucess + W2_Failed)).ToString("P")} W1/All," +
                $"W2_Sucess={W2_Sucess}-{((double)W2_Sucess / (W1 + W2_Sucess + W2_Failed)).ToString("P")} W2_Sucess/All," +
                $"W2_Failed={W2_Failed}-{((double)W2_Failed / (W2_Sucess + W2_Failed)).ToString("P")} W2_Sucess/W2_All" +
                $" [A={A} {((double)A / max_VHP).ToString("P")},B={B} {((double)B / max_VHP).ToString("P")},C={C} {((double)C / max_VHP).ToString("P")}," +
                $"D={D} {((double)D / max_VHP).ToString("P")},E={E} {((double)E / max_VHP).ToString("P")},F={F} {((double)F / max_VHP).ToString("P")}]" +
                $"G={G} {((double)G / max_VHP).ToString("P")}]";


            //livechart

            Chart_VHP_Count.Values = new ChartValues<int>(vhp_count);

            List<double> vhp_count_percent = new List<double>();
            vhp_count_percent.Add(Math.Round((double)A / max_VHP * 100, 2));
            vhp_count_percent.Add(Math.Round((double)B / max_VHP * 100, 2));
            vhp_count_percent.Add(Math.Round((double)C / max_VHP * 100, 2));
            vhp_count_percent.Add(Math.Round((double)D / max_VHP * 100, 2));
            vhp_count_percent.Add(Math.Round((double)E / max_VHP * 100, 2));
            vhp_count_percent.Add(Math.Round((double)F / max_VHP * 100, 2));
            vhp_count_percent.Add(Math.Round((double)G / max_VHP * 100, 2));


            Chart_VHP_Percent_Count.Values = new ChartValues<double>(vhp_count_percent);


            List<double> w1w2_percent = new List<double>();
            w1w2_percent.Add(Math.Round((double)W1 / (W1 + W2_Sucess + W2_Failed) * 100, 2));
            w1w2_percent.Add(Math.Round((double)W2_Sucess / (W1 + W2_Sucess + W2_Failed) * 100, 2));
            w1w2_percent.Add(Math.Round((double)W2_Failed / (W1 + W2_Sucess + W2_Failed) * 100, 2));


            Chart_W1.Values = new ChartValues<double>(new List<double> { w1w2_percent[0] });
            Chart_W2_Sucess.Values = new ChartValues<double>(new List<double> { w1w2_percent[1] });
            Chart_W2_Failed.Values = new ChartValues<double>(new List<double> { w1w2_percent[2] });

            List<double> w1w2_all_percent = new List<double>();
            w1w2_all_percent.Add(Math.Round((double)(ShouldVHPSum - W1 - W2_Sucess - W2_Failed) / ShouldVHPSum * 100, 2));
            w1w2_all_percent.Add(Math.Round((double)W1 / ShouldVHPSum * 100, 2));
            w1w2_all_percent.Add(Math.Round((double)W2_Sucess / ShouldVHPSum * 100, 2));
            w1w2_all_percent.Add(Math.Round((double)W2_Failed / ShouldVHPSum * 100, 2));

            Chart_All_W0.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[0] });
            Chart_All_W1.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[1] });
            Chart_All_W2_Sucess.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[2] });
            Chart_All_W2_Failed.Values = new ChartValues<double>(new List<double> { w1w2_all_percent[3] });


            double SUM = A + B + C + D + E + F;


            PbVHPUsed.Value = SUM * 100 / ShouldVHPSum;
            TxtVHPUsed.Text = PbVHPUsed.Value.ToString("F2") + "%";

            TxtVHPUsedPerDay.Text = (SUM / EffectiveVHPDay).ToString("F2") + " VHP Per Day";
        }


        private int W1 = 0;
        private int W2_Sucess = 0;
        private int W2_Failed = 0;


        private int A = 0, B = 0, C = 0, D = 0, E = 0, F = 0, G = 0;

        private void AddRect(double l, double t, PlanResult ptype,DateTime date,string vhpcode)
        {
            Rectangle r = new Rectangle();
            r.Height = rectSize;
            r.Width = rectSize;
            r.Stroke = Brushes.Blue;
            r.StrokeThickness = 1.0;
            r.ToolTip = $"{date.ToString("yyMMdd dddd")} {vhpcode}";
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
                    r.Fill = Brushes.Orange;
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
