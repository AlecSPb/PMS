using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.ExtraService;
using PMSClient.ToolService;


namespace PMSClient.ViewModel
{
    public class MillingToolWindowVM : BaseViewModelPage
    {
        public MillingToolWindowVM()
        {
            Intialize();
        }

        private void Intialize()
        {
            searchElementA = searchElementB=searchElementC = "";
            ToolSieves = new ObservableCollection<DcToolSieve>();

            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcToolSieve>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PrintList = new RelayCommand(ActionPrintList);
            SetPageParametersWhenConditionChange();

            Select = new RelayCommand<DcToolSieve>(ActionSelect);
        }

        private void ActionSelect(DcToolSieve obj)
        {
            if (obj == null) return;
            PMSHelper.ViewModels.RecordMillingEdit.SetSieveDescription(obj.SearchID);
        }

        private void ActionPrintList()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定生成索引单吗？"))
            {
                return;
            }
            try
            {
                var tool = new ReportsHelperNew.ReportMillingTool();
                tool.Intialize("筛网工具索引单");
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
            SearchElementA = SearchElementB=SearchElementC = "";

            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcToolSieve arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(new string[] { "管理员", "制粉组", "生产经理" });
        }

        private void ActionEdit(DcToolSieve model)
        {
            PMSHelper.ViewModels.MillingToolEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.MillingToolEdit);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup(new string[] { "管理员", "制粉组", "生产经理" });
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.MillingToolEdit.SetNew();
            NavigationService.GoTo(PMSViews.MillingToolEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 40;
            using (var service = new ToolSieveServiceClient())
            {
                RecordCount = service.GetToolSieveUsedCount(SearchElementC, SearchElementA, SearchElementB);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new ToolSieveServiceClient())
            {
                var data = service.GetToolSieveUsed(SearchElementC, SearchElementA, SearchElementB, skip, take);
                ToolSieves.Clear();
                data.ToList().ForEach(o => ToolSieves.Add(o));
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
        private string searchElementC;
        public string SearchElementC
        {
            get
            {
                return searchElementC;
            }
            set
            {
                searchElementC = value;
                RaisePropertyChanged(nameof(SearchElementC));
            }
        }
        public ObservableCollection<DcToolSieve> ToolSieves { get; set; }
        #endregion
        #region 命令
        public RelayCommand Add { get; set; }
        public RelayCommand<DcToolSieve> Edit { get; set; }
        public RelayCommand<DcToolSieve> Select { get; set; }
        public RelayCommand PrintList { get; set; }
        #endregion

    }
}
