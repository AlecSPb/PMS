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
            OrderStates = new ObservableCollection<string>();
            var states = Enum.GetNames(typeof(PMSCommon.SimpleState));
            states.ToList().ForEach(s => OrderStates.Add(s));
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
                item.State = PMSCommon.SimpleState.UnDeleted.ToString();
                item.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                item.CreateTime = DateTime.Now;
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
            }

        }

        public void SetEdit(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                IsNew = false;
                CurrentMaterialOrderItem = item;
            }
        }

        public void SetBySelect(DcMaterialNeed need)
        {
            if (need != null)
            {
                CurrentMaterialOrderItem.Composition = need.Composition;
                CurrentMaterialOrderItem.PMINumber = need.PMINumber;
                CurrentMaterialOrderItem.Weight = need.Weight;
                RaisePropertyChanged(nameof(CurrentMaterialOrderItem));
            }
        }


        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
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
                var service = new MaterialOrderServiceClient();
                if (IsNew)
                {
                    service.AddMaterialOrderItem(CurrentMaterialOrderItem);
                }
                else
                {
                    service.UpdateMaterialOrderItem(CurrentMaterialOrderItem);
                }
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

        public ObservableCollection<string> OrderStates { get; set; }

        private DcMaterialOrderItem currentMaterialOrderItem;
        public DcMaterialOrderItem CurrentMaterialOrderItem
        {
            get { return currentMaterialOrderItem; }
            set
            {
                Set(ref currentMaterialOrderItem, value);
            }
        }

        public RelayCommand Select { get; set; }



    }
}
