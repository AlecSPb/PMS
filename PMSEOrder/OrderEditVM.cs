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
            model.CustomerName = CustomerNames[0];
            model.PO = "None";
            model.Composition = "CuInGaSe";
            model.CompositionDetail = "None";
            model.ProductType = ProductTypes[0];
            model.Purity = "99.995%";
            model.Quantity = 1;
            model.QuantityUnit = QuantityUnits[0];
            model.Dimension = "230mm OD x 4mm";
            model.DimensionDetails = "None";
            model.Drawing = "None";
            model.SampleNeed = "None";
            model.SampleNeedRemark = "None";
            model.SampleForAnlysis = "None";
            model.SampleForAnlysisRemark = "None";
            model.DeadLine = DateTime.Now.AddDays(30);
            model.MinimumAcceptDefect = "None";
            model.ShipTo = "TCB";
            model.WithBackingPlate = "No";
            model.PlateDrawing = "None";
            model.SpecialRequirement = "None";
            model.BondingRequirement = "None";
            model.PartNumber = "None";
            model.Remark = "None";
            model.OrderState = "UnSend";

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
        }

        private void ActionSave()
        {
            try
            {
                var s = new DataService();
                if (NewOrEditIndicator == "New")
                {
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
