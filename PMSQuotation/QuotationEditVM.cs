using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSQuotation.Models;
using XSHelper;

namespace PMSQuotation
{
    public class QuotationEditVM : ViewModelBase
    {
        public QuotationEditVM()
        {
            CurrencyTypes = new List<string>();
            CurrencyTypes.Add("RMB");
            CurrencyTypes.Add("USD");


            ModelStates = new List<string>();
            ModelStates.AddRange(Helpers.QuotationHelper.GetModelStates());

            #region Commands
            Save = new RelayCommand(ActionSave);
            #endregion

        }

        private VMState vMState;

        public void SetNew()
        {
            vMState = VMState.New;
            EditState = vMState.ToString();

            CurrentQuotation = new Quotation();

            CurrentQuotation.State = ModelState.Finished.ToString();
            CurrentQuotation.CurrencyType = CurrencyTypes[0];
            CurrentQuotation.CreateTime = DateTime.Now;
            CurrentQuotation.LastUpdateTime = DateTime.Now;
            CurrentQuotation.ExpirationTime = DateTime.Now.AddMonths(1);
            CurrentQuotation.Lot = Helpers.QuotationHelper.GetDefaultLot();


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



        }


        private void ActionSave()
        {
            if (vMState == VMState.New)
            {

                XS.MessageBox.ShowInfo("New Saved");
            }
            else if (vMState == VMState.Edit)
            {

                XS.MessageBox.ShowInfo("Edit Saved");
            }
        }

        public List<string> CurrencyTypes { get; set; }
        public List<string> ModelStates { get; set; }

        public string EditState { get; set; }

        public Quotation CurrentQuotation { get; set; }
        public RelayCommand Save { get; set; }

    }
}
