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
        public void SetKeyProperties(ModelObject model)
        {
            IsNew = model.IsNew;
            CurrentMaterialOrderItem = model.Model as DcMaterialOrderItem;
        }

        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialOrder }));
            Save = new RelayCommand(ActionSave);
        }

        private void ActionSave()
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
            NavigationService.GoTo(new MsgObject() { NavigateTo = VToken.MaterialOrder });
            NavigationService.Refresh(VToken.MaterialOrderItemRefresh);
        }
        public ObservableCollection<string> OrderStates { get; set; }
        public DcMaterialOrderItem CurrentMaterialOrderItem { get; set; }
    }
}
