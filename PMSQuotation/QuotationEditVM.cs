using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSQuotation.Models;
using PMSQuotation.Services;
using XSHelper;
using GalaSoft.MvvmLight.Messaging;

namespace PMSQuotation
{
    public class QuotationEditVM : ViewModelBase
    {
        public QuotationEditVM()
        {
            db_service = new QuotationDbService();
            calc_service = new CalculationService();
            dict_service = new DataDictionaryService();


            ModelStates = new List<string>();
            ModelStates.AddRange(Helpers.QuotationHelper.GetQuotationStates());

            #region Commands
            Save = new RelayCommand(ActionSave);
            #endregion

        }


        private VMState vMState;

        public void SetNew()
        {

            string calculationCurrency = dict_service.GetString("basecurrency");
            vMState = VMState.New;
            EditState = vMState.ToString();

            CurrentQuotation = new Quotation();

            CurrentQuotation.State = QuotationState.UnFinished.ToString();
            CurrentQuotation.CurrencyType = calculationCurrency;
            CurrentQuotation.CreateTime = DateTime.Now;
            CurrentQuotation.LastUpdateTime = DateTime.Now;
            CurrentQuotation.ExpirationTime = DateTime.Now.AddMonths(1);
            CurrentQuotation.Lot = Helpers.QuotationHelper.GetDefaultLot();
            CurrentQuotation.Creator = dict_service.GetString("creator");
            CurrentQuotation.KeyWord = "";

            CurrentQuotation.ContactInfo_Customer = "++++";
            CurrentQuotation.ContactInfo_Self = dict_service.GetString("contactInfo_self_zh_cn");


            CurrentQuotation.PackageFee = 0;
            CurrentQuotation.PackageRemark = "";
            CurrentQuotation.ShippingFee = 0;
            CurrentQuotation.ShippingRemark = "";
            CurrentQuotation.CustomFee = 0;
            CurrentQuotation.CustomRemark = "";
            CurrentQuotation.TaxFee = 0;
            CurrentQuotation.TaxRemark = "";
            CurrentQuotation.Remark = "none";
        }

        public void SetEdit(Quotation model)
        {
            vMState = VMState.Edit;
            EditState = vMState.ToString();
            CurrentQuotation = model;
        }

        public void SetClone(Quotation model)
        {
            vMState = VMState.New;
            EditState = vMState.ToString();
            CurrentQuotation = new Quotation();
            CurrentQuotation.State = QuotationState.UnFinished.ToString();
            CurrentQuotation.CurrencyType = model.CurrencyType;
            CurrentQuotation.CreateTime = DateTime.Now;
            CurrentQuotation.LastUpdateTime = DateTime.Now;
            CurrentQuotation.ExpirationTime = DateTime.Now.AddMonths(1);
            CurrentQuotation.Lot = Helpers.QuotationHelper.GetDefaultLot();
            CurrentQuotation.Creator = dict_service.GetString("creator");
            CurrentQuotation.KeyWord = model.KeyWord;

            CurrentQuotation.ContactInfo_Customer = model.ContactInfo_Customer;
            CurrentQuotation.ContactInfo_Self = model.ContactInfo_Self;


            CurrentQuotation.PackageFee = model.PackageFee;
            CurrentQuotation.PackageRemark = model.PackageRemark;
            CurrentQuotation.ShippingFee = model.ShippingFee;
            CurrentQuotation.ShippingRemark = model.ShippingRemark;
            CurrentQuotation.CustomFee = model.CustomFee;
            CurrentQuotation.CustomRemark = model.CustomRemark;
            CurrentQuotation.TaxFee = model.TaxFee;
            CurrentQuotation.TaxRemark = model.TaxRemark;
            CurrentQuotation.Remark = model.Remark;


        }

        private QuotationDbService db_service;
        private CalculationService calc_service;
        private DataDictionaryService dict_service;
        private void ActionSave()
        {
            if (CurrentQuotation == null) return;
            try
            {
                #region AutoCalculation
                var result = calc_service.Calculate(CurrentQuotation);
                CurrentQuotation.TaxFee = result.TaxFee;
                CurrentQuotation.TotalCost = result.TargetFee + result.ExtraFee + result.TaxFee;
                #endregion

                if (vMState == VMState.New)
                {
                    db_service.Add(CurrentQuotation);
                    XS.MessageBox.ShowInfo("New Saved");
                }
                else if (vMState == VMState.Edit)
                {
                    CurrentQuotation.LastUpdateTime = DateTime.Now;
                    db_service.Update(CurrentQuotation);
                    XS.MessageBox.ShowInfo("Edit Saved");
                }

                Messenger.Default.Send(new NotificationMessage("CloseEditWindow"), "MSG");
                Messenger.Default.Send(new NotificationMessage("RefreshMain"), "MSG");
            }
            catch (Exception ex)
            {

                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }

        }
        public List<string> ModelStates { get; set; }

        public string EditState { get; set; }

        public Quotation CurrentQuotation { get; set; }
        public RelayCommand Save { get; set; }

    }
}
