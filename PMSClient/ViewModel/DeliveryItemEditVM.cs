using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class DeliveryItemEditVM : BaseViewModelEdit
    {
        public DeliveryItemEditVM()
        {
            InitializeBasicData();

            InitialCommands();
        }

        private void InitializeBasicData()
        {
            States = new List<string>();
            PMSBasicData.SimpleStates.ToList().ForEach(s => States.Add(s));

            ProductTypes = new List<string>();
            PMSBasicData.ProductTypes.ToList().ForEach(i => ProductTypes.Add(i));

            GoodPositions = new List<string>();
            PMSBasicData.GoodPositions.ToList().ForEach(i => GoodPositions.Add(i));

            PackNumbers = new List<string>();
            PMSBasicData.PackNumbers.ToList().ForEach(i => PackNumbers.Add(i));
        }

        public void SetNew(DcDelivery delivery)
        {
            IsNew = true;
            var model = new DcDeliveryItem();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.DeliveryID = delivery.ID;
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.ProductID = DateTime.Now.ToString("yyMMdd");
            model.Composition = "填写成分";
            model.Abbr = "缩写";
            model.PO = "PO";
            model.Customer = "客户";
            model.Weight = "重量";
            model.DetailRecord = "细节";
            model.Remark = "无";
            model.PackNumber = 1;
            model.Position = "A2";
            model.State = PMSCommon.SimpleState.正常.ToString();
            #endregion
            CurrentDeliveryItem = model;
        }

        public void SetEdit(DcDeliveryItem model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentDeliveryItem = model;
            }
        }

        public void SetBySelect(DcRecordTest model)
        {
            if (model != null)
            {
                CurrentDeliveryItem.ProductID = model.ProductID;
                CurrentDeliveryItem.Composition = model.Composition;
                CurrentDeliveryItem.Abbr = model.CompositionAbbr;
                CurrentDeliveryItem.Customer = model.Customer;
                CurrentDeliveryItem.Weight = model.Weight;
                CurrentDeliveryItem.PO = model.PO;

                //RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }
        public void SetBySelect(DcProduct model)
        {
            if (model != null)
            {
                CurrentDeliveryItem.ProductID = model.ProductID;
                CurrentDeliveryItem.Composition = model.Composition;
                CurrentDeliveryItem.Abbr = model.Abbr;
                CurrentDeliveryItem.Customer = model.Customer;
                CurrentDeliveryItem.Weight = model.Weight;
                CurrentDeliveryItem.PO = model.PO;

                //RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }

        private void InitialCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
        }

        private void ActionSelect()
        {
            PMSHelper.ViewModels.ProductSelect.SetRequestView(PMSViews.DeliveryItemEdit);
            NavigationService.GoTo(PMSViews.ProductSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.Delivery);
        }

        private void ActionSave()
        {
            try
            {
                if (CurrentDeliveryItem != null)
                {
                    var service = new DeliveryServiceClient();
                    if (IsNew)
                    {
                        service.AddDeliveryItem(CurrentDeliveryItem);
                    }
                    else
                    {
                        service.UpdateDeliveryItem(CurrentDeliveryItem);
                    }
                }
                PMSHelper.ViewModels.Delivery.RefreshDataItem();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }
        public List<string> States { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> GoodPositions { get; set; }
        public List<string> PackNumbers { get; set; }
        private DcDeliveryItem currentDeliveryItem;
        public DcDeliveryItem CurrentDeliveryItem
        {
            get
            {
                return currentDeliveryItem;
            }
            set
            {
                currentDeliveryItem = value;
                RaisePropertyChanged(nameof(CurrentDeliveryItem));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
