using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSXMLCreator.MainService;


namespace PMSXMLCreator
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            Initialize();
            LoadData();
        }

        private void Initialize()
        {
            SearchProductID = "190615-F-1";
            CurrentCOA = ECOA.NewInstance();
            RecordTests = new ObservableCollection<DcRecordTest>();


            Search = new RelayCommand(ActionSearch, CanSearch);
            Create = new RelayCommand(ActionCreate, CanCreate);
            Select = new RelayCommand<DcRecordTest>(ActionSelect);
        }

        private void ActionSelect(DcRecordTest obj)
        {
            CurrentCOA = ToECOA(obj);
        }

        private ECOA ToECOA(DcRecordTest model)
        {
            if (model == null) return null;

            var temp = new ECOA();
            temp.ProductID = model.ProductID;
            temp.ProductName = model.Composition;
            temp.PONumber = model.PO;
            
            temp.DeliveryTo = "TCB";
            temp.ScheduledShipDate = DateTime.Today;
            temp.ActualShipDate = DateTime.Today.AddDays(12);

            temp.Weight = model.Weight;
            temp.Density = model.Density;
            temp.ActualDimension = model.DimensionActual;

            temp.Resistance = model.Resistance;
            temp.GDMS = "Li=0 B=0 P=0 F=0";
            temp.Composition = model.CompositionXRF;
            return temp;
        }

        private bool CanCreate()
        {
            return true;
        }

        private void ActionCreate()
        {
            throw new NotImplementedException();
        }

        private bool CanSearch()
        {
            return !string.IsNullOrEmpty(SearchProductID);
        }

        private void ActionSearch()
        {
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                using (var service = new RecordTestServiceClient())
                {
                    var result = service.GetRecordTestBySearchInPage(0, 10, SearchProductID, string.Empty);
                    RecordTests.Clear();
                    foreach (var item in result)
                    {
                        RecordTests.Add(item);
                    }
                    CurrentCOA = ToECOA(RecordTests.FirstOrDefault());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string searchProductID;
        public string SearchProductID
        {
            get
            {
                return searchProductID;
            }
            set
            {
                searchProductID = value;
                RaisePropertyChanged(nameof(SearchProductID));
            }
        }


        private ECOA currentCOA;
        public ECOA CurrentCOA
        {
            get
            {
                return currentCOA;
            }
            set
            {
                if (currentCOA != value)
                {
                    currentCOA = value;
                    RaisePropertyChanged(nameof(CurrentCOA));
                }
            }
        }


        public ObservableCollection<DcRecordTest> RecordTests { get; set; }




        public RelayCommand Search { get; set; }
        public RelayCommand Create { get; set; }

        public RelayCommand<DcRecordTest> Select { get; set; }

    }
}
