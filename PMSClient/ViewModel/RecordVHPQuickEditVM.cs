using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using System.Windows.Input;

namespace PMSClient.ViewModel
{
    public class RecordVHPQuickEditVM : BaseViewModelPage
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
            RecordVHPs = new ObservableCollection<DcRecordVHP>();
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
            CurrentRecordVHP = new DcRecordVHP();

            QuickVHPMesseges = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.QuickVHPMessege>(QuickVHPMesseges);
        }

        private void InitializeCommmands()
        {
            GiveUp = new RelayCommand(() =>
            {
                NavigationService.GoTo(PMSViews.RecordVHP);
            });

            Refresh = new RelayCommand(() => SetPageParametersWhenConditionChange());
            CopyFill = new RelayCommand<DcRecordVHP>(ActionCopyFill);
            Save = new RelayCommand(ActionSave);

            SelectionChanged = new RelayCommand<DcPlanWithMisson>(obj => { ActionSectionChanged(obj); });

            EditItem = new RelayCommand<DcRecordVHP>(ActionEditItem);
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
            NavigationService.ShowStatusMessage("全新创建一个记录");
        }

        private void ActionEditItem(DcRecordVHP mdoel)
        {
            if (mdoel != null)
            {
                CurrentRecordVHP = mdoel;
                isNew = false;
                NavigationService.ShowStatusMessage("请修改上方数据，然后保存，取消修改请点新建");
            }
        }

        public void ActionSectionChanged(DcPlanWithMisson model)
        {
            if (model != null)
            {
                CurrentPlanWithMisson = model;
                CurrentRecordVHP.PlanVHPID = CurrentPlanWithMisson.Plan.ID;
                ReLoadRecordVHPs();
            }
        }

        private void ReLoadRecordVHPs()
        {
            using (var service = new RecordVHPServiceClient())
            {
                //TODO:这里以后使用异步操作
                var task = service.GetRecordVHP(CurrentPlanWithMisson.Plan.ID);
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
                model.PlanVHPID = CurrentPlanWithMisson.Plan.ID;
                model.CurrentTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.SimpleState.正常.ToString();
                model.PV1 = 0;
                model.PV2 = 0;
                model.PV3 = 0;
                model.SV = 0;
                model.Ton = 5;
                model.Vaccum = 1E-3;
                model.Shift1 = 0;
                model.Shift2 = 0;
                model.Omega = 0;
                model.WaterTemperatureIn = 25;
                model.WaterTemperatureOut = 25;
                model.ExtraInformation = "无";
                isNew = true;
                CurrentRecordVHP = model;

            }
        }


        private void ActionSave()
        {
            try
            {
                if (CurrentRecordVHP != null)
                {
                    using (var service = new RecordVHPServiceClient())
                    {
                        if (isNew)
                        {
                            CurrentRecordVHP.CurrentTime = DateTime.Now;
                            service.AddRecordVHP(CurrentRecordVHP);
                        }
                        else
                        {
                            service.UpdateReocrdVHP(CurrentRecordVHP);
                        }
                        service.Close();
                        ReLoadRecordVHPs();
                        EmptyCurrentRecordVHP();
                        NavigationService.ShowStatusMessage("保存完毕");
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

                #region 初始化
                model.PlanVHPID = obj.PlanVHPID;
                model.ID = Guid.NewGuid();
                model.CurrentTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.SimpleState.正常.ToString();
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
                #endregion

                isNew = true;

                CurrentRecordVHP = model;
                NavigationService.ShowStatusMessage("填充选定项完毕");
            }
        }


        private void InitializeCommands()
        {
            PageChanged = new RelayCommand(ActionPaging);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 5;
            using (var service = new MissonServiceClient())
            {
                RecordCount = service.GetPlanWithMissonCheckedCount();
            }

            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;

            using (var service = new MissonServiceClient())
            {
                var orders = service.GetPlanWithMissonChecked(skip, take);
                PlanWithMissons.Clear();
                orders.ToList().ForEach(o => PlanWithMissons.Add(o));
            }
            CurrentPlanWithMisson = PlanWithMissons.FirstOrDefault();
            ActionSectionChanged(CurrentPlanWithMisson);
        }


        #region Properties
        public ObservableCollection<DcRecordVHP> RecordVHPs { get; set; }
        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }

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

        private DcPlanWithMisson currentPlanWithMisson;
        public DcPlanWithMisson CurrentPlanWithMisson
        {
            get { return currentPlanWithMisson; }
            set
            {
                currentPlanWithMisson = value;
                RaisePropertyChanged(nameof(CurrentPlanWithMisson));
            }
        }


        public List<string> QuickVHPMesseges { get; set; }
        #endregion

        #region Commands
        public RelayCommand Refresh { get; set; }
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
        public RelayCommand<DcPlanWithMisson> SelectionChanged { get; set; }
        public RelayCommand New { get; set; }
        public RelayCommand<DcRecordVHP> EditItem { get; set; }
        public RelayCommand<DcRecordVHP> CopyFill { get; set; }

        public RelayCommand Chart { get; set; }
        #endregion
    }
}
