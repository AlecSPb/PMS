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
using PMSSPC.Services;
using LiveCharts.Wpf;
using LiveCharts;
using Newtonsoft.Json;
using System.IO;
using PMSSPC.Models;

namespace PMSSPC
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadDefaultValues();


            try
            {
                LoadDataFromFile($"DataSaved\\{Properties.Settings.Default.LastSavedFile}");
            }
            catch (Exception)
            {
                BtnFetch_Click(this, null);
            }

        }

        private SPCModel spc_model;

        public void LoadDefaultValues()
        {
            spc_model = new SPCModel();
            #region set default value
            CboComposition.Items.Clear();
            CboComposition.Items.Add("In40S60");
            CboComposition.Items.Add("In40Se60");
            CboComposition.Items.Add("Cu25Ga25Se50");
            CboComposition.Items.Add("Cu22.8In22.0Ga5.0Se50.2");
            CboComposition.Items.Add("Cu22.8In21.5Ga5.5Se50.2");
            CboComposition.Items.Add("Cu22.8In21.0Ga6.0Se50.2");
            CboComposition.Items.Add("Cu22.8In20.5Ga6.5Se50.2");
            CboComposition.Items.Add("Cu22.8In20.0Ga7.0Se50.2");
            CboComposition.Items.Add("Cu23.72In19.76Ga8.32Se48.20");
            CboComposition.Items.Add("Cu23.72In18.72Ga9.36Se48.20");
            CboComposition.Items.Add("Cu23.72In17.68Ga10.40Se48.20");

            CboSPCType.Items.Clear();
            CboSPCType.Items.Add("Density");

            DpStart.SelectedDate = new DateTime(2019, 1, 1);
            DpEnd.SelectedDate = DateTime.Today.AddDays(1);
            #endregion
        }

        private SPCServce service = new SPCServce();

        private void LoadSPCModel(SPCModel model)
        {
            SetSPCModel(model.Items, model.SPCType, model.Unit);
            DpStart.SelectedDate = model.Start;
            DpEnd.SelectedDate = model.End;
            CboComposition.SelectedItem = spc_model.Items[0].Composition;
            CboSPCType.SelectedItem = model.SPCType;
        }

        private void SetSPCModel(List<SPCDataItem> data, string spctype, string unit)
        {
            spc_model.Items.Clear();
            spc_model.Items.AddRange(data);
            spc_model.Unit = unit;
            spc_model.SPCType = spctype;
            spc_model.Start = DpStart.SelectedDate ?? DateTime.Now;
            spc_model.End = DpEnd.SelectedDate ?? DateTime.Now;

            spc_model.Calc();

            DgSPCData.ItemsSource = null;
            DgSPCData.ItemsSource = spc_model.Items;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"From [{spc_model.Start.ToShortDateString()}] to [{spc_model.End.ToShortDateString()}]");
            sb.AppendLine($"Unit:{spc_model.Unit}");
            sb.AppendLine($"Composition:{spc_model.Items[0].Composition}");
            sb.AppendLine($"Data Count:{spc_model.Items.Count}");
            sb.AppendLine($"USL={spc_model.USL.ToString("F2")}");
            sb.AppendLine($"SL={spc_model.SL.ToString("F2")}");
            sb.AppendLine($"LSL={spc_model.LSL.ToString("F2")}");
            sb.AppendLine($"UCL={spc_model.UCL.ToString("F2")}");
            sb.AppendLine($"CL={spc_model.CL.ToString("F2")}");
            sb.AppendLine($"LCL={spc_model.LCL.ToString("F2")}");
            sb.AppendLine($"Sigma={spc_model.Sigma.ToString("F2")}");
            sb.AppendLine($"Cp={spc_model.Cp.ToString("F2")}");
            sb.AppendLine($"Cpk={spc_model.Cpk.ToString("F2")}");
            TxtCpk.Text = sb.ToString();

            Plot(spc_model);
        }

        private void BtnFetch_Click(object sender, RoutedEventArgs e)
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("Fetch Data From PMS?"))
            {
                var data = service.GetCleanedSPCDataItemDensity(CboComposition.SelectedItem.ToString(), DpStart.SelectedDate?.ToString("yyyy-MM-dd"),
                    DpEnd.SelectedDate?.ToString("yyyy-MM-dd"));
                SetSPCModel(data, CboSPCType.SelectedItem.ToString(), "g/cm3");
            }

        }

        private void Plot(SPCModel spc_model)
        {
            var chart_values = new ChartValues<double>(spc_model.Items.Select(i => i.Value));
            var chart_usl = new ChartValues<double>();
            var chart_sl = new ChartValues<double>();
            var chart_lsl = new ChartValues<double>();
            var chart_ucl = new ChartValues<double>();
            var chart_cl = new ChartValues<double>();
            var chart_lcl = new ChartValues<double>();



            for (int i = 0; i < spc_model.Items.Count; i++)
            {
                chart_usl.Add(Math.Round(spc_model.USL, 2));
                chart_sl.Add(Math.Round(spc_model.SL, 2));
                chart_lsl.Add(Math.Round(spc_model.LSL, 2));
                chart_ucl.Add(Math.Round(spc_model.UCL, 2));
                chart_cl.Add(Math.Round(spc_model.CL, 2));
                chart_lcl.Add(Math.Round(spc_model.LCL, 2));
            }

            Chart_Value.Values = chart_values;
            Chart_USL.Values = chart_usl;
            Chart_SL.Values = chart_sl;
            Chart_LSL.Values = chart_lsl;

            Chart_UCL.Values = chart_ucl;
            Chart_CL.Values = chart_cl;
            Chart_LCL.Values = chart_lcl;


            List<string> xlabels = new List<string>();
            spc_model.Items.ForEach(i => xlabels.Add(i.ProductID));

            AxisX.Labels = xlabels.ToArray();
        }

        private void LoadDataFromFile(string filename)
        {
            if (File.Exists(filename))
            {
                string json_str = File.ReadAllText(filename);
                SPCModel model = JsonConvert.DeserializeObject<SPCModel>(json_str);
                LoadSPCModel(model);
            }
        }

        private string savedFolder = System.IO.Path.Combine(Environment.CurrentDirectory, "DataSaved");

        private void BtnOpen_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var result = XSHelper.XS.Dialog.ShowOpenDialog(savedFolder, "json data file (*.json)|*.json");
                if (result.HasSelected)
                {
                    string filename = result.SelectPath;

                    LoadDataFromFile(filename);
                }
            }
            catch (Exception)
            {

                throw;
            }


        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (XSHelper.XS.MessageBox.ShowYesNo("save the current spc data?"))
                {
                    SaveFile(false);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        private void SaveFile(bool isAuto=false)
        {
            string filename = "";
            if (isAuto)
            {
                filename= "auto";
            }

            filename += $"{spc_model.Start.ToString("yyMMdd")}-{spc_model.End.ToString("yyMMdd")}-" +
                  $"{spc_model.Items[0].Composition.ToString()}-{spc_model.SPCType.ToString()}.json";

            if (!Directory.Exists(savedFolder))
            {
                Directory.CreateDirectory(savedFolder);
            }


            string fullfilename = System.IO.Path.Combine(savedFolder, filename);

            var savedData = spc_model;

            string json = JsonConvert.SerializeObject(savedData, Formatting.Indented);

            File.WriteAllText(fullfilename, json);


            Properties.Settings.Default.LastSavedFile = filename;
            Properties.Settings.Default.Save();
        }

        private void BtnFolder_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(savedFolder);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SaveFile(true);
        }
    }
}
