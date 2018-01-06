using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public class RecordBondingVM : BaseViewModelPage
    {
        public RecordBondingVM()
        {
            RecordBondings = new ObservableCollection<DcRecordBonding>();

            InitializeCommands();
            searchCompositionStd = searchProductID = "";
            SetPageParametersWhenConditionChange();
        }

        public void Refresh()
        {
            SetPageParametersWhenConditionChange();
        }
        private void InitializeCommands()
        {
            Add = new RelayCommand(ActionAdd, CanAdd);
            QuickAdd = new RelayCommand(ActionQuickAdd, CanQuickAdd);
            Detail = new RelayCommand<DcRecordBonding>(ActionDetail);
            Edit = new RelayCommand<DcRecordBonding>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            PageChanged = new RelayCommand(ActionPaging);
            Finish = new RelayCommand<MainService.DcRecordBonding>(ActionFinish, CanFinish);
        }

        private bool CanFinish(DcRecordBonding arg)
        {
            if (arg == null)
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding);
            }
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding) && RecordBondingStateTransfer(arg);
        }

        private bool RecordBondingStateTransfer(DcRecordBonding arg)
        {
            return arg.State == PMSCommon.BondingState.未完成.ToString();
        }

        private void ActionFinish(DcRecordBonding model)
        {
            CustomControls.BondingConclusion dialog = new CustomControls.BondingConclusion();
            if (dialog.ShowDialog() == true)
            {
                using (var service = new RecordBondingServiceClient())
                {
                    model.State =dialog.State;
                    model.PlateLot = dialog.PlateNumber;
                    model.Remark = dialog.Defects;
                    service.UpdateRecordBongdingByUID(model, PMSHelper.CurrentSession.CurrentUser.UserName);
                }
                SetPageParametersWhenConditionChange();
            }


            //if (PMSDialogService.ShowYesNo("请问", "确定这个绑定完成了吗？"))
            //{
            //    using (var service = new RecordBondingServiceClient())
            //    {
            //        model.State = PMSCommon.BondingState.完成.ToString();
            //        service.UpdateRecordBongdingByUID(model, PMSHelper.CurrentSession.CurrentUser.UserName);
            //    }
            //    SetPageParametersWhenConditionChange();
            //}
        }

        private bool CanQuickAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding);
        }

        private void ActionQuickAdd()
        {
            PMSHelper.ViewModels.RecordTestSelect.SetRequestView(PMSViews.RecordBondingEdit);
            PMSHelper.ViewModels.RecordTestSelect.RefreshData();
            PMSBatchHelper.SetRecordTestBatchEnable(true);
            NavigationService.GoTo(PMSViews.RecordTestSelect);
        }

        private bool CanEdit(DcRecordBonding arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding);
        }

        private void ActionEdit(DcRecordBonding model)
        {
            if (model != null)
            {
                PMSHelper.ViewModels.RecordBondingEdit.SetEdit(model);
                NavigationService.GoTo(PMSViews.RecordBondingEdit);
            }
        }

        private void ActionDetail(DcRecordBonding obj)
        {
            throw new NotImplementedException();
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordBondingEdit.SetNew();
            NavigationService.GoTo(PMSViews.RecordBondingEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            using (var service = new RecordBondingServiceClient())
            {
                RecordCount = service.GetRecordBondingCount(SearchProductID, SearchCompositionStd);
            }

            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;

            using (var service = new RecordBondingServiceClient())
            {
                var orders = service.GetRecordBondings(skip, take, SearchProductID, SearchCompositionStd);
                RecordBondings.Clear();
                orders.ToList().ForEach(o => RecordBondings.Add(o));
            }
        }


        public ObservableCollection<DcRecordBonding> RecordBondings { get; set; }

        private string searchCompositionStd;
        public string SearchCompositionStd
        {
            get { return searchCompositionStd; }
            set { searchCompositionStd = value; RaisePropertyChanged(nameof(SearchCompositionStd)); }
        }

        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set { searchProductID = value; RaisePropertyChanged(nameof(SearchProductID)); }
        }


        public RelayCommand Add { get; set; }
        public RelayCommand QuickAdd { get; set; }
        public RelayCommand<DcRecordBonding> Detail { get; set; }
        public RelayCommand<DcRecordBonding> Edit { get; set; }
        public RelayCommand<DcRecordBonding> Finish { get; set; }
    }
}
