using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;

namespace PMSClient.ViewModel
{
    public class DeliveryEditVM : BaseViewModelEdit
    {
        public DeliveryEditVM()
        {
            InitialCommands();
            InitialProperties();
        }

        public void SetNew()
        {
            IsNew = true;
            #region 初始化
            var model = new DcDelivery();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.DeliveryState.未核验.ToString();
            model.DeliveryName = $"FH{DateTime.Now.ToString("yyMMdd")}";
            model.InvoiceNumber = "无";
            model.DeliveryExpress = "UPS";
            model.DeliveryNumber = "";
            model.PackageInformation = "无";
            model.PackageType = PMSCommon.PackageType.木箱.ToString();
            model.Remark = "";
            model.ShipTime = DateTime.Now;
            model.Address = "无";
            model.FinishTime = DateTime.Now;
            model.Country = PMSCommon.CountryRegion.美国.ToString();
            model.PackageWeight = "";
            model.LastCheckIDCollection = "";
            model.IsCustomerSigned = false;
            model.CustomerSignedDate = DateTime.Today;
            model.CustomerSignedDetails = "";
            model.LastUpdateTime = DateTime.Now;
            model.Receiver = "";
            #endregion
            CurrentDelivery = model;
        }
        public void SetEdit(DcDelivery model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentDelivery = model;
            }
        }




        private void InitialProperties()
        {
            OrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.DeliveryState>(OrderStates);

            Countries = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.CountryRegion>(Countries);

            PackageTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.PackageType>(PackageTypes);

            DeliveryExpresses = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.DeliveryExpress>(DeliveryExpresses);
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Delivery);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentDelivery.State == "作废")
            {
                PMSDialogService.Show("作废功能被限制");
                return;
            }
            try
            {
                var service = new DeliveryServiceClient();
                CurrentDelivery.LastUpdateTime = DateTime.Now;
                if (IsNew)
                {
                    service.AddDeliveryByUID(CurrentDelivery, PMSHelper.CurrentSession.CurrentUser.UserName);
                }
                else
                {
                    service.UpdateDeliveryByUID(CurrentDelivery, PMSHelper.CurrentSession.CurrentUser.UserName);
                }
                service.Close();
                PMSHelper.ViewModels.Delivery.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }


        #region Properties
        private DcDelivery currentDelivery;
        public DcDelivery CurrentDelivery
        {
            get
            {
                return currentDelivery;
            }
            set
            {
                currentDelivery = value;
                RaisePropertyChanged(nameof(CurrentDelivery));
            }
        }
        public List<string> OrderStates { get; set; }
        public List<string> Countries { get; set; }
        public List<string> PackageTypes { get; set; }
        public List<string> DeliveryExpresses { get; set; }
        #endregion

    }
}
