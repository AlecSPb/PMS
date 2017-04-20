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
        }
        public void SetNew()
        {
            IsNew = true;
            #region 初始化
            var model = new DcRecordBonding();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.CommonState.未核验.ToString();
            model.InstructionCode = "标准作业流程";
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
            model.TargetAppearance = "外观检查";
            model.TargetWarpageCheck = "翘曲检查";
            model.TargetThicknessCheck = "厚度检查";
            model.TargetDiameterCheck = "直径检查";
            model.TargetPerson = "";
            model.TargetCheckTime = DateTime.Now;



            #endregion
            CurrentRecordBonding = model;
        }

        public void SetEdit(DcRecordBonding model)
        {
            IsNew = false;
            CurrentRecordBonding = model;
        }

        public void SetBySelect(DcRecordTest model)
        {

        }

        public void SetBySelect()
        {
            //TODO:以后用作从中间库房选择背板信息用
        }

        private void ActionSave()
        {

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
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordBonding);
        }



        private DcRecordBonding currentRecordBonding;

        public DcRecordBonding CurrentRecordBonding
        {
            get { return currentRecordBonding; }
            set { currentRecordBonding = value;RaisePropertyChanged(nameof(CurrentRecordBonding)); }
        }




    }
}
