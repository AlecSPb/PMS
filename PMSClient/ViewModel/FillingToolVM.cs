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
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcToolFilling>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
        }

        private void ActionAll()
        {
            throw new NotImplementedException();
        }

        private void ActionSearch()
        {
            throw new NotImplementedException();
        }

        private bool CanEdit(DcToolFilling arg)
        {
            throw new NotImplementedException();
        }

        private void ActionEdit(DcToolFilling obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAdd()
        {
            throw new NotImplementedException();
        }

        private void ActionAdd()
        {
            throw new NotImplementedException();
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
        #region 命令
        public RelayCommand Add { get; set; }
        public RelayCommand<DcToolFilling> Edit { get; set; }
        #endregion

    }
}
