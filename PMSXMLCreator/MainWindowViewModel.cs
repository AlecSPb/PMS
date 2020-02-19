using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;
using PMSXMLCreator.MainService;
using PMSXMLCreator.Service;
using CommonHelper;
using Newtonsoft.Json;
using System.IO;



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
            LoadingInformation = "";
            CurrentCOA = new ECOA();
            RecordTests = new ObservableCollection<DcRecordTest>();


            Search = new RelayCommand(ActionSearch, CanSearch);
            CreateXML = new RelayCommand(ActionCreateXML, CanCreateXML);
            CreateDocx = new RelayCommand(ActionCreateDocx, CanCreateDocx);
            Select = new RelayCommand<DcRecordTest>(ActionSelect);
            LoadFromFile = new RelayCommand(ActionLoadFromFile);
            LoadFromPMSFile = new RelayCommand(ActionLoadFromPMSFile);
            Save = new RelayCommand(ActionSave);
        }

        private void ActionLoadFromPMSFile()
        {
            try
            {
                string initialPath = XSHelper.FileHelper.GetDesktopPath();
                string filter = "Data|*.json";
                XSDialogResult openPath = XSHelper.DialogHelper.ShowOpenDialog(initialPath, filter);
                if (openPath.HasSelected)
                {
                    string json = XSHelper.FileHelper.ReadText(openPath.SelectPath);
                    DcRecordTest test = JsonConvert.DeserializeObject<DcRecordTest>(json);
                    if (test != null)
                    {
                        CurrentCOA = ToECOA(test);
                    }
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }

        private void ActionSave()
        {
            if (XSHelper.MessageHelper.ShowYesNo("请问确定要保存吗"))
            {
                if (CurrentCOA != null)
                {
                    FileHelper fh = XSHelper.FileHelper;
                    string json = JsonConvert.SerializeObject(CurrentCOA);
                    string filepath = fh.GetFullFileName(fh.GetGoodDateTimeFileName(CurrentCOA.LotNumber,"json"),
                        fh.GetCurrentFolderPath(), "SavedFile");
                    XSHelper.FileHelper.SaveText(filepath, json);
                    XSHelper.MessageHelper.ShowInfo("保存数据成功");
                }
            }
        }

        /// <summary>
        /// 创建docx文件
        /// </summary>
        private void ActionCreateDocx()
        {
            if (CurrentCOA == null)
            {
                XSHelper.MessageHelper.ShowInfo("当前数据模型为空");
                return;
            }
            if (XSHelper.MessageHelper.ShowYesNo($"确定使用该条数据[{CurrentCOA.LotNumber}]生成Docx文件？"))
            {
                helper_docx.CreateFile(CurrentCOA);
            }
        }

        private bool CanCreateDocx()
        {
            return true;
        }

        private void ActionLoadFromFile()
        {
            string initialDirectory = XSHelper.FileHelper.GetCurrentFolderPath("SavedFile");
            string filter = "Data|*.json";
            XSDialogResult savePath = XSHelper.DialogHelper.ShowOpenDialog(initialDirectory, filter);

            if (savePath.HasSelected == true)
            {
                string file = savePath.SelectPath;

                LoadingInformation = "Loading From XML File";

                string json = XSHelper.FileHelper.ReadText(file);

                CurrentCOA = JsonConvert.DeserializeObject<ECOA>(json);

                XSHelper.MessageHelper.ShowInfo("载入数据成功");
            }

        }

        private void ActionSelect(DcRecordTest record)
        {
            if (XSHelper.MessageHelper.ShowYesNo($"确定使用该条数据覆盖下面当前的数据[{record.ProductID}]？"))
            {
                CurrentCOA = ToECOA(record);
                LoadingInformation = "Loading From PMS";
            }
        }

        /// <summary>
        /// 转换数据模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        private bool CanCreateXML()
        {
            return true;
        }

        private XmlHelper helper_xml = new XmlHelper();
        private DocxHelper helper_docx = new DocxHelper();
        /// <summary>
        /// 创建xml文件
        /// </summary>
        private void ActionCreateXML()
        {
            if (CurrentCOA == null)
            {
                XSHelper.MessageHelper.ShowInfo("当前数据模型为空");
                return;
            }
            if (XSHelper.MessageHelper.ShowYesNo($"确定使用该条数据[{CurrentCOA.LotNumber}]生成xml文件？"))
            {
                helper_xml.CreateFile(CurrentCOA);
            }
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
                    LoadingInformation = "Loading From PMS";
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


        private string loadingInformation;
        public string LoadingInformation
        {
            get
            {
                return loadingInformation;
            }
            set
            {
                if (loadingInformation != value)
                {
                    loadingInformation = value;
                    RaisePropertyChanged(nameof(LoadingInformation));
                }
            }
        }

        public ObservableCollection<DcRecordTest> RecordTests { get; set; }




        public RelayCommand Search { get; set; }
        public RelayCommand CreateXML { get; set; }
        public RelayCommand CreateDocx { get; set; }
        public RelayCommand LoadFromFile { get; set; }
        public RelayCommand LoadFromPMSFile { get; set; }
        public RelayCommand Save { get; set; }

        public RelayCommand<DcRecordTest> Select { get; set; }

    }
}
