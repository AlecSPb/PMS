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


            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.CommonState>(States);
        }

        private void ActionSelectTest()
        {
            PMSHelper.ViewModels.RecordTestSelect.SetRequestView(PMSViews.RecordBondingEdit);
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
            model.State = PMSCommon.CommonState.未核验.ToString();
            model.InstructionCode = "SOP";
            model.TargetProductID = UsefulPackage.PMSTranslate.PlanLot();
            model.TargetComposition = "靶材成分";
            model.TargetDimension = "靶材尺寸";
            //暂时用不到
            model.TargetAbbr = "";
            model.TargetPO = "";
            model.TargetWeight = "";
            model.TargetDimensionActual = "";
            model.TargetDefects = "";
            model.TargetDetailRecord = "";
            //暂时用不到
            //1.0
            model.TargetAppearance = "外观检查";
            model.TargetWarpageCheck = "翘曲检查";
            model.TargetThicknessCheck = "厚度检查";
            model.TargetDiameterCheck = "直径检查";
            model.TargetPerson = "无";
            model.TargetCheckTime = DateTime.Now;

            //2.0
            model.PlateLot = "铜板编号";
            model.PlateMaterial = "CuCr";
            model.PlateDimension = "237mm  ODx 11 mm";
            model.PlateUseCount = "1";
            model.PlateHardness = "65HRB";
            model.PlateSuplier = "GuangHan";
            model.PlateLastWeldMaterial = "In";
            model.PlateAppearance = "良好";
            model.PlatePerson = "无";
            model.PlateCheckTime = DateTime.Now;

            //3.0
            model.TargetPreProcessRecord = "正常";
            model.TargetPreProcessPerson = "无";
            model.TargetPreProcessCheckTime = DateTime.Now;

            //4.0
            model.PlatePreProcessRecord = "正常";
            model.PlatePreProcessPerson = "无";
            model.PlatePreProcessCheckTime = DateTime.Now;

            //5.0
            model.WeldMaterial = "In";
            model.WeldCuStringDiameter = "3.0";
            model.WeldHold = "4个";
            model.WeldPerson = "无";
            model.WeldCheckTime = DateTime.Now;
            //6.0
            model.WarpageFix = "正常";
            model.WarpagePerson = "无";
            model.WarpageCheckTime = DateTime.Now;
            //7.0
            model.DimensionCheck = "正常";
            model.DimensionWarpageCheck = "无";
            model.DimensionPerson = "无";
            model.DimensionCheckTime = DateTime.Now;
            //
            model.BindingCheck = "正常";
            model.BindingPerson = "无";
            model.BindingCheckTime = DateTime.Now;
            //
            model.SprayCheck = "正常";
            model.SprayPerson = "无";
            model.SprayCheckTime = DateTime.Now;
            //
            model.CleanCheck = "正常";
            model.CleanPerson = "无";
            model.CleanCheckTime = DateTime.Now;
            //
            model.ApperanceCheck = "正常";
            model.ApperancePerson = "无";
            model.ApperanceCheckTime = DateTime.Now;
            //
            model.PackCheck = "正常";
            model.PackPerson = "无";
            model.PackCheckTime = DateTime.Now;

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
                CurrentRecordBonding.TargetWeight = model.Weight;
                CurrentRecordBonding.TargetDimension = model.Dimension;
                CurrentRecordBonding.TargetDimensionActual = model.DimensionActual;
                CurrentRecordBonding.TargetDefects = model.Defects;
            }
        }

        public void SetBySelect()
        {
            //TODO:以后用作从中间库房选择背板信息用
        }


        private void ActionSign(string param)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            var currentUser = PMSHelper.CurrentSession.CurrentUser.UserName;
            var currentTime = DateTime.Now;
            //通过参数判断是按下的是那个签名按钮
            //并设置对应的签名和时间
            switch (param)
            {
                case "00":
                    //CurrentRecordBonding.Creator = currentUser;
                    //CurrentRecordBonding.CreateTime = currentTime;
                    break;
                case "10":
                    CurrentRecordBonding.TargetPerson = currentUser;
                    CurrentRecordBonding.TargetCheckTime = currentTime;
                    break;
                case "20":
                    CurrentRecordBonding.PlatePerson = currentUser;
                    CurrentRecordBonding.PlateCheckTime = currentTime;
                    break;
                case "30":
                    CurrentRecordBonding.TargetPreProcessPerson = currentUser;
                    CurrentRecordBonding.TargetPreProcessCheckTime = currentTime;
                    break;
                case "40":
                    CurrentRecordBonding.PlatePreProcessPerson = currentUser;
                    CurrentRecordBonding.PlatePreProcessCheckTime = currentTime;
                    break;
                case "50":
                    CurrentRecordBonding.WeldPerson = currentUser;
                    CurrentRecordBonding.WeldCheckTime = currentTime;
                    break;
                case "60":
                    CurrentRecordBonding.WarpagePerson = currentUser;
                    CurrentRecordBonding.WarpageCheckTime = currentTime;
                    break;
                case "70":
                    CurrentRecordBonding.DimensionPerson = currentUser;
                    CurrentRecordBonding.DimensionCheckTime = currentTime;
                    break;
                case "80":
                    CurrentRecordBonding.BindingPerson = currentUser;
                    CurrentRecordBonding.BindingCheckTime = currentTime;
                    break;
                case "90":
                    CurrentRecordBonding.SprayPerson = currentUser;
                    CurrentRecordBonding.SprayCheckTime = currentTime;
                    break;
                case "100":
                    CurrentRecordBonding.ApperancePerson = currentUser;
                    CurrentRecordBonding.ApperanceCheckTime = currentTime;
                    break;
                default:
                    break;
            }

            try
            {
                using (var service = new RecordBondingServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddRecordBongding(CurrentRecordBonding);
                    }
                    else
                    {
                        service.UpdateRecordBongding(CurrentRecordBonding);
                    }
                }
                //PMSDialogService.ShowYes("Success", $"{param}-{currentUser}-{currentTime}");
                NavigationService.ShowStatusMessage("保存成功，请刷新列表");
                GoBack();
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

        private DcRecordBonding currentRecordBonding;

        public DcRecordBonding CurrentRecordBonding
        {
            get { return currentRecordBonding; }
            set { currentRecordBonding = value; RaisePropertyChanged(nameof(CurrentRecordBonding)); }
        }


        public RelayCommand<string> Sign { get; set; }
        public RelayCommand SelectTest { get; set; }
    }
}
