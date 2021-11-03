﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSQuotation.Models;
using PMSQuotation.Services;
using XSHelper;

namespace PMSQuotation
{
    public class QuotationItemEditVM : ViewModelBase
    {
        public QuotationItemEditVM()
        {
            ModelStates = new List<string>();
            ModelStates.AddRange(Helpers.QuotationHelper.GetModelStates());


            db_service = new QuotationDbService();
            Save = new RelayCommand(ActionSave);
        }

        private VMState vMState;

        public void SetNew(int quotationid)
        {
            vMState = VMState.New;
            EditState = vMState.ToString();
            CurrentQuotationItem = new QuotationItem();
            CurrentQuotationItem.CreateTime = DateTime.Now;
            CurrentQuotationItem.Creator = db_service.GetDataDictByKey("Creator").DataValue;
            CurrentQuotationItem.QuotationID = quotationid;
            CurrentQuotationItem.Composition = "";
            CurrentQuotationItem.Specification = "";
            CurrentQuotationItem.UnitPrice = 0;
            CurrentQuotationItem.Quantity = 1;
            CurrentQuotationItem.TotalPrice = 0;
            CurrentQuotationItem.Note = "";
            CurrentQuotationItem.UnitPriceDetail = "";
            CurrentQuotationItem.State = QuotationState.Checked.ToString();

        }
        public void SetEdit(QuotationItem model)
        {
            vMState = VMState.Edit;
            EditState = vMState.ToString();
            CurrentQuotationItem = model;
        }

        public void SetClone(QuotationItem item)
        {
            vMState = VMState.New;
            EditState = vMState.ToString();
            CurrentQuotationItem = new QuotationItem();
            CurrentQuotationItem.CreateTime = DateTime.Now;
            CurrentQuotationItem.Creator = db_service.GetDataDictByKey("Creator").DataValue;
            CurrentQuotationItem.QuotationID = item.QuotationID;
            CurrentQuotationItem.Composition = item.Composition;
            CurrentQuotationItem.Specification = item.Specification;
            CurrentQuotationItem.UnitPrice = item.UnitPrice;
            CurrentQuotationItem.Quantity = item.Quantity;
            CurrentQuotationItem.TotalPrice = item.TotalPrice;
            CurrentQuotationItem.Note = item.Note;
            CurrentQuotationItem.UnitPriceDetail = item.UnitPriceDetail;
            CurrentQuotationItem.State = item.State;

        }
        private void ActionSave()
        {
            if (CurrentQuotationItem == null) return;
            try
            {
                if (vMState == VMState.New)
                {
                    db_service.AddItem(CurrentQuotationItem);
                    XS.MessageBox.ShowInfo("New Saved");
                }
                else if (vMState == VMState.Edit)
                {
                    db_service.UpdateItem(CurrentQuotationItem);
                    XS.MessageBox.ShowInfo("Edit Saved");
                }

                Messenger.Default.Send(new NotificationMessage("CloseItemEditWindow"), "MSG");
                Messenger.Default.Send(new NotificationMessage("RefreshMain"), "MSG");
            }
            catch (Exception ex)
            {

                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }
        }

        public List<string> ModelStates { get; set; }
        private QuotationDbService db_service;

        private QuotationItem currentQuotationItem;

        public QuotationItem CurrentQuotationItem
        {
            get { return currentQuotationItem; }
            set { currentQuotationItem = value; RaisePropertyChanged(nameof(CurrentQuotationItem)); }
        }

        public string EditState { get; set; }
        public RelayCommand Save { get; set; }


    }
}
