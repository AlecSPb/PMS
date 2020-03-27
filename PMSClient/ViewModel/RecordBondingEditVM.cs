using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class RecordBondingEditVM : BaseViewModelEdit
    {
        public RecordBondingEditVM()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Sign = new RelayCommand<string>(ActionSign);
            SelectTest = new RelayCommand(ActionSelectTest);
            SelectPlate = new RelayCommand(ActionSelectPlate);

            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.BondingState>(States);

            BondingDefects = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.BondingDefects, BondingDefects);

            PlateTypes = new List<string>();
            PlateTypes.Clear();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.PlateTypes, PlateTypes);
        }

        private void ActionSelectPlate()
        {
            PMSHelper.ViewModels.PlateSelect.SetRequestView(PMSViews.RecordBondingSimpleEdit);
            PMSBatchHelper.SetPlateBatchEnable(false);
            NavigationService.GoTo(PMSViews.PlateSelect);
        }

        private void ActionSelectTest()
        {
            PMSHelper.ViewModels.RecordTestSelect.SetRequestView(PMSViews.RecordBondingSimpleEdit);
            PMSHelper.ViewModels.RecordTestSelect.RefreshData();
            PMSBatchHelper.SetRecordTestBatchEnable(true);
            NavigationService.GoTo(PMSViews.RecordTestSelect);
        }

        public void SetNew()
        {
            IsNew = true;
            #region 初始化
            var model = new DcRecordBonding();
            model.ID = Guid.NewGuid();
            //0.0
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.BondingState.未完成.ToString();
            model.PlanBatchNumber = 1;
            model.TargetProductID = "无";
            model.TargetComposition = "无";
            model.TargetDimension = "无";
            model.PlateType = "新背板230";
            model.CoverPlateNumber = "无";
            model.WeldingRate = 0;

            //暂时用不到
            model.TargetAbbr = "";
            model.TargetPO = "";
            model.TargetPMINumber = UsefulPackage.PMSTranslate.PMINumber();
            model.TargetWeight = "";
            model.TargetDimensionActual = "";
            model.TargetDefects = "";
            model.TargetDetailRecord = "";

            model.PlateLot = "暂无";
            model.Remark = "";


            #endregion
            CurrentRecordBonding = model;
        }

        public void SetEdit(DcRecordBonding model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordBonding = model;
            }
        }

        public void SetBySelect(DcRecordTest model)
        {
            if (model != null)
            {
                CurrentRecordBonding.TargetProductID = model.ProductID;
                CurrentRecordBonding.TargetCustomer = model.Customer;
                CurrentRecordBonding.TargetComposition = model.Composition;
                CurrentRecordBonding.TargetAbbr = model.CompositionAbbr;
                CurrentRecordBonding.TargetPO = model.PO;
                CurrentRecordBonding.TargetPMINumber = model.PMINumber;
                CurrentRecordBonding.TargetWeight = model.Weight;
                CurrentRecordBonding.TargetDimension = model.Dimension;
                CurrentRecordBonding.TargetDimensionActual = model.DimensionActual;
                CurrentRecordBonding.TargetDefects = model.Defects;
            }
        }

        public void SetBySelect(DcPlate model)
        {
            if (model != null)
            {
                CurrentRecordBonding.PlateLot = model.PrintNumber;
            }


        }


        private void ActionSign(string param)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentRecordBonding.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            #region 暂时作废
            //var currentUser = PMSHelper.CurrentSession.CurrentUser.UserName;
            //var currentTime = DateTime.Now;
            ////通过参数判断是按下的是那个签名按钮
            ////并设置对应的签名和时间
            //switch (param)
            //{
            //    //case "00":
            //    //    CurrentRecordBonding.Creator = currentUser;
            //    //    CurrentRecordBonding.CreateTime = currentTime;
            //    //    break;
            //    case "10":
            //        CurrentRecordBonding.TargetPerson = currentUser;
            //        CurrentRecordBonding.TargetCheckTime = currentTime;
            //        break;
            //    case "20":
            //        CurrentRecordBonding.PlatePerson = currentUser;
            //        CurrentRecordBonding.PlateCheckTime = currentTime;
            //        break;
            //    case "30":
            //        CurrentRecordBonding.TargetPreProcessPerson = currentUser;
            //        CurrentRecordBonding.TargetPreProcessCheckTime = currentTime;
            //        break;
            //    case "40":
            //        CurrentRecordBonding.PlatePreProcessPerson = currentUser;
            //        CurrentRecordBonding.PlatePreProcessCheckTime = currentTime;
            //        break;
            //    case "50":
            //        CurrentRecordBonding.WeldPerson = currentUser;
            //        CurrentRecordBonding.WeldCheckTime = currentTime;
            //        break;
            //    case "60":
            //        CurrentRecordBonding.WarpagePerson = currentUser;
            //        CurrentRecordBonding.WarpageCheckTime = currentTime;
            //        break;
            //    case "70":
            //        CurrentRecordBonding.DimensionPerson = currentUser;
            //        CurrentRecordBonding.DimensionCheckTime = currentTime;
            //        break;
            //    case "80":
            //        CurrentRecordBonding.BindingPerson = currentUser;
            //        CurrentRecordBonding.BindingCheckTime = currentTime;
            //        break;
            //    case "90":
            //        CurrentRecordBonding.SprayPerson = currentUser;
            //        CurrentRecordBonding.SprayCheckTime = currentTime;
            //        break;
            //    case "100":
            //        CurrentRecordBonding.ApperancePerson = currentUser;
            //        CurrentRecordBonding.ApperanceCheckTime = currentTime;
            //        break;
            //    default:
            //        break;
            //}
            #endregion
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                using (var service = new RecordBondingServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordBongdingByUID(CurrentRecordBonding, uid);
                    }
                    else
                    {
                        service.UpdateRecordBongdingByUID(CurrentRecordBonding, uid);
                    }
                }

                NavigationService.Status("保存成功，请刷新列表");
                PMSHelper.ViewModels.RecordBonding.Refresh();
                GoBack();
                //if (!PMSDialogService.ShowYesNo("请问", "留在本页Yes，返回列表No？"))
                //{
                //    GoBack();
                //}

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionSave()
        {

        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordBonding);
        }

        public List<string> States { get; set; }
        public List<string> BondingDefects { get; set; }

        public List<string> PlateTypes { get; set; }

        private DcRecordBonding currentRecordBonding;

        public DcRecordBonding CurrentRecordBonding
        {
            get { return currentRecordBonding; }
            set { currentRecordBonding = value; RaisePropertyChanged(nameof(CurrentRecordBonding)); }
        }


        public RelayCommand<string> Sign { get; set; }
        public RelayCommand SelectTest { get; set; }
        public RelayCommand SelectPlate { get; set; }
    }
}
