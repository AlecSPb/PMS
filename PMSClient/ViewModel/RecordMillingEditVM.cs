using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class RecordMillingEditVM : BaseViewModelEdit
    {
        public RecordMillingEditVM()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);

            InitializeBasicData();

            Select = new RelayCommand(() =>
            {
                PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMillingEdit);
                NavigationService.GoTo(PMSViews.PlanSelect);
            });
        }

        private void InitializeBasicData()
        {
            States = new List<string>();
            States.Clear();
            PMSBasicData.SimpleStates.ToList().ForEach(i => States.Add(i));

            MillingTools = new List<string>();
            MillingTools.Clear();
            PMSBasicData.MillingTools.ToList().ForEach(i => MillingTools.Add(i));


            MillingTimes = new List<string>();
            MillingTimes.Clear();
            PMSBasicData.MillingTimes.ToList().ForEach(i => MillingTimes.Add(i));



            GasProtections = new List<string>();
            GasProtections.Clear();
            PMSBasicData.MillingGases.ToList().ForEach(i => GasProtections.Add(i));


        }

        public void SetNew()
        {
            IsNew = true;
            #region 数据初始化
            var model = new DcRecordMilling();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.Composition = "填入成分";
            model.GasProtection = "Ar气";
            model.MaterialSource = "Sanjie";
            model.MillingTool = "行星球磨";
            model.MillingTime = "20min, 2次";
            model.Remark = "";
            model.WeightIn = 0;
            model.WeightOut = 0;
            model.WeightRemain = 0;
            model.Ratio = 0;
            #endregion
            CurrentRecordMilling = model;
        }

        public void SetEdit(DcRecordMilling model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordMilling = model;
            }
        }
        public void SetByDuplicate(DcRecordMilling model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRecordMilling = model;
            }
        }
        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRecordMilling.Composition = plan.Misson.CompositionStandard;
                CurrentRecordMilling.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(plan);
                //RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }



        private void ActionSave()
        {
            if (CurrentRecordMilling != null)
            {
                try
                {
                    using (var dc = new RecordMillingServiceClient())
                    {
                        if (IsNew)
                        {
                            dc.AddRecordMilling(CurrentRecordMilling);
                        }
                        else
                        {
                            dc.UpdateRecordMilling(CurrentRecordMilling);
                        }
                        PMSHelper.ViewModels.RecordMilling.RefreshData();
                        GoBack();
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordMilling);
        }

        private DcRecordMilling currentRecordMilling;

        public DcRecordMilling CurrentRecordMilling
        {
            get { return currentRecordMilling; }
            set
            {
                currentRecordMilling = value;
                RaisePropertyChanged(nameof(CurrentRecordMilling));
            }
        }
        public List<string> States { get; set; }
        public List<string> MillingMaterialSources { get; set; }
        public List<string> MillingTimes { get; set; }
        public List<string> MillingTools { get; set; }
        public List<string> GasProtections { get; set; }
        public RelayCommand Select { get; set; }
    }
}
