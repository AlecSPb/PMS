using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ExtraService;

namespace PMSClient.ViewModel
{
    public class FillingToolVM : BaseViewModelPage
    {
        public FillingToolVM()
        {
            Intialize();
        }

        private void Intialize()
        {
            SearchElementA = SearchElementB = "";
        }

        #region 属性
        private string searchElementA;
        public string SearchElementA
        {
            get
            {
                return searchElementA;
            }
            set
            {
                if (searchElementA == value)
                    return;
                RaisePropertyChanged(nameof(SearchElementA));
            }
        }
        private string searchElementB;
        public string SearchElementB
        {
            get
            {
                return searchElementB;
            }
            set
            {
                if (searchElementB == value)
                    return;
                RaisePropertyChanged(nameof(SearchElementB));
            }
        }
        public ObservableCollection<DcToolFilling> ToolFillings { get; set; }
        #endregion

    }
}
