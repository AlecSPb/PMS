using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSClient.ViewModel
{
    public class MaterialOrderItemSelectVM:BaseViewModelPage
    {
        public MaterialOrderItemSelectVM()
        {
            MaterialOrderItems = new ObservableCollection<DcMaterialOrderItem>();
            All = new RelayCommand(ActionAll);
            GiveUp = new RelayCommand(GoBack);
            Select = new RelayCommand<DcMaterialOrderItem>(ActionSelect);


            SetPageParametersWhenConditionChange();
        }

        private void ActionSelect(DcMaterialOrderItem model)
        {
            if (model!=null)
            {
                switch (requestView)
                {
                    case PMSViews.MaterialInventoryInEdit:
                        PMSHelper.ViewModels.MaterialInventoryInEdit.SetBySelect(model);
                        break;        
                    default:
                        break;
                }
                GoBack();
            }
        }

        private void GoBack()
        {
            NavigationService.GoTo(requestView);
        }

        private void ActionAll()
        {
            SetPageParametersWhenConditionChange();
        }

        private PMSViews requestView;
        public void SetRequestView(PMSViews view)
        {
            this.requestView = view;
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 10;
            var service = new MaterialOrderServiceClient();
            RecordCount = service.GetMaterialOrderItemsCount();
            service.Close();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new MaterialOrderServiceClient();
            var orders = service.GetMaterialOrderItems(skip, take);
            service.Close();
            MaterialOrderItems.Clear();
            orders.ToList<DcMaterialOrderItem>().ForEach(o => MaterialOrderItems.Add(o));
        }
        public ObservableCollection<DcMaterialOrderItem> MaterialOrderItems { get; set; }

        public RelayCommand GiveUp { get; set; }
        public RelayCommand<DcMaterialOrderItem> Select { get; set; }
  
    }
}
