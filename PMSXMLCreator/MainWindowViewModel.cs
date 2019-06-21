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
            SearchProductID = "190615";
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

            var temp = new ECOA
            {
                ProductID = model.ProductID,
                ProductName = model.Composition,
                PONumber = model.PO,
                ProductAbbr = model.CompositionAbbr,

                DeliveryTo = "TCB",
                ScheduledShipDate = DateTime.Today,
                ActualShipDate = DateTime.Today.AddDays(12),

                Weight = model.Weight,
                Density = model.Density,
                ActualDimension = model.DimensionActual,

                Resistance = model.Resistance,
                GDMS = "Li=0 B=0 P=0 F=0",
                XRF = model.CompositionXRF
            };
            return temp;
        }

        private bool CanCreate()
        {
            return true;
        }

        private ECOAXMLHelper helper = new ECOAXMLHelper();
        private void ActionCreate()
        {
            if (CurrentCOA == null)
            {
                CommonHelper.ShowMessage("当前数据模型为空");
                return;
            }
            helper.CreateXMLFile(CurrentCOA);
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
