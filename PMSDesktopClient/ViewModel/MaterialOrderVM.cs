using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSCommon;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;
using DocGenerator;
using gn = DocGenerator.DocModels;
using AutoMapper;
using System.Windows;

namespace PMSDesktopClient.ViewModel
{
    public class MaterialOrderVM : ViewModelBase
    {
        public MaterialOrderVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            MainMaterialOrders = new ObservableCollection<DcMaterialOrder>();
        }
        private void InitializeCommands()
        {
            GoToNavigation = new RelayCommand(() => NavigationService.GoTo(VNCollection.Navigation));
            PageChanged = new RelayCommand(ActionPaging);
            Search = new RelayCommand(ActionSearch, CanSearch);
            All = new RelayCommand(ActionAll);
            GenerateDoc = new RelayCommand<PMSMainService.DcMaterialOrder>(ActionGenerateDoc);

            Add = new RelayCommand(ActionAdd);
            Edit = new RelayCommand<PMSMainService.DcMaterialOrder>(ActionEdit);

            AddItem = new RelayCommand<PMSMainService.DcMaterialOrder>(ActionAddItem);
            EditItem = new RelayCommand<PMSMainService.DcMaterialOrderItem>(ActionEditItem);

        }

        private void ActionEditItem(DcMaterialOrderItem obj)
        {
            if (obj != null)
            {
                MsgObject msg = new MsgObject();
                msg.GoToToken = VN.MaterialOrderItemEdit.ToString();
                msg.Model = new ModelObject() { IsNew = false, Model = obj };
                NavigationService.GoToWithParameter(msg);
            }
        }

        private void ActionAddItem(DcMaterialOrder obj)
        {
            if (obj != null)
            {



                MsgObject msg = new MsgObject();
                msg.GoToToken = VN.MaterialNeedSelect.ToString();
                msg.Model = new ModelObject() { Model = obj };
                NavigationService.GoToWithParameter(msg);
            }
        }

        private void ActionEdit(DcMaterialOrder obj)
        {
            if (obj != null)
            {
                MsgObject msg = new PMSDesktopClient.MsgObject();
                msg.GoToToken = VNCollection.MaterialOrderEdit;
                msg.IsAdd = false;
                msg.ModelObject = obj;
                NavigationService.GoToWithParameter(msg);
            }
        }

        private void ActionAdd()
        {
            var model = new DcMaterialOrder();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.State = PMSCommon.OrderState.UnChecked.ToString();
            model.Creator = (App.Current as App).CurrentUser.UserName;
            model.Supplier = "Sanjie";
            model.SupplierAbbr = "SJ";
            model.SupplierEmail = "sj_materials@163.com";
            model.SupplierReceiver = "Mr.Wang";
            model.SupplierAddress = "Chengdu,Sichuan CHINA";
            model.ShipFee = 0;
            model.Priority = PMSCommon.OrderPriority.Normal.ToString();
            model.Remark = "";
            model.OrderPO = DateTime.Now.ToString("yyMMdd") + "_" + model.SupplierAbbr;

            MsgObject msg = new PMSDesktopClient.MsgObject();
            msg.GoToToken = VNCollection.MaterialOrderEdit;
            msg.IsAdd = true;
            msg.ModelObject = model;
            NavigationService.GoToWithParameter(msg);
        }

        private void ActionGenerateDoc(DcMaterialOrder args)
        {
            if (MessageBox.Show("Do you want to create doc in desktop?", "Ask", MessageBoxButton.YesNo, MessageBoxImage.Information)
                == MessageBoxResult.No)
            {
                return;
            }

            if (args != null)
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<DcMaterialOrder, gn.MaterialOrder>();
                    cfg.CreateMap<DcMaterialOrderItem, gn.MaterialOrderItem>();
                });
                var mapper = config.CreateMapper();

                var readyModel = mapper.Map<DcMaterialOrder, gn.MaterialOrder>(args);
                gn.MaterialOrder model = readyModel;

                var mainGenerator = new GeneralGenerator();
                IDoc<gn.MaterialOrder> generator = new GeneratorMaterialOrder();
                string source = nameof(DocTemplateEnum.MaterialOrder);
                string target = model.OrderPO;
                mainGenerator.Generate<gn.MaterialOrder>(generator, model, source, "PO" + target);

                MessageBox.Show("File At:" + mainGenerator.TargetFolder, "Doc has been Created");
            }
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchOrderPO) && string.IsNullOrEmpty(SearchSupplier));
        }

        private void ActionAll()
        {
            SearchOrderPO = "";
            SearchSupplier = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new MaterialOrderServiceClient();
            RecordCount = service.GetMaterialOrderCountBySearch(SearchOrderPO, SearchSupplier);
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MaterialOrderServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMaterialOrderBySearchInPage(skip, take, SearchOrderPO, SearchSupplier);
            MainMaterialOrders.Clear();
            orders.ToList<DcMaterialOrder>().ForEach(o => MainMaterialOrders.Add(o));
        }


        #region PagingProperties
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                RaisePropertyChanged(nameof(RecordCount));
            }
        }
        #endregion

        #region Proeperties
        private string searchOrderPO;
        public string SearchOrderPO
        {
            get { return searchOrderPO; }
            set
            {
                if (searchOrderPO == value)
                    return;
                searchOrderPO = value;
                RaisePropertyChanged(() => SearchOrderPO);
            }
        }
        private string searchSupplier;
        public string SearchSupplier
        {
            get { return searchSupplier; }
            set
            {
                if (searchSupplier == value)
                    return;
                searchSupplier = value;
                RaisePropertyChanged(() => SearchSupplier);
            }
        }





        private ObservableCollection<DcMaterialOrder> mainMaterialOrders;
        public ObservableCollection<DcMaterialOrder> MainMaterialOrders
        {
            get { return mainMaterialOrders; }
            set { mainMaterialOrders = value; RaisePropertyChanged(nameof(MainMaterialOrders)); }
        }
        #endregion

        #region Commands
        public RelayCommand GoToNavigation { get; private set; }
        public RelayCommand Search { get; private set; }
        public RelayCommand All { get; set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand<DcMaterialOrder> Edit { get; set; }
        public RelayCommand PageChanged { get; private set; }
        public RelayCommand<DcMaterialOrder> GenerateDoc { get; private set; }


        public RelayCommand<DcMaterialOrder> AddItem { get; private set; }
        public RelayCommand<DcMaterialOrderItem> EditItem { get; private set; }
        #endregion
    }
}
