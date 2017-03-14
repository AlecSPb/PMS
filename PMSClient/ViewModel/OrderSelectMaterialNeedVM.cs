using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class OrderSelectMaterialNeedVM : OrderSelectBaseVM
    {
        public OrderSelectMaterialNeedVM()
        {

            SelectOrder = new RelayCommand<MainService.DcOrder>(ActionSelectOrder);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken = VToken.MaterialNeed }));
        }
        private void ActionSelectOrder(DcOrder order)
        {
            if (order != null)
            {
                var materialNeed = EmptyModel.GetMaterialNeedByOrder(order);

                MsgObject msg = new PMSClient.MsgObject();
                msg.MsgToken = VToken.MaterialNeedEdit;
                var materialneed = EmptyModel.GetMaterialNeedByOrder(order);
                msg.MsgModel = new PMSClient.ModelObject() { IsNew = true, Model = materialNeed };
                NavigationService.GoTo(msg);
            }
        }
        public RelayCommand<DcOrder> SelectOrder { get; set; }
        public RelayCommand GiveUp { get; set; }
    }
}
