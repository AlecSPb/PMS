using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
namespace PMSQuotation.Tools
{
    public class MaterialNeedCalculationVM : ViewModelBase
    {
        public MaterialNeedCalculationVM()
        {
            condition = new CalculationConditionItem();

            Results = new ObservableCollection<CalculationResultItem>();

            totalWeight = 0;

            #region diameters
            Molds = new List<double>();
            Molds.Clear();
            Molds.Add(50);
            Molds.Add(50.8);
            Molds.Add(76.2);
            Molds.Add(80);
            Molds.Add(100);
            Molds.Add(105);
            Molds.Add(110);
            Molds.Add(124.5);
            Molds.Add(128);
            Molds.Add(152);
            Molds.Add(155);
            Molds.Add(168);
            Molds.Add(200);
            Molds.Add(205);
            Molds.Add(206);
            Molds.Add(230);
            Molds.Add(233);
            Molds.Add(255);
            Molds.Add(300);
            Molds.Add(303);
            Molds.Add(319);
            Molds.Add(330);
            Molds.Add(336);
            Molds.Add(440);
            Molds.Add(444.7);
            Molds.Add(450);
            #endregion
            Add = new RelayCommand(ActionAdd);
            Delete = new RelayCommand<CalculationResultItem>(ActionDelete);
        }

        private void ActionDelete(CalculationResultItem obj)
        {
            if (obj != null)
            {
                Results.Remove(obj);
                CalculateTotalWeight();
            }
        }

        private void ActionAdd()
        {
            if (condition != null)
            {
                CalculationResultItem item = new CalculationResultItem();
                item.Diameter = Condition.Diameter;
                item.Thickness = Condition.Thickness;
                item.Quantity = Condition.Quantity;
                item.Loss = Condition.Loss;

                item.Weight = (Condition.Density * Math.PI * Condition.Diameter * Condition.Diameter * Condition.Thickness / 4000.0)
                    * condition.Quantity + Condition.Loss;

                Results.Add(item);
                CalculateTotalWeight();
            }
        }

        private CalculationConditionItem condition;

        public CalculationConditionItem Condition
        {
            get { return condition; }
            set { condition = value; RaisePropertyChanged(nameof(Condition)); }
        }

        private double totalWeight;

        public double TotalWeight
        {
            get { return totalWeight; }
            set { totalWeight = value; RaisePropertyChanged(nameof(TotalWeight)); }
        }



        public ObservableCollection<CalculationResultItem> Results { get; set; }

        public RelayCommand Add { get; set; }

        public RelayCommand<CalculationResultItem> Delete { get; set; }


        public List<double> Molds { get; set; }


        private void CalculateTotalWeight()
        {
            TotalWeight = 0;
            foreach (var item in Results)
            {
                TotalWeight += item.Weight;
            }
        }

    }

    public class CalculationConditionItem
    {
        public CalculationConditionItem()
        {
            Density = 5.75;
            Diameter = 233;
            Thickness = 5;
            Quantity = 1;
            Loss = 0;
        }
        public double Density { get; set; }
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public double Quantity { get; set; }
        public double Loss { get; set; }
    }

    public class CalculationResultItem
    {
        public double Diameter { get; set; }
        public double Thickness { get; set; }
        public double Quantity { get; set; }
        public double Loss { get; set; }
        public double Weight { get; set; }
    }

}
