using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft;
using GalaSoft.MvvmLight;
using PMSClient.MainService;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ReportsHelper;

namespace PMSClient.ViewModel
{
    public class OutputVM : BaseViewModel
    {
        public OutputVM()
        {
            Years = new List<int>();
            Years.Clear();
            int FirstYear = 2011;
            for (int i = 0; i < 30; i++)
            {
                Years.Add(FirstYear + i);
            }
            CurrentYear = DateTime.Now.Year;
            OutputOrder = new RelayCommand(ActionOutputOrder);
        }

        private void ActionOutputOrder()
        {
            ExcelOrder report = new ExcelOrder();
            report.Year = CurrentYear;
            report.Output();
        }

        public RelayCommand OutputOrder { get; set; }

        private int currentYear;
        public int CurrentYear
        {
            get { return currentYear; }
            set { currentYear = value; RaisePropertyChanged(nameof(CurrentYear)); }
        }

        public List<int> Years { get; set; }
    }
}
