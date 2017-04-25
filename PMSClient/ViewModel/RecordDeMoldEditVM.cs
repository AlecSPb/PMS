using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using PMSCommon;

namespace PMSClient.ViewModel
{
    public class RecordDeMoldEditVM : BaseViewModelEdit
    {
        public RecordDeMoldEditVM()
        {
            DeMoldTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.DeMoldType>(DeMoldTypes);

            States = new List<string>();
            PMSBasicDataService.SetListDS<SimpleState>(States);


            Save = new RelayCommand(ActionSave);
            GiveUp = new RelayCommand(ActionGiveUp);
            Select = new RelayCommand(ActionSelect);
            Calculator = new RelayCommand(AcionCalculator);
        }
        public void SetDensity(double density,double ratiodensity)
        {
            CurrentRecordDeMold.Density = density;
            CurrentRecordDeMold.RatioDensity = ratiodensity;
           
        }
        private void AcionCalculator()
        {
            var vm = PMSHelper.ToolViewModels.DensityEstamator;
            vm.SetRequestView(PMSViews.RecordDeMoldEdit);
            vm.SetCalculationItem(CurrentRecordDeMold);
            vm.CalculateDensity();
            NavigationService.GoTo(PMSViews.DensityEstamator);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordDeMoldEdit);
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        public void SetKeyProperties(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentRecordDeMold = model.Model as DcRecordDeMold;
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcRecordDeMold();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot();
            model.DeMoldType = PMSCommon.DeMoldType.手动轻松.ToString();
            model.PlanType = PMSCommon.VHPPlanType.加工.ToString();
            model.PMINumber = UsefulPackage.PMSTranslate.PMINumber();
            model.Dimension = "无";
            model.CalculateDimension = "无";
            model.CalculationDensity = 0;
            model.Density = 0;
            model.RatioDensity = 0;
            model.Composition = "成分";
            model.Temperature1 = "100";
            model.Temperature2 = "25";
            model.Weight = 0;
            model.Diameter1 = 0;
            model.Diameter2 = 0;
            model.Thickness1 = 0;
            model.Thickness2 = 0;
            model.Thickness3 = 0;
            model.Thickness4 = 0;
            model.Remark = "无";
            #endregion
            CurrentRecordDeMold = model;
        }
        public void SetEdit(DcRecordDeMold model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordDeMold = model;
            }
        }

        public void SetByDuplicate(DcRecordDeMold model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;

                CurrentRecordDeMold = model;
            }
        }

        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRecordDeMold.Composition = plan.Misson.CompositionStandard;
                CurrentRecordDeMold.VHPPlanLot = UsefulPackage.PMSTranslate.PlanLot(plan);
                CurrentRecordDeMold.PMINumber = plan.Misson.PMINumber;
                CurrentRecordDeMold.Dimension = plan.Misson.Dimension;
                CurrentRecordDeMold.PlanType = plan.Plan.PlanType;
                CurrentRecordDeMold.CalculationDensity = plan.Plan.CalculationDensity;
                CurrentRecordDeMold.CalculateDimension = $"{plan.Plan.MoldDiameter.ToString("F2")}mm OD x {plan.Plan.Thickness}mm";
                //RaisePropertyChanged(nameof(CurrentRecordDeMold));
            }
        }
        private void ActionGiveUp()
        {
            GoBack();
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordDeMold);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                using (var service = new RecordDeMoldServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordDeMoldByUID(CurrentRecordDeMold, uid);
                    }
                    else
                    {
                        service.UpdateRecordDeMoldByUID(CurrentRecordDeMold, uid);
                    }
                }
                PMSHelper.ViewModels.RecordDeMold.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcRecordDeMold currentRecordDeMold;
        public DcRecordDeMold CurrentRecordDeMold
        {
            get { return currentRecordDeMold; }
            set { currentRecordDeMold = value; RaisePropertyChanged(nameof(CurrentRecordDeMold)); }
        }

        public RelayCommand Select { get; set; }
        public RelayCommand Calculator { get; set; }

        public List<string> States { get; set; }

        public List<string> DeMoldTypes { get; set; }
    }
}
