using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Tool.Models
{
    public class DensityEstamatorModel:ViewModelBase
    {
        public DensityEstamatorModel()
        {
            GraphiteDiameter = 80;
            GraphiteWeight = 4.4;
            GraphiteThickness = 0.4;
            Weight = 0;
            CalculateDensity = 5.75;
            T1 = T2 = T3 = T4 =TAverage= 0;
            Density = RatioDensity = 0;
        }
        public double GraphiteDiameter { get; set; }
        public double GraphiteWeight { get; set; }
        public double GraphiteThickness { get; set; }
        public double Weight { get; set; }
        public double CalculateDensity { get; set; }
        public double D1 { get; set; }
        public double D2 { get; set; }
        public double T1 { get; set; }
        public double T2 { get; set; }
        public double T3 { get; set; }
        public double T4 { get; set; }
        public double TAverage { get; set; }
        public double Density { get; set; }
        public double RatioDensity { get; set; }

        public void RaiseAll()
        {
            RaisePropertyChanged(nameof(GraphiteDiameter));
            RaisePropertyChanged(nameof(GraphiteThickness));
            RaisePropertyChanged(nameof(GraphiteWeight));
            RaisePropertyChanged(nameof(Weight));
            RaisePropertyChanged(nameof(CalculateDensity));
            RaisePropertyChanged(nameof(D1));
            RaisePropertyChanged(nameof(D2));
            RaisePropertyChanged(nameof(T1));
            RaisePropertyChanged(nameof(T2));
            RaisePropertyChanged(nameof(T3));
            RaisePropertyChanged(nameof(T4));
            RaisePropertyChanged(nameof(TAverage));
            RaisePropertyChanged(nameof(Density));
            RaisePropertyChanged(nameof(RatioDensity));

        }

    }
}
