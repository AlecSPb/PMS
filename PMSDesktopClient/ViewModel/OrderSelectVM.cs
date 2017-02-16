using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.PMSMainService;
using PMSDesktopClient.View;

namespace PMSDesktopClient.ViewModel
{
    public class OrderSelectVM : OrderVM
    {
        public OrderSelectVM()
        {

        }
        //要转到的页面
        private string goToViewName;

        public OrderSelectVM(string gotoviewname)
        {
            goToViewName = gotoviewname;


            SelectEmpty = new RelayCommand(ActionSelectEmpty);
            SelectOrder = new RelayCommand<PMSMainService.DcOrder>(ActionSelectOrder);
        }

        private void ActionSelectOrder(DcOrder obj)
        {
            if (obj!=null)
            {
                var materialNeed = new DcMaterialNeed();
                materialNeed.Id = Guid.NewGuid();
                materialNeed.State = PMSCommon.NonOrderState.UnDeleted.ToString();
                materialNeed.CreateTime = DateTime.Now;
                materialNeed.Creator = (App.Current as App).CurrentUser.UserName;

                MessageObject mo = new PMSDesktopClient.MessageObject();
                mo.ViewName = goToViewName;
                mo.ModelObject = obj;
                mo.IsAdd = false;

                NavigationService.GoToWithParameter(mo);
            }
        }

        private void ActionSelectEmpty()
        {
            var nModel = new MessageObject();

        }

        public RelayCommand SelectEmpty { get; set; }
        public RelayCommand<DcOrder> SelectOrder { get; set; }

    }
}
