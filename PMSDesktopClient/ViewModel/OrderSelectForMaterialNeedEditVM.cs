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
    public class OrderSelectForMaterialNeedEditVM:OrderSelectBaseVM
    {
        private string goToViewName;
        public OrderSelectForMaterialNeedEditVM(MsgObject msg)
        {
            goToViewName = msg.ModelObject.ToString();

            SelectOrder = new RelayCommand<PMSMainService.DcOrder>(ActionSelectOrder);
            GiveUp = new RelayCommand(() => NavigationService.GoTo(VNCollection.MaterialNeed));
        }
        private void ActionSelectOrder(DcOrder obj)
        {
            if (obj != null)
            {
                var materialNeed = ModelInitializer.GetMaterialNeedByOrder(obj);

                MsgObject mo = new PMSDesktopClient.MsgObject();
                mo.GoToToken =goToViewName;
                mo.ModelObject = materialNeed;
                mo.IsAdd = true;

                NavigationService.GoToWithParameter(mo);
            }
        }
        public RelayCommand<DcOrder> SelectOrder { get; set; }
        public RelayCommand GiveUp { get; set; }
    }
}
