using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.SimpleMaterialService;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class SimpleMaterialEditVM : BaseViewModelEdit
    {
        public SimpleMaterialEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);
            InitializeCommands();
        }
        public List<string> States { get; set; }

        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcSimpleMaterial();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.ElementName = "A";
            model.UnitPrice = 0;
            model.Remark = "无";

            #endregion
            CurrentSimpleMaterial = model;
        }
        public void SetDuplicate(DcSimpleMaterial model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentSimpleMaterial = new DcSimpleMaterial();
                CurrentSimpleMaterial.ID = Guid.NewGuid();
                CurrentSimpleMaterial.CreateTime = DateTime.Now;
                CurrentSimpleMaterial.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentSimpleMaterial.State = PMSCommon.SimpleState.正常.ToString();
                CurrentSimpleMaterial.ElementName = model.ElementName;
                CurrentSimpleMaterial.UnitPrice = model.UnitPrice;
                CurrentSimpleMaterial.Remark = model.Remark;
            }
        }
        public void SetEdit(DcSimpleMaterial model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentSimpleMaterial = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.SimpleMaterial);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentSimpleMaterial.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new SimpleMaterialServiceClient();
                if (IsNew)
                {
                    service.AddSimpleMaterial(CurrentSimpleMaterial);
                }
                else
                {
                    service.UpdateSimpleMaterial(CurrentSimpleMaterial);
                }
                service.Close();
                PMSHelper.ViewModels.SimpleMaterial.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private DcSimpleMaterial currentSimpleMaterial;
        public DcSimpleMaterial CurrentSimpleMaterial
        {
            get { return currentSimpleMaterial; }
            set
            {
                currentSimpleMaterial = value;
                RaisePropertyChanged(nameof(CurrentSimpleMaterial));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
