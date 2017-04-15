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
        }

        public void SetNew(DcRecordDelivery delivery)
        {
            IsNew = true;
            var model = new DcRecordDeliveryItem();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.DeliveryID = delivery.ID;
            model.ProductType = PMSCommon.ProductType.Target.ToString();
            model.ProductID = DateTime.Now.ToString("yyMMdd");
            model.Composition = "填写成分";
            model.Abbr = "缩写";
            model.PO = "PO";
            model.Customer = "客户";
            model.Weight = "重量";
            model.DetailRecord = "细节";
            model.Remark = "无";
            model.Position = "A2";
            model.State = PMSCommon.SimpleState.UnDeleted.ToString();
            #endregion
            CurrentRecordDeliveryItem = model;
        }

        public void SetEdit(DcRecordDeliveryItem model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentRecordDeliveryItem = model;
            }
        }

        public void SetBySelect(DcRecordTest test)
        {
            if (test != null)
            {
                CurrentRecordDeliveryItem.ProductID = test.ProductID;
                CurrentRecordDeliveryItem.Composition = test.Composition;
                CurrentRecordDeliveryItem.Abbr = test.CompositionAbbr;
                CurrentRecordDeliveryItem.Customer = test.Customer;
                CurrentRecordDeliveryItem.Weight = test.Weight;
                CurrentRecordDeliveryItem.PO = test.PO;

                //RaisePropertyChanged(nameof(CurrentRecordDeliveryItem));
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
            PMSHelper.ViewModels.RecordTestSelect.SetRequestView(PMSViews.RecordDeliveryItemEdit);
            NavigationService.GoTo(PMSViews.RecordTestSelect);
        }

        private void GoBack()
        {
            NavigationService.GoTo(PMSViews.RecordDelivery);
        }

        private void ActionSave()
        {
            try
            {
                if (CurrentRecordDeliveryItem != null)
                {
                    var service = new RecordDeliveryServiceClient();
                    if (IsNew)
                    {
                        service.AddRecordDeliveryItem(CurrentRecordDeliveryItem);
                    }
                    else
                    {
                        service.UpdateReocrdDeliveryItem(CurrentRecordDeliveryItem);
                    }
                }
                PMSHelper.ViewModels.RecordDelivery.RefreshDataItem();
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

        private DcRecordDeliveryItem currentRecordDeliveryItem;
        public DcRecordDeliveryItem CurrentRecordDeliveryItem
        {
            get
            {
                return currentRecordDeliveryItem;
            }
            set
            {
                currentRecordDeliveryItem = value;
                RaisePropertyChanged(nameof(CurrentRecordDeliveryItem));
            }
        }

        public RelayCommand Select { get; set; }

    }
}
