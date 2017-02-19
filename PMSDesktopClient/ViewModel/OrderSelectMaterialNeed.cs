using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSDesktopClient.PMSMainService;

namespace PMSDesktopClient.ViewModel
{
    public class OrderSelectMaterialNeed:OrderSelectBaseVM
    {
        private VT goToViewName;
        public OrderSelectMaterialNeed(MsgObject msg)
        {
            goToViewName = msg.MsgToken;

            SelectOrder = new RelayCommand<PMSMainService.DcOrder>(ActionSelectOrder);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(new MsgObject() { MsgToken=VT.MaterialNeed}));
        }
        private void ActionSelectOrder(DcOrder obj)
        {
            if (obj != null)
            {
                var materialNeed = ModelInitializer.GetMaterialNeedByOrder(obj);

                MsgObject msg = new PMSDesktopClient.MsgObject();
                msg.MsgToken =goToViewName;
                msg.MsgModel = new PMSDesktopClient.ModelObject() { IsNew = true, Model = obj };
                NavigationService.GoTo(msg);
            }
        }
        public RelayCommand<DcOrder> SelectOrder { get; set; }
        public RelayCommand GiveUp { get; set; }
    }
}
