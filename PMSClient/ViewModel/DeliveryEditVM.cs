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
            var model = new DcRecordDelivery();
            model.ID = Guid.NewGuid();
            model.InvoiceNumber = "发票号";
            model.DeliveryName =$"FH{DateTime.Now.ToString("yyMMdd")}";
            model.DeliveryNumber = "UPS";
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.SimpleState.UnDeleted.ToString();
            model.PackageInformation = "重量未知";
            model.PackageType = PMSCommon.PackageType.木箱.ToString();
            model.Remark = "";
            model.ShipTime = DateTime.Now;
            model.Address = "这里填写发货地址";
            model.Country = "美国";
            #endregion
            CurrentRecordDelivery = model;
        }
        public void SetEdit(DcRecordDelivery model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordDelivery = model;
            }
        }




        private void InitialProperties()
        {
            OrderStates = new ObservableCollection<string>();
            var states = PMSBasicData.SimpleStates;
            states.ToList().ForEach(s => OrderStates.Add(s));

            Countries = new ObservableCollection<string>();
            var countries = PMSBasicData.Countries;
            countries.ToList().ForEach(s => Countries.Add(s));

            PackageTypes = new List<string>();
            PMSBasicData.PackageTypes.ToList().ForEach(i => PackageTypes.Add(i));
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordDelivery);
        }

        private void ActionSave()
        {
            try
            {
                var service = new RecordDeliveryServiceClient();
                if (IsNew)
                {
                    service.AddRecordDelivery(CurrentRecordDelivery);
                }
                else
                {
                    service.UpdateReocrdDelivery(CurrentRecordDelivery);
                }
                PMSHelper.ViewModels.RecordDelivery.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }


        #region Properties
        private DcRecordDelivery currentRecordDelivery;
        public DcRecordDelivery CurrentRecordDelivery
        {
            get
            {
                return currentRecordDelivery;
            }
            set
            {
                currentRecordDelivery = value;
                RaisePropertyChanged(nameof(CurrentRecordDelivery));
            }
        }
        public ObservableCollection<string> OrderStates { get; set; }
        public ObservableCollection<string> Countries { get; set; }

        public List<string> PackageTypes { get; set; }
        #endregion

    }
}
