using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class ProductEditVM:BaseViewModelEdit
    {
        public ProductEditVM()
        {

        }
        public void SetNew()
        {
            IsNew = true;
            var model = new DcProduct();
            #region 初始化
          

            #endregion
            CurrentProduct = model;
        }
        public void SetEdit(DcProduct model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentProduct = model;
            }
        }
        public void SetNew(DcProduct model)
        {
            if (model != null)
            {
                IsNew = true;
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentProduct = model;
            }
        }
        public void SetBySelect(DcRecordTest model)
        {
            if (model != null)
            {
                CurrentProduct.Composition = model.Composition;
                CurrentProduct.Abbr = model.CompositionAbbr;
                CurrentProduct.PO = model.PO;
                CurrentProduct.ProductID = model.ProductID;
                CurrentProduct.Customer = model.Customer;
                CurrentProduct.Dimension = model.Dimension;
                CurrentProduct.DimensionActual = model.DimensionActual;
                CurrentProduct.Defects = model.Defects;
                CurrentProduct.ProductType = PMSCommon.ProductType.靶材.ToString();
                CurrentProduct.Remark = model.Remark;
                //RaisePropertyChanged(nameof(CurrentProduct));
            }
        }
        public void SetBySelect(DcRecordBonding model)
        {
            if (model != null)
            {
                CurrentProduct.ProductID = model.TargetProductID;
                CurrentProduct.Customer = model.TargetCustomer;
                CurrentProduct.Composition = model.TargetComposition;
                CurrentProduct.Abbr = model.TargetAbbr;
                CurrentProduct.PO = model.TargetPO;
                CurrentProduct.Dimension = model.TargetDimension;
                CurrentProduct.DimensionActual = model.TargetDimensionActual;
                CurrentProduct.Defects = model.TargetDefects;
                CurrentProduct.ProductType = PMSCommon.ProductType.绑定.ToString();
                CurrentProduct.Remark = model.PlateLot;


                //RaisePropertyChanged(nameof(CurrentProduct));
            }
        }
        private void ActionSelect()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.ProductEdit);
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Product);
        }

        private void ActionSave()
        {
            try
            {
                var service = new ProductServiceClient();
                if (IsNew)
                {
                    service.AddProduct(CurrentProduct);
                }
                else
                {
                    service.UpdateProduct(CurrentProduct);
                }
                PMSHelper.ViewModels.Product.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public ObservableCollection<string> TestTypes { get; set; }
        public ObservableCollection<string> States { get; set; }
        private DcProduct currentProduct;
        public DcProduct CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                Set(nameof(CurrentProduct), ref currentProduct, value);
            }
        }
        public RelayCommand Select { get; set; }
    }
}
