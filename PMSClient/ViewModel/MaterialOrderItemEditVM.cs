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
    public class MaterialOrderItemEditVM : BaseViewModelEdit
    {
        public MaterialOrderItemEditVM()
        {
            checkResult = "";
            OrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.SimpleState>(OrderStates);
            InitialCommmands();
        }

        public void SetNew(DcMaterialOrder order)
        {
            if (order != null)
            {
                IsNew = true;

                var item = new DcMaterialOrderItem();
                #region 初始化
                item.ID = Guid.NewGuid();
                item.MaterialOrderID = order.ID;
                item.State = PMSCommon.SimpleState.正常.ToString();
                item.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                item.CreateTime = DateTime.Now;
                var prefix = order.OrderPO.Substring(0, order.OrderPO.Length - 3);
                item.OrderItemNumber = prefix+"-"+ (GetNowItemCount(order)+1).ToString();
                item.Composition = "需求成分";
                item.PMINumber = DateTime.Now.ToString("yyMMdd");
                item.Purity = "5N";
                item.Description = "";
                item.ProvideRawMaterial = "";
                item.UnitPrice = 0;
                item.Weight = 0;
                item.DeliveryDate = DateTime.Now.AddDays(7);
                #endregion

                CurrentMaterialOrderItem = item;
                CheckResult = "";
            }

        }
        private int GetNowItemCount(DcMaterialOrder order)
        {
            if (order!=null)
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    return service.GetMaterialOrderItemCountByMaterialID(order.ID);
                }
            }
            return 1;
        }
        public void SetEdit(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                IsNew = false;
                CurrentMaterialOrderItem = item;
                CheckResult = "";
            }
        }

        public void SetBySelect(DcMaterialNeed need)
        {
            if (need != null)
            {
                CurrentMaterialOrderItem.Composition = need.Composition;
                CurrentMaterialOrderItem.PMINumber = need.PMINumber;
                CurrentMaterialOrderItem.Weight = need.Weight;
                //RaisePropertyChanged(nameof(CurrentMaterialOrderItem));
            }
        }


        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
            Calculator = new RelayCommand(ActionCalculator);
            CheckOrderItemNumberExist = new RelayCommand(ActionCheck);
        }

        private void ActionCheck()
        {
            try
            {
                using (var service=new MaterialOrderServiceClient())
                {
                    if (CurrentMaterialOrderItem!=null)
                    {
                        var result = service.CheckOrderItemNumberExist(CurrentMaterialOrderItem.OrderItemNumber);
                        CheckResult = result ? "被占用" : "可以用";
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionCalculator()
        {
            PMSHelper.ToolViewModels.MaterialNeedCalcualtion.SetRequestView(PMSViews.MaterialOrderItemEdit);
            NavigationService.GoTo(PMSViews.MaterialNeedCalcuationTool);
        }
        public void SetByCalculate(double weight)
        {
            //克到千克的转换
            CurrentMaterialOrderItem.Weight = weight / 1000;
            //RaisePropertyChanged(nameof(CurrentMaterialNeed));
        }
        private void ActionSelect()
        {
            PMSHelper.ViewModels.MaterialNeedSelect.SetRequestView(PMSViews.MaterialOrderItemEdit);
            NavigationService.GoTo(PMSViews.MaterialNeedSelect);
        }

        private void ActionSave()
        {
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new MaterialOrderServiceClient();
                if (IsNew)
                {
                    service.AddMaterialOrderItemByUID(CurrentMaterialOrderItem,uid);
                }
                else
                {
                    service.UpdateMaterialOrderItemByUID(CurrentMaterialOrderItem,uid);
                }
                service.Close();
                PMSHelper.ViewModels.MaterialOrder.RefreshDataItem();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialOrder);
        }

        public List<string> OrderStates { get; set; }

        private DcMaterialOrderItem currentMaterialOrderItem;
        public DcMaterialOrderItem CurrentMaterialOrderItem
        {
            get { return currentMaterialOrderItem; }
            set
            {
                Set(ref currentMaterialOrderItem, value);
            }
        }

        private string checkResult;

        public string CheckResult
        {
            get { return checkResult; }
            set { checkResult = value; RaisePropertyChanged(nameof(CheckResult)); }
        }



        public RelayCommand Select { get; set; }

        public RelayCommand Calculator { get; set; }


        public RelayCommand CheckOrderItemNumberExist { get; set; }

    }
}
