using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace PMSDesktopClient.ViewModel
{
    public class RecordVHPQuickEditVM : ViewModelBase
    {
        public RecordVHPQuickEditVM()
        {
            InitializeProperties();
            InitializeCommmands();

            SetPageParametersWhenConditionChange();

            EmptyCurrentRecordVHP();
        }
        private bool isNew;

        private void InitializeProperties()
        {
            isNew = true;
            RecordVHPs = new ObservableCollection<PMSMainService.DcRecordVHP>();
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
            CurrentRecordVHP = new DcRecordVHP();
        }

        private void InitializeCommmands()
        {
            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(new MsgObject() { MsgToken = VToken.RecordVHP });
                NavigationService.Refresh(VToken.RecordVHPRefresh);
            });

            Refresh = new RelayCommand(() => SetPageParametersWhenConditionChange());
            CopyFill = new RelayCommand<DcRecordVHP>(ActionCopyFill);
            Save = new RelayCommand(ActionSave);

            SelectionChanged = new RelayCommand<DcMissonWithPlan>(obj => { ActionSectionChanged(obj); });

            EditItem = new RelayCommand<PMSMainService.DcRecordVHP>(ActionEditItem);
            New = new RelayCommand(ActionNew);

            Chart = new RelayCommand(ActionChart);


            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionChart()
        {

        }

        private void ActionNew()
        {
            EmptyCurrentRecordVHP();
            isNew = true;
            NavigationService.ShowStateMessage("全新创建一个记录");
        }

        private void ActionEditItem(DcRecordVHP obj)
        {
            if (obj != null)
            {
                CurrentRecordVHP = obj;
                isNew = false;
                NavigationService.ShowStateMessage("请修改上方数据，然后保存，取消修改请点新建");
            }
        }

        public void ActionSectionChanged(DcMissonWithPlan model)
        {
            if (model != null)
            {
                CurrentMissonWithPlan = model;
                CurrentRecordVHP.PlanVHPID = CurrentMissonWithPlan.PlanID;

                ReLoadRecordVHPs();
            }
        }

        private void ReLoadRecordVHPs()
        {
            using (var service = new RecordVHPServiceClient())
            {
                //这里使用异步操作
                var task = service.GetRecordVHP(CurrentMissonWithPlan.PlanID);
                RecordVHPs.Clear();
                var result = task.ToList();
                result.ToList().ForEach(i => RecordVHPs.Add(i));
            }
        }

        private void EmptyCurrentRecordVHP()
        {
            if (CurrentRecordVHP != null)
            {
                var model = new DcRecordVHP();
                model.ID = Guid.NewGuid();
                model.PlanVHPID = CurrentMissonWithPlan.PlanID;
                model.CurrentTime = DateTime.Now;
                model.Creator = (App.Current as App).CurrentUser.UserName;
                model.State = PMSCommon.CommonState.Show.ToString();
                model.PV1 = 10;
                model.PV2 = 10;
                model.PV3 = 10;
                model.SV = 10;
                model.Ton = 5;
                model.Vaccum = 1E-3;
                model.Shift1 = 0;
                model.Shift2 = 0;
                model.Omega = 0;
                model.WaterTemperatureIn = 0;
                model.WaterTemperatureOut = 0;
                model.ExtraInformation = "";
                isNew = true;
                CurrentRecordVHP = model;

            }
        }


        private void ActionSave()
        {
            if (CurrentRecordVHP == null)
            {
                return;
            }
            try
            {
                if (CurrentRecordVHP != null)
                {
                    using (var service = new RecordVHPServiceClient())
                    {
                        if (isNew)
                        {
                            service.AddRecordVHP(CurrentRecordVHP);
                        }
                        else
                        {
                            service.UpdateReocrdVHP(CurrentRecordVHP);
                        }

                        ReLoadRecordVHPs();
                        EmptyCurrentRecordVHP();
                        NavigationService.ShowStateMessage("保存完毕");
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void ActionCopyFill(DcRecordVHP obj)
        {
            if (obj != null)
            {
                var model = new DcRecordVHP();

                model.PlanVHPID = obj.PlanVHPID;
                model.ID = Guid.NewGuid();
                model.CurrentTime = DateTime.Now;
                model.Creator = (App.Current as App).CurrentUser.UserName;
                model.PV1 = obj.PV1;
                model.PV2 = obj.PV2;
                model.PV3 = obj.PV3;
                model.SV = obj.SV;

                model.Ton = obj.Ton;
                model.Vaccum = obj.Vaccum;
                model.Shift1 = obj.Shift1;
                model.Shift2 = obj.Shift2;
                model.Omega = obj.Omega;
                model.WaterTemperatureIn = obj.WaterTemperatureIn;
                model.WaterTemperatureOut = obj.WaterTemperatureOut;
                model.ExtraInformation = obj.ExtraInformation;

                isNew = true;
                CurrentRecordVHP = model;
                NavigationService.ShowStateMessage("填充选定项完毕");
            }
        }


        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 6;
            var service = new MissonServiceClient();
            RecordCount = service.GetMissonCountBySearch();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MissonWithPlanServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissonWithPlan(skip, take);
            MissonWithPlans.Clear();
            orders.ToList().ForEach(o => MissonWithPlans.Add(o));

            CurrentMissonWithPlan = MissonWithPlans.FirstOrDefault();
            ActionSectionChanged(CurrentMissonWithPlan);
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
        public RelayCommand PageChanged { get; private set; }
        #endregion
        #region Properties
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }

        private DcRecordVHP currentRecordVHP;
        public DcRecordVHP CurrentRecordVHP
        {
            get { return currentRecordVHP; }
            set
            {
                currentRecordVHP = value;
                RaisePropertyChanged(nameof(CurrentRecordVHP));
            }
        }

        private DcMissonWithPlan currentMissonWithPlan;
        public DcMissonWithPlan CurrentMissonWithPlan
        {
            get { return currentMissonWithPlan; }
            set
            {
                currentMissonWithPlan = value;
                RaisePropertyChanged(nameof(CurrentMissonWithPlan));
            }
        }
        #endregion

        #region Commands
        public RelayCommand Refresh { get; set; }
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand<DcMissonWithPlan> SelectionChanged { get; set; }
        public RelayCommand New { get; set; }
        public RelayCommand<DcRecordVHP> EditItem { get; set; }
        public RelayCommand<DcRecordVHP> CopyFill { get; set; }

        public RelayCommand Chart { get; set; }
        #endregion
    }
}
