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
            States = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.TestResultState));
            states.ToList().ForEach(s => States.Add(s));

            TestTypes = new ObservableCollection<string>();
            var testTypes = Enum.GetNames(typeof(PMSCommon.TestType));
            testTypes.ToList().ForEach(t => TestTypes.Add(t));


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
            model.ProductID = DateTime.Now.ToString("yyMMdd") + "-M-1";
            model.CompositionXRF = "暂无";
            model.Dimension = "要求尺寸";
            model.DimensionActual = "实际尺寸";
            model.PO = "PO";
            model.CompositionAbbr = "成分缩写";
            model.Customer = "客户信息";
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.TestType = PMSCommon.TestType.Product.ToString();
            model.State = PMSCommon.CommonState.UnChecked.ToString();
            model.Weight = "100";
            model.Remark = "";
            model.Resistance = "OutOfRange";
            model.Sample = "无需样品";
            model.CompositionXRF = "暂无";
            model.Density = "0";
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
        public void SetBySelect(DcMissonWithPlan plan)
        {
            if (plan != null)
            {
                CurrentRecordTest.Composition = plan.CompositionStandard;
                CurrentRecordTest.CompositionAbbr = plan.CompositionAbbr;
                CurrentRecordTest.PO = plan.PO;
                CurrentRecordTest.ProductID = plan.PlanDate.ToString("yyMMdd") + "-" + plan.VHPDeviceCode + "-1";
                CurrentRecordTest.Customer = plan.CustomerName;
                CurrentRecordTest.Dimension = plan.Dimension;
                CurrentRecordTest.DimensionActual = plan.Dimension;

                RaisePropertyChanged(nameof(CurrentRecordTest));
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

                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public ObservableCollection<string> TestTypes { get; set; }
        public ObservableCollection<string> States { get; set; }
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
