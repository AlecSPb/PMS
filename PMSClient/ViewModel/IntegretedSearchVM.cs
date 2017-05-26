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
    public class IntegretedSearchVM : BaseViewModelEdit
    {
        public IntegretedSearchVM()
        {

            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(States);

            ProductTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ProductType>(ProductTypes);

            CustomerNames = new List<string>();
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);


            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcFeedBack();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.正常.ToString();
            model.Composition = "";
            model.Customer = "Midsummer";
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.ProductID = "";
            model.ProcessWay = "未处理";
            model.Remark = "";

            #endregion
            CurrentFeedBack = model;
        }
        public void SetDuplicate(DcFeedBack model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentFeedBack = model;
                CurrentFeedBack.ID = Guid.NewGuid();
                CurrentFeedBack.CreateTime = DateTime.Now;
                CurrentFeedBack.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentFeedBack.State = PMSCommon.SimpleState.正常.ToString();
                CurrentFeedBack.ProductType = model.ProductType;
                CurrentFeedBack.Composition = model.Composition;
                CurrentFeedBack.Customer = model.Customer;
                CurrentFeedBack.ProcessWay = model.ProcessWay;


                CurrentFeedBack.Remark = model.Remark;
            }
        }
        public void SetEdit(DcFeedBack model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentFeedBack = model;
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.FeedBack);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentFeedBack.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new FeedBackServiceClient();
                if (IsNew)
                {
                    service.AddFeedBack(CurrentFeedBack, uid);
                }
                else
                {
                    service.UpdateFeedBack(CurrentFeedBack, uid);
                }
                service.Close();
                PMSHelper.ViewModels.FeedBack.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> ProductTypes { get; set; }
        public List<string> States { get; set; }
        public List<string> CustomerNames { get; set; }

        private DcFeedBack currentFeedBack;
        public DcFeedBack CurrentFeedBack
        {
            get { return currentFeedBack; }
            set
            {
                currentFeedBack = value;
                RaisePropertyChanged(nameof(CurrentFeedBack));
            }
        }

    }
}
