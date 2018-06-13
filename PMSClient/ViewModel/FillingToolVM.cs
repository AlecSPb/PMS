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
            ToolFillings = new ObservableCollection<DcToolFilling>();

            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcToolFilling>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PrintList = new RelayCommand(ActionPrintList);
            SetPageParametersWhenConditionChange();


        }

        private void ActionPrintList()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定生成索引单吗？"))
            {
                return;
            }
            try
            {
                var tool = new ReportsHelperNew.ReportFillingTool();
                tool.Intialize("装料具索引单");
                tool.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        public void Refresh()
        {
            ActionAll();
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

        private void ActionEdit(DcToolFilling model)
        {
            PMSHelper.ViewModels.FillingToolEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.FillingToolEdit);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordVHP);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.FillingToolEdit.SetNew();
            NavigationService.GoTo(PMSViews.FillingToolEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 40;
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
                searchElementA = value;
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
                searchElementB = value;
                RaisePropertyChanged(nameof(SearchElementB));
            }
        }
        public ObservableCollection<DcToolFilling> ToolFillings { get; set; }
        #endregion
        #region 命令
        public RelayCommand Add { get; set; }
        public RelayCommand<DcToolFilling> Edit { get; set; }
        public RelayCommand PrintList { get; set; }
        #endregion

    }
}
