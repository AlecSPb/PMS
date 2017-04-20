using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class RecordTestEditVM : BaseViewModelEdit
    {
        public RecordTestEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.CommonState>(States);

            TestTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.TestType>(TestTypes);

            TestDefects = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.TestDefectsTypes>(TestDefects);

            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcRecordTest();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Composition = "成分";
            model.ProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.CompositionXRF = "暂无";
            model.Dimension = "要求尺寸";
            model.DimensionActual = "实际尺寸";
            model.PO = "PO";
            model.CompositionAbbr = "成分缩写";
            model.Customer = "客户信息";
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.TestType = PMSCommon.TestType.靶材.ToString();
            model.State = PMSCommon.CommonState.未核验.ToString();
            model.Weight = "100";
            model.Remark = "";
            model.Resistance = "";
            model.Sample = "无需样品";
            model.CompositionXRF = "暂无";
            model.Density = "0";
            model.Defects = "无";

            #endregion
            CurrentRecordTest = model;
        }
        public void SetEdit(DcRecordTest model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordTest = model;
            }
        }

        public void SetDimensionActual(DcRecordMachine model)
        {
            if (model!=null)
            {
                CurrentRecordTest.DimensionActual = $"{model.Diameter1}mm OD x {model.Thickness1}mm";
            }
        }

        public void SetNew(DcRecordTest model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentRecordTest = model;
            }
        }
        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRecordTest.Composition = plan.Misson.CompositionStandard;
                CurrentRecordTest.CompositionAbbr = plan.Misson.CompositionAbbr;
                CurrentRecordTest.PO = plan.Misson.PO;
                CurrentRecordTest.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan);
                CurrentRecordTest.Customer = plan.Misson.CustomerName;
                CurrentRecordTest.Dimension = plan.Misson.Dimension;
                CurrentRecordTest.DimensionActual = plan.Misson.Dimension;
                CurrentRecordTest.CompositionAbbr = plan.Misson.CompositionAbbr;
                //RaisePropertyChanged(nameof(CurrentRecordTest));
            }
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordTestEdit);
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordTest);
        }

        private void ActionSave()
        {
            try
            {
                var service = new RecordTestServiceClient();
                if (IsNew)
                {
                    service.AddRecordTest(CurrentRecordTest);
                }
                else
                {
                    service.UpdateRecordTest(CurrentRecordTest);
                }
                service.Close();
                PMSHelper.ViewModels.RecordTest.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> TestDefects { get; set; }
        public List<string> TestTypes { get; set; }
        public List<string> States { get; set; }

        private DcRecordTest currentRecordTest;
        public DcRecordTest CurrentRecordTest
        {
            get { return currentRecordTest; }
            set
            {
                Set(nameof(CurrentRecordTest), ref currentRecordTest, value);
            }
        }
        public RelayCommand Select { get; set; }

    }
}
