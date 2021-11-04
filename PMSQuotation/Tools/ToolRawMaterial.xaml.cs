using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;
using Newtonsoft.Json;
using PMSQuotation.Models;

namespace PMSQuotation.Tools
{
    /// <summary>
    /// ToolRawMaterial.xaml 的交互逻辑
    /// </summary>
    public partial class ToolRawMaterial : Window
    {
        public ToolRawMaterial()
        {
            InitializeComponent();
            Items = new ObservableCollection<RawMaterialItem>();
        }
        public void SetEmpty()
        {
            Items.Add(new RawMaterialItem { Material = "Main", UnitPrice = 0, Weight = 0 });
            Items.Add(new RawMaterialItem { Material = "Additive", UnitPrice = 0, Weight = 0 });
            Items.Add(new RawMaterialItem { Material = "", UnitPrice = 0, Weight = 0 });
        }
        public ObservableCollection<RawMaterialItem> Items { get; set; }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public string GetJson()
        {
            var items = Items.Where(i => i.Material != "");
            string json = JsonConvert.SerializeObject(items);
            return json;
        }

        public void SetJson(string json_str)
        {
            if (string.IsNullOrEmpty(json_str))
            {
                XSHelper.XS.MessageBox.ShowInfo("Empty Template Will Be Showed");

                SetEmpty();
                DgMain.ItemsSource = Items;
                return;
            }

            try
            {
                var models = JsonConvert.DeserializeObject<List<RawMaterialItem>>(json_str);
                Items.Clear();
                foreach (var item in models)
                {
                    Items.Add(item);
                }
                if (Items != null)
                {
                    DgMain.ItemsSource = Items;
                }
            }
            catch (Exception)
            {

            }
        }


    }
}
