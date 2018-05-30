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
            searchElementA = searchElementB = "";

            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcToolFilling>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);

            SetPageParametersWhenConditionChange();


        }

        private void ActionAll()
        {
            SearchElementA = SearchElementB = "";

            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcToolFilling arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordVHP);
        }

        private void ActionEdit(DcToolFilling obj)
        {
            throw new NotImplementedException();
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordVHP);
        }

        private void ActionAdd()
        {
            throw new NotImplementedException();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new ToolInventoryServiceClient())
            {
                RecordCount = service.GetToolFillingsCount(SearchElementA, SearchElementB);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new ToolInventoryServiceClient())
            {
                var data = service.GetToolFillings(skip, take, SearchElementA, SearchElementB);
                ToolFillings.Clear();
                data.ToList().ForEach(o => ToolFillings.Add(o));
            }
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
