using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;

namespace PMSTabletClient.ViewModel
{
    public class ProductReportVM:ViewModelBase
    {
        public ProductReportVM()
        {
            BackToProduct = new RelayCommand(ActionBackToProduct);
        }

        private void ActionBackToProduct()
        {
            NavigationWizard.GoToProduct();
        }


        #region Commands
        public RelayCommand BackToProduct { get; set; }
        #endregion
    }
}
