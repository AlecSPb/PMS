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
    public partial class ToolBonding : Window
    {
        public ToolBonding()
        {
            InitializeComponent();
            Items = new ObservableCollection<CostItemBonding>();
        }

        public void SetEmpty()
        {
            var dds_service = new DataDictionaryService();
            var dicts = dds_service.GetKeyValue("bonding_price_rule");
            foreach (var item in dicts)
            {
                Items.Add(new CostItemBonding { BondingGrade = item.Key, UnitPrice = item.Value, Quantity = 0 });
            }

        }
        public ObservableCollection<CostItemBonding> Items { get; set; }
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        public string GetJson()
        {
            var items = Items.Where(i => i.BondingGrade != "");
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
                var models = JsonConvert.DeserializeObject<List<CostItemBonding>>(json_str);
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
