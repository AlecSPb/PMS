using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using PMSClient.CheckLogic;



namespace PMSClient.ViewModel
{
    public class RecordTestEditVM : BaseViewModelEdit
    {
        public RecordTestEditVM()
        {
            InitializeBasic();

            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
            SelectMisson = new RelayCommand(ActionSelectMisson);
            SelectDimensionActual = new RelayCommand(ActionSelectDimensionActual);
        }

        private void InitializeBasic()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.CommonState>(States);

            TestTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.TestType>(TestTypes);

            TestFollowUps = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.TestFollowUps>(TestFollowUps);

            TestDefects = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.TestDefectsTypes>(TestDefects);

            CustomerNames = new List<string>();
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);
        }

        private void ActionSelectDimensionActual()
        {
            PMSHelper.ViewModels.RecordMachineSelect.SetRequestView(PMSViews.RecordTestEdit);
            NavigationService.GoTo(PMSViews.RecordMachineSelect);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcRecordTest();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            model.FollowUps = "发货";
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
            model.State = PMSCommon.CommonState.未录完.ToString();
            model.Weight = "0";
            model.Remark = "";
            model.Resistance = "0";
            model.Sample = "无";
            model.CompositionXRF = "暂无";
            model.Density = "0";
            model.Defects = "无";
            model.OrderDate = DateTime.Now.AddDays(-30);
            model.FollowUps = PMSCommon.TestFollowUps.发货.ToString();
            model.Roughness = "无";
            model.Warping = "未知";
            model.QC = "无";
            model.BackingPlateLot = "无";
            #endregion
            CurrentRecordTest = model;

            originalProductID = "";
        }
        private string originalProductID;
        public void SetEdit(DcRecordTest model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordTest = model;
                originalProductID = model.ProductID;
            }
        }

        public void SetDimensionActual(DcRecordMachine model)
        {
            if (model != null)
            {
                CurrentRecordTest.DimensionActual = UsefulPackage.PMSTranslate.Dimension(model);
            }
        }

        public void SetDuplicate(DcRecordTest model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.CommonState.未录完.ToString();
                CurrentRecordTest = model;
            }
        }

        public void SetBySelectMisson(DcOrder order)
        {
            if (order != null)
            {
                CurrentRecordTest.PMINumber = order.PMINumber;
                CurrentRecordTest.Composition = order.CompositionStandard;
                CurrentRecordTest.CompositionAbbr = order.CompositionAbbr;
                CurrentRecordTest.PO = order.PO;
                CurrentRecordTest.ProductID = "请手动输入";
                CurrentRecordTest.Customer = order.CustomerName;
                CurrentRecordTest.Dimension = order.Dimension;
                CurrentRecordTest.DimensionActual = order.Dimension;
                CurrentRecordTest.OrderDate = order.CreateTime;
            }
        }

        public void SetBySelect(DcPlanWithMisson plan)
        {
            if (plan != null)
            {
                CurrentRecordTest.PMINumber = plan.Misson.PMINumber;
                CurrentRecordTest.Composition = plan.Misson.CompositionStandard;
                CurrentRecordTest.CompositionAbbr = plan.Misson.CompositionAbbr;
                CurrentRecordTest.PO = plan.Misson.PO;

                //如果是440mm，自动加上后缀
                //添加440 加工的大靶材到记录单
                if (plan.Plan.VHPRequirement.Contains("#"))
                {
                    string remark = GetBigNumber(plan.Plan.VHPRequirement);
                    CurrentRecordTest.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan) + remark;
                }
                else
                {
                    CurrentRecordTest.ProductID = UsefulPackage.PMSTranslate.PlanLot(plan);
                }



                CurrentRecordTest.Customer = plan.Misson.CustomerName;
                CurrentRecordTest.Dimension = plan.Misson.Dimension;
                CurrentRecordTest.DimensionActual = plan.Misson.Dimension;
                CurrentRecordTest.OrderDate = plan.Misson.CreateTime;
                //RaisePropertyChanged(nameof(CurrentRecordTest));
            }
        }

        /// <summary>
        /// 从备注字符串中获取#165格式的编号
        /// </summary>
        /// <param name="millling"></param>
        /// <returns></returns>
        private string GetBigNumber(string millling)
        {
            if (string.IsNullOrEmpty(millling))
                return "";
            return System.Text.RegularExpressions.Regex.Match(millling, @"#\d*").Value;
        }

        private void ActionSelectMisson()
        {
            PMSHelper.ViewModels.MissonSelect.SetRequestView(PMSViews.RecordTestEdit);
            PMSHelper.ViewModels.MissonSelect.RefreshData();
            NavigationService.GoTo(PMSViews.MissonSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordTestEdit);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordTest);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "仔细核对每项数据是否准确\r\n确定保存这条记录？"))
            {
                return;
            }
            #region 核验部分

            #endregion

            //新建的时候检查产品ID是否正确
            if (IsNew)
            {
                if (!CheckLogic.RecordTestCheckLogic.IsProductIDUnique(CurrentRecordTest.ProductID))
                {
                    RecordTestCheckLogic.ShowWarningDialog("该产品ID有重复，无法保存，产品ID必须唯一，请仔细核对");
                    return;
                }

                if (!CheckLogic.RecordTestCheckLogic.IsProductIDLogic(CurrentRecordTest.ProductID,
                    CurrentRecordTest.Dimension))
                {
                    if (!PMSDialogService.ShowYesNo("提醒", "改产品ID可能不符合生产惯例,即热压日期1到2天（230mm）后才测试，要继续保存吗？"))
                    {
                        return;
                    }
                }
            }
            else
            {
                if (originalProductID != CurrentRecordTest.ProductID)
                {
                    if (!CheckLogic.RecordTestCheckLogic.IsProductIDUnique(CurrentRecordTest.ProductID))
                    {
                        RecordTestCheckLogic.ShowWarningDialog("该产品ID有重复，无法保存，产品ID必须唯一，请仔细核对");
                        return;
                    }

                    if (!CheckLogic.RecordTestCheckLogic.IsProductIDLogic(CurrentRecordTest.ProductID,
                        CurrentRecordTest.Dimension))
                    {
                        if (!PMSDialogService.ShowYesNo("提醒", "改产品ID可能不符合生产惯例,即热压日期1到2天（230mm）后才测试，要继续保存吗？"))
                        {
                            return;
                        }
                    }
                }
            }

            string composition = CurrentRecordTest.CompositionXRF;
            //检测是否错误输入Si，S，P，B，C之类不可测试的元素
            if (composition.Contains("Si atm%")
                || composition.Contains("S atm%")
                || composition.Contains("P atm%")
                || composition.Contains("B atm%")
                || composition.Contains("C atm%")
                )
            {
                if (!PMSDialogService.ShowYesNo("请问", "成分误包含有Si，S，P，B，C,确定继续保存吗？"))
                {
                    return;
                }
            }

            //密度检查
            string abbr = CurrentRecordTest.CompositionAbbr;
            double density = 0;
            double.TryParse(CurrentRecordTest.Density, out density);

            //if (string.IsNullOrEmpty(CurrentRecordTest.CompositionXRF))
            //{
            //    CurrentRecordTest.CompositionXRF = "无";
            //}

            if (!string.IsNullOrEmpty(abbr) && density != 0)
            {
                CheckResult msg = RecordTestCheckLogic.IsDensityOK(abbr, density);
                if (!msg.IsCheckOK)
                {
                    PMSDialogService.ShowWarning(msg.Message);
                }
            }

            //BridgeLine成分检查警告
            if (!RecordTestCheckLogic.IsBridgeLineCompositionOK(CurrentRecordTest.Customer,
                CurrentRecordTest.CompositionXRF))
            {
                RecordTestCheckLogic.ShowWarningDialog("BridgeLine的成分测试需要有13点数据！");

            }

            if (CurrentRecordTest.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }

            //报废记录添加
            if (CurrentRecordTest.FollowUps.Contains("报废"))
            {
                if (PMSDialogService.ShowYesNo("请问", "确定同时给[报废记录]当中添加该靶材信息吗？已经添加过的，请选择【否】"))
                {

                    var model = new FailureService.DcFailure();
                    model.ID = Guid.NewGuid();
                    model.CreateTime = DateTime.Now;
                    model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                    model.ProductID = CurrentRecordTest.ProductID;
                    model.Composition = CurrentRecordTest.Composition;
                    model.Problem = "测试未通过";
                    model.Details = CurrentRecordTest.PMINumber;
                    model.Stage = "测试";
                    model.Process = "无";
                    model.Remark = CurrentRecordTest.Defects;
                    model.State = PMSCommon.SimpleState.正常.ToString();

                    using (var service = new FailureService.FailureServiceClient())
                    {
                        service.AddFailure(model);
                    }
                }
            }

            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new RecordTestServiceClient();
                if (IsNew)
                {
                    //新建#编号的产品测试记录的时候自动设置为未定
                    if (CurrentRecordTest.ProductID.Contains("#"))
                    {
                        CurrentRecordTest.FollowUps = PMSCommon.TestFollowUps.未定.ToString();
                    }
                    service.AddRecordTestByUID(CurrentRecordTest, uid);
                }
                else
                {
                    service.UpdateRecordTestByUID(CurrentRecordTest, uid);
                }
                service.Close();
                PMSHelper.ViewModels.RecordTest.RefreshData();
                NavigationService.Status("保存成功，请刷新列表");
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> TestDefects { get; set; }
        public List<string> TestTypes { get; set; }
        public List<string> TestFollowUps { get; set; }
        public List<string> States { get; set; }
        public List<string> CustomerNames { get; set; }

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
        public RelayCommand SelectMisson { get; set; }

        public RelayCommand SelectDimensionActual { get; set; }
    }
}
