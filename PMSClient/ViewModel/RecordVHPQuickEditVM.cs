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
using System.Windows;

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
            //PMSBasicDataService.SetListDS<PMSCommon.QuickVHPMessege>(QuickVHPMesseges);
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.VHPQuickMessage, QuickVHPMesseges);
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
            DeleteItem = new RelayCommand<DcRecordVHP>(ActionDeleteItem);
            New = new RelayCommand(ActionNew);

            Chart = new RelayCommand(ActionChart);


            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionDeleteItem(DcRecordVHP model)
        {
            var msgResult = PMSDialogService.ShowYesNo("请问", "确定要作废该条记录？");
            if (!msgResult)
            {
                return;
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                if (model != null)
                {
                    using (var service = new RecordVHPServiceClient())
                    {
                        model.State = PMSCommon.SimpleState.作废.ToString();

                        service.UpdateRecordVHPByUID(model,uid);
                        service.Close();
                        ReLoadRecordVHPs();
                        //EmptyCurrentRecordVHP();
                        NavigationService.ShowStatusMessage("作废完毕");
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }


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
                EditStatus = "编辑创建";
                NavigationService.ShowStatusMessage("请修改上方数据，然后保存，保存将使用新的时间，取消修改请点新建");
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
            EditStatus = "全新创建";
            if (CurrentRecordVHP != null)
            {
                var temp = new DcRecordVHP();
                temp.ID = Guid.NewGuid();
                temp.PlanVHPID = CurrentPlanWithMisson.Plan.ID;
                temp.CurrentTime = DateTime.Now;
                temp.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                temp.State = PMSCommon.SimpleState.正常.ToString();
                temp.PV1 = 22;
                temp.PV2 = 0;
                temp.PV3 = 0;
                temp.SV = 0;
                temp.Ton = 5;
                temp.Vaccum = 1E-3;
                temp.Shift1 = 0;
                temp.Shift2 = 0;
                temp.Omega = 0;
                temp.WaterTemperatureIn = 25;
                temp.WaterTemperatureOut = 25;
                temp.ExtraInformation = "无";
                isNew = true;
                CurrentRecordVHP = temp;

            }
        }


        private void ActionSave()
        {
            var msgResult = PMSDialogService.ShowYesNo("请问", "确定要保存该条记录？");
            if (!msgResult)
            {
                return;
            }


            try
            {
                if (CurrentRecordVHP != null)
                {
                    string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                    using (var service = new RecordVHPServiceClient())
                    {
                        if (isNew)
                        {
                            CurrentRecordVHP.CurrentTime = DateTime.Now;
                            service.AddRecordVHPByUID(CurrentRecordVHP,uid);
                        }
                        else
                        {
                            service.UpdateRecordVHPByUID(CurrentRecordVHP,uid);
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
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private void ActionCopyFill(DcRecordVHP model)
        {
            if (model != null)
            {
                var temp = new DcRecordVHP();

                #region 初始化
                temp.PlanVHPID = model.PlanVHPID;
                temp.ID = Guid.NewGuid();
                temp.CurrentTime = DateTime.Now;
                temp.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                temp.State = PMSCommon.SimpleState.正常.ToString();
                temp.PV1 = model.PV1;
                temp.PV2 = model.PV2;
                temp.PV3 = model.PV3;
                temp.SV = model.SV;

                temp.Ton = model.Ton;
                temp.Vaccum = model.Vaccum;
                temp.Shift1 = model.Shift1;
                temp.Shift2 = model.Shift2;
                temp.Omega = model.Omega;
                temp.WaterTemperatureIn = model.WaterTemperatureIn;
                temp.WaterTemperatureOut = model.WaterTemperatureOut;
                temp.ExtraInformation = model.ExtraInformation;
                #endregion

                isNew = true;
                EditStatus = "全新创建";
                CurrentRecordVHP = temp;
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
            PageSize = 6;
            using (var service = new MissonServiceClient())
            {//TODO:切换搜索
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
        private string editStatus;

        public string EditStatus
        {
            get { return editStatus; }
            set { editStatus = value; RaisePropertyChanged(nameof(EditStatus)); }
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
        public RelayCommand<DcRecordVHP> DeleteItem { get; set; }
        public RelayCommand<DcRecordVHP> CopyFill { get; set; }

        public RelayCommand Chart { get; set; }
        #endregion
    }
}
