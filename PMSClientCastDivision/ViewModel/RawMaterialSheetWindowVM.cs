using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.Other;

namespace PMSClient.ViewModel
{
    public class RawMaterialSheetWindowVM : BaseViewModelPage
    {
        public RawMaterialSheetWindowVM()
        {
            searchLot = searchComposition = "";
            RawMaterialSheets = new ObservableCollection<DcRawMaterialSheet>();

            InitializeCommands();

            SetPageParametersWhenConditionChange();
        }

        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRawMaterialSheet>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Duplicate = new RelayCommand<DcRawMaterialSheet>(ActionDuplicate, CanDuplicate);
            Select = new RelayCommand<DcRawMaterialSheet>(ActionSelect);
        }

        public event EventHandler<string> FillData;

        private void ActionSelect(DcRawMaterialSheet obj)
        {
            FillData?.Invoke(this, $"{obj.Composition}:{obj.Lot};");
        }

        private void ActionDuplicate(DcRawMaterialSheet obj)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                if (obj != null)
                {
                    PMSHelper.ViewModels.RawMaterialSheetEdit.SetDuplicate(obj);
                    NavigationService.GoTo(PMSViews.RawMaterialSheetEdit);
                }
            }

        }

        private bool CanDuplicate(DcRawMaterialSheet arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }

        private void ActionAll()
        {
            SearchLot = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanEdit(DcRawMaterialSheet arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditMaterialOrder);
        }

        private void ActionEdit(DcRawMaterialSheet model)
        {
            PMSHelper.ViewModels.RawMaterialSheetEdit.SetEdit(model);
            NavigationService.GoTo(PMSViews.RawMaterialSheetEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RawMaterialSheetEdit.SetNew();
            NavigationService.GoTo(PMSViews.RawMaterialSheetEdit);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }


        private string searchLot;
        public string SearchLot
        {
            get { return searchLot; }
            set { searchLot = value; RaisePropertyChanged(nameof(SearchLot)); }
        }
        private string searchComposition;
        public string SearchComposition
        {
            get { return searchComposition; }
            set { searchComposition = value; RaisePropertyChanged(nameof(SearchComposition)); }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new RawMaterialSheetServiceClient())
            {
                RecordCount = service.GetRawMaterialSheetCount(SearchLot, SearchComposition);
            }
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            using (var service = new RawMaterialSheetServiceClient())
            {
                var orders = service.GetRawMaterialSheet(skip, take, SearchLot, SearchComposition);
                RawMaterialSheets.Clear();
                orders.ToList().ForEach(o => RawMaterialSheets.Add(o));
            }
        }
        #region Commands
        public ObservableCollection<DcRawMaterialSheet> RawMaterialSheets { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRawMaterialSheet> Edit { get; set; }
        public RelayCommand<DcRawMaterialSheet> Duplicate { get; set; }
        public RelayCommand<DcRawMaterialSheet> Select { get; set; }

        #endregion

    }
}
