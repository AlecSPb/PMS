using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SimpleMaterialService;

namespace PMSClient.ViewModel
{
    public class SimpleMaterialVM : BaseViewModelPage
    {
        public SimpleMaterialVM()
        {
            searchElementName = "";
            SimpleMaterials = new ObservableCollection<DcSimpleMaterial>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcSimpleMaterial>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcSimpleMaterial>(ActionDuplicate, CanDuplicate);
        }

        private void ActionDuplicate(DcSimpleMaterial obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.SimpleMaterialEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.SimpleMaterialEdit);
                }
            }

        }

        private bool CanDuplicate(DcSimpleMaterial arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(groupnames);
        }

        private void ActionAll()
        {
            SearchElementName = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private string[] groupnames = { "管理员", "统筹组" };

        private bool CanEdit(DcSimpleMaterial arg)
        {
            return PMSHelper.CurrentSession.IsInGroup(groupnames);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsInGroup(groupnames);
        }

        private void ActionEdit(DcSimpleMaterial model)
        {
            PMSHelper.ViewModels.SimpleMaterialEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.SimpleMaterialEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.SimpleMaterialEdit.SetNew();
            NavigationService.GoTo(PMSViews.SimpleMaterialEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchElementName;
        public string SearchElementName
        {
            get { return searchElementName; }
            set { searchElementName = value; RaisePropertyChanged(nameof(SearchElementName)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new SimpleMaterialServiceClient())
            {
                RecordCount = service.GetSimpleMaterialCount(SearchElementName);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new SimpleMaterialServiceClient())
            {
                var orders = service.GetSimpleMaterial(skip, take, SearchElementName);
                SimpleMaterials.Clear();
                orders.ToList().ForEach(o => SimpleMaterials.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcSimpleMaterial> SimpleMaterials { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcSimpleMaterial> Edit { get; set; }
        public RelayCommand<DcSimpleMaterial> Duplicate { get; set; }
        #endregion

    }
}
