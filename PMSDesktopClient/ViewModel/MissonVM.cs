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

namespace PMSDesktopClient.ViewModel
{
    public class MissonVM : ViewModelBase
    {
        public MissonVM()
        {
            InitializeProperties();
            InitializeCommands();
            SetPageParametersWhenConditionChange();
        }

        private void InitializeProperties()
        {
            MainMissons = new ObservableCollection<DcOrder>();
        }
        private void InitializeCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.GoTo(VNCollection.Navigation));
            GoToPlan = new RelayCommand(() => NavigationService.GoTo(VNCollection.Plan));


            PageChanged = new RelayCommand(ActionPaging);
            AddNewPlan = new RelayCommand<PMSMainService.DcOrder>(ActionAddNewPlan);
            EditPlan = new RelayCommand<DcPlanVHP>(ActionEditPlan);
            DuplicatePlan = new RelayCommand<PMSMainService.DcPlanVHP>(ActionDuplicatePlan);
        }

        private void ActionDuplicatePlan(DcPlanVHP obj)
        {
            if (obj!=null)
            {
                var service = new PlanVHPServiceClient();
                obj.ID = Guid.NewGuid();
                service.AddVHPPlan(obj);
                SetPageParametersWhenConditionChange();
            }
        }

        private void ActionEditPlan(DcPlanVHP obj)
        {
            if (obj!=null)
            {
                var nModel = new MsgObject();
                nModel.GoToToken = "PlanEditView";
                nModel.ModelObject = obj;
                nModel.IsAdd = false;
                NavigationService.GoToWithParameter(nModel);
            }
        }

        private void ActionAddNewPlan(DcOrder obj)
        {
            if (obj!=null)
            {
                DcPlanVHP plan = new DcPlanVHP();
                plan.ID = Guid.NewGuid();
                plan.OrderID = obj.ID;
                plan.PlanDate = DateTime.Now.Date;
                plan.MoldType = "GQ";
                plan.VHPDeviceCode = "A";
                plan.Temperature=0;
                plan.Pressure = 0;
                plan.Vaccum = 0;
                plan.ProcessCode = "W1";
                plan.PrePressure = 0;
                plan.PreTemperature = 0;
                plan.Quantity = 1;
                plan.MoldDiameter = 230;
                plan.Thickness = 5;
                plan.CreateTime = DateTime.Now;
                plan.State = "UnChecked";
                plan.CalculationDensity = 5.75;
                plan.GrainSize = "-200";
                plan.RoomHumidity = 80;
                plan.RoomTemperature = 23;
                plan.KeepTempTime = 120;
                plan.MillingRequirement = "常规要求";
                plan.MachineRequirement="常规要求";
                plan.FillingRequirement = "常规要求";
                plan.SpecialRequirement = "无";
                plan.Creator = "xs.zhou";

                var nModel = new MsgObject();
                nModel.GoToToken = "PlanEditView";
                nModel.ModelObject = plan;
                nModel.IsAdd = true;
                NavigationService.GoToWithParameter(nModel);
            }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new MissonServiceClient();
            RecordCount = service.GetMissonCountBySearch();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MissonServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissonBySearchInPage(skip, take);
            MainMissons.Clear();
            orders.ToList<DcOrder>().ForEach(o => MainMissons.Add(o));
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

        private ObservableCollection<DcOrder> mainMissons;
        public ObservableCollection<DcOrder> MainMissons
        {
            get { return mainMissons; }
            set { mainMissons = value; RaisePropertyChanged(nameof(MainMissons)); }
        }

        #endregion

        #region Commands
        public RelayCommand Navigate { get; private set; }
        public RelayCommand GoToPlan { get; private set; }
        public RelayCommand Add { get; private set; }
        public RelayCommand PageChanged { get; private set; }

        public RelayCommand<DcOrder> AddNewPlan { get; set; }

        public RelayCommand<DcPlanVHP> EditPlan { get; set; }

        public RelayCommand<DcPlanVHP> DuplicatePlan { get; set; }
        #endregion
    }
}
