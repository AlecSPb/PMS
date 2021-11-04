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
            XSHelper.XS.MessageBox.ShowInfo($"you are using {name} tool");

            switch (name)
            {
                case "Raw Material":
                    var win1 = new ToolRawMaterial();
                    win1.SetJson(current_item.Remark);
                    if (win1.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumPriceRawMaterial(win1.Items.ToList());
                        current_item.Remark = win1.GetJson();
                    }
                    break;
                case "Powder":
                    var win2 = new ToolPowder();
                    win2.SetJson(current_item.Remark);
                    if (win2.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumPricePowder(win2.Items.ToList());
                        current_item.Remark = win2.GetJson();
                    }
                    break;
                case "VHP":
                    var win3 = new ToolVHP();
                    win3.SetJson(current_item.Remark);
                    if (win3.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumPriceVHP(win3.Items.ToList());
                        current_item.Remark = win3.GetJson();
                    }
                    break;
                case "Machine":
                    var win4 = new ToolMachine();
                    win4.SetJson(current_item.Remark);
                    if (win4.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumPriceMachine(win4.Items.ToList());
                        current_item.Remark = win4.GetJson();
                    }
                    break;
                case "Bonding":
                    var win5 = new ToolBonding();
                    win5.SetJson(current_item.Remark);
                    if (win5.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumPriceBonding(win5.Items.ToList());
                        current_item.Remark = win5.GetJson();
                    }
                    break;
                case "Analysis":
                    var win6 = new ToolAnalysis();
                    win6.SetJson(current_item.Remark);
                    if (win6.ShowDialog() == true)
                    {
                        current_item.ItemUnitPrice = SumPriceAnalysis(win6.Items.ToList());
                        current_item.Remark = win6.GetJson();
                    }
                    break;
                default:
                    break;
            }




            DgUnitPrice.ItemsSource = null;
            DgUnitPrice.ItemsSource = Items;
        }

        private double SumPriceRawMaterial(List<CostItemRawMaterial> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Weight;
            }
            return sum;
        }

        private double SumPricePowder(List<CostItemPowder> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Weight;
            }
            return sum;
        }

        private double SumPriceVHP(List<CostItemVHPCost> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.MachineTime;
            }
            return sum;
        }

        private double SumPriceMachine(List<CostItemMachine> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Quantity;
            }
            return sum;
        }

        private double SumPriceBonding(List<CostItemBonding> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Quantity;
            }
            return sum;
        }
        private double SumPriceAnalysis(List<CostItemAnalysis> items)
        {
            double sum = 0;
            foreach (var item in items)
            {
                sum += item.UnitPrice * item.Quantity;
            }
            return sum;
        }
    }
}
