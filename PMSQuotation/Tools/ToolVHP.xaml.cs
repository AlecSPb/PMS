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
using PMSQuotation.Services;

namespace PMSQuotation.Tools
{
    /// <summary>
    /// ToolRawMaterial.xaml 的交互逻辑
    /// </summary>
    public partial class ToolVHP : Window
    {
        public ToolVHP()
        {
            InitializeComponent();
            Items = new ObservableCollection<CostItemVHPCost>();
        }

        public void SetEmpty()
        {
            var dds_service = new DataDictionaryService();
            var dicts = dds_service.GetKeyValue("vhp_price_rule");
            foreach (var item in dicts)
            {
                Items.Add(new CostItemVHPCost { Machine = item.Key, UnitPrice = item.Value, MachineTime = 0 });
            }

        }
        public ObservableCollection<CostItemVHPCost> Items { get; set; }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public string GetJson()
        {
            var items = Items.Where(i => i.Machine != "");
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
                var models = JsonConvert.DeserializeObject<List<CostItemVHPCost>>(json_str);
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
