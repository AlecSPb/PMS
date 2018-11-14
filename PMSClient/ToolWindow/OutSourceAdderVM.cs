using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using PMSClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ToolWindow
{
    public class OutSourceAdderVM : BaseViewModelEdit
    {
        public OutSourceAdderVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.InventoryState>(States);
            States.Remove("作废");

            ProductTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.ProductType>(ProductTypes);

            GoodPositions = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.GoodPosition>(GoodPositions);

            CustomerNames = new List<string>();
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);

            ProductNumbers = new List<int>();
            for (int i = 1; i < 100; i++)
            {
                ProductNumbers.Add(i);
            }

            ProductNumber = 1;

            InitializeCommands();

            SetNew();
        }

        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcProduct();
            #region 初始化
            model.ID = Guid.NewGuid();
            model.ProductID = UsefulPackage.PMSTranslate.OutSourceLot();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.Composition = "WTi";
            model.Abbr = "WTi";
            model.Weight = "5.0";
            model.PO = "TBD";
            model.Customer = "Midsummer";
            model.Position = PMSCommon.GoodPosition.A1.ToString();
            model.ProductType = PMSCommon.ProductType.靶材.ToString();
            model.State = PMSCommon.InventoryState.库存.ToString();
            model.Remark = "无";

            model.Dimension = "237mm x 7.0mm";
            model.DimensionActual = "237mm x 7.0mm";
            model.Defects = "无";



            #endregion
            CurrentProduct = model;
        }


        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Product);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", $"确定保存【{ProductNumber}】条外包记录？"))
            {
                return;
            }

            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new ProductServiceClient();
                string orignal_id = CurrentProduct.ProductID.Substring(0, 9);
                if (IsNew)
                {
                    for (int i = 1; i <= ProductNumber; i++)
                    {
                        CurrentProduct.ID = Guid.NewGuid();
                        CurrentProduct.ProductID = $"{orignal_id}-{i}";
                        CurrentProduct.CreateTime = DateTime.Now;
                        service.AddProductByUID(CurrentProduct, uid);
                    }
                }
                service.Close();
                PMSHelper.ViewModels.Product.RefreshData();
                PMSDialogService.Show("添加成功");
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> ProductTypes { get; set; }
        public List<string> States { get; set; }

        public List<string> GoodPositions { get; set; }

        public List<string> CustomerNames { get; set; }


        public List<int> ProductNumbers { get; set; }

        private DcProduct currentProduct;
        public DcProduct CurrentProduct
        {
            get { return currentProduct; }
            set
            {
                currentProduct = value;
                RaisePropertyChanged(nameof(CurrentProduct));
            }
        }

        private int productNumber;
        public int ProductNumber
        {
            get { return productNumber; }
            set
            {
                productNumber = value;
                RaisePropertyChanged(nameof(ProductNumber));
            }
        }

    }
}
