using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using PMSClient.ReportsHelper;
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
            searchCompositionStd = searchProductID = SearchPlateLot = "";
            SetPageParametersWhenConditionChange();
        }

        public void SetSearch(string vhpnumber)
        {
            SearchCompositionStd = "";
            SearchProductID = vhpnumber;
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
            Finish = new RelayCommand<DcRecordBonding>(ActionFinish, CanFinish);
            TempFinish = new RelayCommand<DcRecordBonding>(ActionTempFinish, CanTempFinish);

            RecordSheet = new RelayCommand(ActionRecordSheet);
            ScanAdd = new RelayCommand(ActionScanAdd, CanScanAdd);
            Output = new RelayCommand(ActionOutput);
            OneKeyTempFinish = new RelayCommand(ActionOneKeyTempFinish);
        }

        private void ActionOneKeyTempFinish()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要讲所有未完成设置为【未录完】"))
                return;

            try
            {
                using (var service = new RecordBondingServiceClient())
                {
                    int result = service.SetAllUnFinsihToTempFinish();
                    if (result> 0)
                    {
                        SetPageParametersWhenConditionChange();
                    }
                    else
                    {
                        PMSDialogService.ShowWarning("未完成任何操作");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        private bool CanTempFinish(DcRecordBonding arg)
        {
            if (arg == null)
            {
                return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding);
            }
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding) && RecordBondingStateTransfer2(arg);
        }

        private bool RecordBondingStateTransfer2(DcRecordBonding arg)
        {
            return arg.State == PMSCommon.BondingState.未完成.ToString();
        }

        private void ActionTempFinish(DcRecordBonding obj)
        {
            if (PMSDialogService.ShowYesNo("请问?", "确定绑定已经完成但暂时还未录完数据？") == false)
                return;
            try
            {
                if (obj == null) return;
                obj.State = PMSCommon.BondingState.未录完.ToString();
                using (var service = new RecordBondingServiceClient())
                {
                    service.UpdateRecordBongdingByUID(obj, PMSHelper.CurrentSession.CurrentUser.UserName);
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionOutput()
        {
            //生成绑定计划报表

            if (!PMSDialogService.ShowYesNo("请问", "确定要导出全部数据吗？"))
            {
                return;
            }
            try
            {
                var excel = new ExcelOutputHelper.ExcelOutputRecordBonding();
                excel.Intialize("绑定记录导出记录", "绑定记录");
                excel.Output();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        private bool CanScanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordBonding);
        }

        private void ActionScanAdd()
        {
            var tool = new DataProcess.ScanInput.ScanInput();
            tool.TxtNumber.Text = "绑定批次";

            var context = new DataProcess.ScanInput.ScanInputRecordBondingVM();
            tool.DataContext = context;
            tool.Show();
        }

        private void ActionRecordSheet()
        {
            if (!PMSDialogService.ShowYesNo("警告", "确定要生成[未完成]的绑定计划的记录单吗？\r\n 不包含[未录完]"))
                return;
            try
            {
                using (var service = new RecordBondingServiceClient())
                {
                    //int count = service.GetUnFinishedRecordBondings().Count();
                    //PMSDialogService.ShowYes($"共有{count}条绑定记录");
                    var report = new WordBondingSheet();
                    var models = service.GetUnFinishedRecordBondings();
                    if (models.Count() > 0)
                    {
                        report.SetModel(models.ToList());
                        report.Output();
                    }
                    else
                    {
                        PMSDialogService.Show("暂时没有绑定计划");
                    }

                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
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
            return arg.State == PMSCommon.BondingState.未完成.ToString() ||
                arg.State == PMSCommon.BondingState.未录完.ToString();
        }

        private void ActionFinish(DcRecordBonding model)
        {
            CustomControls.BondingConclusion dialog = new CustomControls.BondingConclusion();
            dialog.ProductID = model.TargetProductID;
            if (dialog.ShowDialog() == true)
            {
                using (var service = new RecordBondingServiceClient())
                {
                    model.State = dialog.State;
                    model.PlateLot = dialog.PlateNumber;
                    model.Remark = dialog.Defects;
                    model.CoverPlateNumber = dialog.CoverPlateNumber;

                    double welding_rate = 0;
                    double.TryParse(dialog.WeldingRate, out welding_rate);
                    model.WeldingRate = welding_rate;

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
            PMSHelper.ViewModels.RecordTestSelect.SetRequestView(PMSViews.RecordBonding);
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
                NavigationService.GoTo(PMSViews.RecordBondingSimpleEdit);
            }
        }

        private void ActionDetail(DcRecordBonding obj)
        {
            throw new NotImplementedException();
        }

        private void ActionAll()
        {
            SearchProductID = SearchCompositionStd = SearchPlateLot = "";
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
            NavigationService.GoTo(PMSViews.RecordBondingSimpleEdit);
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            using (var service = new RecordBondingServiceClient())
            {
                RecordCount = service.GetRecordBondingCountNew(SearchProductID, SearchCompositionStd, SearchPlateLot);
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
                var orders = service.GetRecordBondingsNew(skip, take,
                    SearchProductID, SearchCompositionStd, SearchPlateLot);
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
        private string searchPlateLot;
        public string SearchPlateLot
        {
            get { return searchPlateLot; }
            set { searchPlateLot = value; RaisePropertyChanged(nameof(SearchPlateLot)); }
        }

        public RelayCommand Add { get; set; }
        public RelayCommand QuickAdd { get; set; }
        public RelayCommand<DcRecordBonding> Detail { get; set; }
        public RelayCommand<DcRecordBonding> Edit { get; set; }
        public RelayCommand<DcRecordBonding> Finish { get; set; }
        public RelayCommand<DcRecordBonding> TempFinish { get; set; }

        public RelayCommand RecordSheet { get; set; }

        public RelayCommand ScanAdd { get; set; }

        public RelayCommand Output { get; set; }
        public RelayCommand OneKeyTempFinish { get; set; }
    }
}
