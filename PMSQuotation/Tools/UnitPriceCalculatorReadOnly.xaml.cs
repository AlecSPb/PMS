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
    public partial class UnitPriceCalculatorReadOnly : Window
    {
        public UnitPriceCalculatorReadOnly()
        {
            InitializeComponent();
            Items = new ObservableCollection<UnitPriceModelItem>();
        }

        public void SetEmpty()
        {
            Items.Add(new UnitPriceModelItem { ItemName = "Raw Material", ItemUnitPrice = 0, ItemUnitPriceDetail = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Powder", ItemUnitPrice = 0, ItemUnitPriceDetail = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "VHP", ItemUnitPrice = 0, ItemUnitPriceDetail = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Machine", ItemUnitPrice = 0, ItemUnitPriceDetail = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Bonding", ItemUnitPrice = 0, ItemUnitPriceDetail = "" });
            Items.Add(new UnitPriceModelItem { ItemName = "Analysis", ItemUnitPrice = 0, ItemUnitPriceDetail = "" });
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
