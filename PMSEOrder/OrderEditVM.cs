using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEOrder.Model;
using PMSEOrder.Service;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace PMSEOrder
{
    public class OrderEditVM : ViewModelBase
    {

        public OrderEditVM()
        {
            NewOrEditIndicator = "New";
            CurrentOrder = new Order();
            GiveUp = new RelayCommand(ActionCloseWindow);
            Save = new RelayCommand(ActionSave);

            CustomerNames = new List<string>();
            CustomerNames.Clear();
            CustomerNames.AddRange(JsonService.GetCustomers());

            ProductTypes = new List<string>();
            ProductTypes.Add("Target");
            ProductTypes.Add("Powder");
            ProductTypes.Add("Granule");

            QuantityUnits = new List<string>();
            QuantityUnits.Add("pcs");
            QuantityUnits.Add("kg");
            QuantityUnits.Add("g");

            OrderStates = new List<string>();
            OrderStates.Add("Deleted");
            OrderStates.Add("UnFinished");
            OrderStates.Add("UnSend");
            OrderStates.Add("Sent");

            Creators = new List<string>();
            Creators.Add("LChiu");
            Creators.Add("ZLi");
            Creators.Add("GFletcher");
        }

        private void ActionCloseWindow()
        {
            Messenger.Default.Send(new NotificationMessage("CloseEditWindow"), "MSG");
        }

        public void SetNew()
        {
            NewOrEditIndicator = "New";
            var model = new Order();
            model.GUIDID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.CustomerName = "";
            model.PO = "";
            model.PODate = DateTime.Today;
            model.Composition = "";
            model.CompositionDetail = "";
            model.ProductType = ProductTypes[0];
            model.Purity = "";
            model.Quantity = 1;
            model.QuantityUnit = QuantityUnits[0];
            model.Dimension = "";
            model.DimensionDetails = "";
            model.Drawing = "";
            model.SampleNeed = "";
            model.SampleNeedRemark = "";
            model.SampleForAnlysis = "";
            model.SampleForAnlysisRemark = "";
            model.DeadLine = DateTime.Now.AddDays(30);
            model.MinimumAcceptDefect = "";
            model.ShipTo = "";
            model.WithBackingPlate = "";
            model.PlateDrawing = "";
            model.SpecialRequirement = "";
            model.BondingRequirement = "";
            model.PartNumber = "";
            model.Remark = "";
            model.OrderState = "UnSend";
            model.Creator = Properties.Settings.Default.Creator;

            CurrentOrder = model;
        }

        public void SetEdit(Order model)
        {
            NewOrEditIndicator = "Edit";
            CurrentOrder = model;
        }

        public void SetDuplicate(Order model)
        {
            NewOrEditIndicator = "New";
            CurrentOrder = model;
            CurrentOrder.GUIDID = Guid.NewGuid();
            CurrentOrder.CreateTime = DateTime.Now;
            CurrentOrder.OrderState = OrderState.UnSend.ToString();
            CurrentOrder.PODate = DateTime.Today;
        }

        private void ActionSave()
        {
            try
            {
                #region 检查逻辑
                if (!CheckService.IsBasicItemNotEmpty(CurrentOrder))
                {
                    XSHelper.XS.MessageBox.ShowError("basic item like\r\n[customer,composition,po,dimension etc]\r\ncan not be empty");
                    return;
                }

                if (!CheckService.IsSeAsGeBondingUsingElastmer(CurrentOrder))
                {
                    if(!XSHelper.XS.MessageBox.ShowYesNo("440 or 444.7 diameter usually needs [Elastomer] bonding,\r\nContinue saving?"))
                    {
                        return;
                    }
                }

                #endregion
                var s = new DataService();
                if (NewOrEditIndicator == "New")
                {
                    #region 新建检查逻辑
                    if (!CheckService.IsPONotRepeat(CurrentOrder))
                    {
                        XSHelper.XS.MessageBox.ShowError($"PO#[{CurrentOrder.PO}] is repeated.\r\nThis may be a duplicate order; Please check");
                        return;
                    }

                    #endregion


                    s.AddOrder(CurrentOrder);
                }
                else if (NewOrEditIndicator == "Edit")
                {
                    s.UpdateOrder(CurrentOrder);
                }

                Messenger.Default.Send(new NotificationMessage("CloseEditWindow"), "MSG");
                Messenger.Default.Send(new NotificationMessage("RefreshMainWindow"), "MSG");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<string> CustomerNames { get; set; }
        public List<string> QuantityUnits { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> OrderStates { get; set; }
        public List<string> Creators { get; set; }

        private string newOrEditIndicator;
        public string NewOrEditIndicator
        {
            get
            {
                return newOrEditIndicator;
            }

            set
            {
                newOrEditIndicator = value;
                RaisePropertyChanged(nameof(NewOrEditIndicator));
            }
        }

        public Order CurrentOrder { get; set; }
        public RelayCommand GiveUp { get; set; }
        public RelayCommand Save { get; set; }
    }
}
