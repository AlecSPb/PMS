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
using PMSQuotation.Models;
using Newtonsoft.Json;

namespace PMSQuotation.Tools
{
    /// <summary>
    /// UnitPriceCalculator.xaml 的交互逻辑
    /// </summary>
    public partial class UnitPriceCalculator : Window
    {
        public UnitPriceCalculator()
        {
            InitializeComponent();
            Items = new ObservableCollection<UnitPriceModelItem>();
        }

        public void SetEmpty()
        {
            Items.Add(new UnitPriceModelItem { ItemName = "Raw Material", ItemUnitPrice = 0, Remark = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Powder", ItemUnitPrice = 0, Remark = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "VHP", ItemUnitPrice = 0, Remark = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Machine", ItemUnitPrice = 0, Remark = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Bonding", ItemUnitPrice = 0, Remark = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Analysis", ItemUnitPrice = 0, Remark = "" });
        }
        public ObservableCollection<UnitPriceModelItem> Items { get; set; }

        public string GetJson()
        {
            string json = JsonConvert.SerializeObject(Items);
            return json;
        }

        public void SetJson(string json_str)
        {
            if (string.IsNullOrEmpty(json_str))
            {
                XSHelper.XS.MessageBox.ShowInfo("Empty Template Will Be Showed");

                SetEmpty();
                DgUnitPrice.ItemsSource = Items;
                return;
            }

            try
            {
                var models = JsonConvert.DeserializeObject<List<UnitPriceModelItem>>(json_str);
                Items.Clear();
                foreach (var item in models)
                {
                    Items.Add(item);
                }
                if (Items != null)
                {
                    DgUnitPrice.ItemsSource = Items;
                }
            }
            catch (Exception)
            {

            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
            this.Close();
        }

        private void BtnDgRowEdit_Click(object sender, RoutedEventArgs e)
        {
            int index = DgUnitPrice.SelectedIndex;

            var current_item = Items[index];
            //Items[index].ItemUnitPrice = 100;
            var name = current_item.ItemName;
            switch (name)
            {
                case "Raw Material":
                    XSHelper.XS.MessageBox.ShowInfo("RawMaterials");
                    var win = new ToolRawMaterial();
                    win.SetJson(current_item.Remark);
                    if (win.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumRawMaterialPrice(win.Items.ToList());
                        current_item.Remark = win.GetJson();
                    }
                    break;
                default:
                    XSHelper.XS.MessageBox.ShowInfo(name);
                    break;
            }




            DgUnitPrice.ItemsSource = null;
            DgUnitPrice.ItemsSource = Items;
        }

        private double SumRawMaterialPrice(List<RawMaterialItem> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Weight;
            }
            return sum;
        }


    }
}
