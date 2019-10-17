using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSXMLCreator.MainService;
using PMSXMLCreator.XMLGenerator;

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
            SearchProductID = "190809-C-1#272";
            CurrentCOA = new ECOA();
            RecordTests = new ObservableCollection<DcRecordTest>();


            Search = new RelayCommand(ActionSearch, CanSearch);
            Create = new RelayCommand(ActionCreate, CanCreate);
            Select = new RelayCommand<DcRecordTest>(ActionSelect);
        }

        private void ActionSelect(DcRecordTest record)
        {
            if (Helper.ShowDialog($"确定使用该条数据[{record.ProductID}]？"))
            {
                CurrentCOA = ToECOA(record);
            }
        }

        private ECOA ToECOA(DcRecordTest model)
        {
            if (model == null) return null;

            var temp = new ECOA();
            #region 赋值
            temp.ThisDocumentGenerationDateTime = DateTime.Now;
            temp.ProductName = model.Composition;
            temp.LotCreatedDate = model.CreateTime;
            temp.LotNumber = model.ProductID;
            temp.Density = model.Density;
            temp.Weight = model.Weight;
            temp.TargetDimension = model.DimensionActual;
            temp.ManufacturerOrderNumber = model.PMINumber;
            temp.ManufacturerPartNumber = model.PMINumber;
            #endregion
            return temp;
        }

        private bool CanCreate()
        {
            return true;
        }

        private ECOAXMLGenerator helper = new ECOAXMLGenerator();
        private void ActionCreate()
        {
            if (CurrentCOA == null)
            {
                Helper.ShowMessage("当前数据模型为空");
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
