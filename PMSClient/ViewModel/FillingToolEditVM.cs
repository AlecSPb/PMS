using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ExtraService;

namespace PMSClient.ViewModel
{
    public class FillingToolEditVM : BaseViewModelEdit
    {
        public FillingToolEditVM()
        {
            Intialize();
        }

        private void Intialize()
        {
            GiveUp = new RelayCommand(ActionGiveUp);
            Save = new RelayCommand(ActionSave);
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);
        }

        private void ActionGiveUp()
        {
            GoBack();
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcToolFilling();
            model.Id = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.ToolNumber = 0;
            model.CompositionAbbr = "CuInGaSe";
            model.Remark = "";

            CurrentToolFilling = model;
        }

        public void SetEdit(DcToolFilling model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentToolFilling = model;
            }
        }
        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.FillingTool);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录?"))
            {
                return;
            }
            if (CurrentToolFilling.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                using (var service = new ToolInventoryServiceClient())
                {
                    if (IsNew)
                    {
                        service.AddToolFilling(CurrentToolFilling);
                    }
                    else
                    {
                        service.UpdateToolFilling(CurrentToolFilling);
                    }
                }
                PMSHelper.ViewModels.FillingTool.Refresh();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        #region 属性

        private DcToolFilling currentToolFilling;
        public DcToolFilling CurrentToolFilling
        {
            get
            {
                return currentToolFilling;
            }
            set
            {
                currentToolFilling = value;
                RaisePropertyChanged(nameof(CurrentToolFilling));
            }
        }

        public List<String> States { get; set; }

        #endregion
    }
}
