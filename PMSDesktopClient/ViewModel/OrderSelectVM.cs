using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.CommandWpf;
using PMSDesktopClient.ServiceReference;

namespace PMSDesktopClient.ViewModel
{
    public class OrderSelectVM : OrderVM
    {
        public OrderSelectVM()
        {
            SelectEmpty = new RelayCommand(ActionSelectEmpty);
            SelectOrder = new RelayCommand<ServiceReference.DcOrder>(ActionSelectOrder);
        }

        private void ActionSelectOrder(DcOrder obj)
        {
            if (obj!=null)
            {
                var materialNeed = new DcMaterialNeed();
                materialNeed.Id = Guid.NewGuid();
                materialNeed.State = PMSCommon.NonOrderState.UnDeleted.ToString();
                materialNeed.CreateTime = DateTime.Now;
                materialNeed.Creator=

                MessageObject mo = new PMSDesktopClient.MessageObject();
                mo.ViewName = nameof(MaterialNeedView);
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
