using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class RecordMachineVM : BaseViewModelPage
    {
        public RecordMachineVM()
        {
            searchVHPPlanLot = "";
            RecordMachines = new ObservableCollection<DcRecordMachine>();
            PageChanged = new RelayCommand(ActionPaging);
            Add = new RelayCommand(ActionAdd, CanAdd);
            Edit = new RelayCommand<DcRecordMachine>(ActionEdit, CanEdit);
            Search = new RelayCommand(ActionSearch);
            All = new RelayCommand(ActionAll);
            Duplicate = new RelayCommand<DcRecordMachine>(ActionDuplicate, CanDuplicate);
            Label = new RelayCommand<DcRecordMachine>(ActionLabel);
            QuickAdd = new RelayCommand(ActionQuickAdd,CanQuickAdd);

            TempRecordSheet = new RelayCommand(ActionTempRecordSheet);
            Fail = new RelayCommand<DcRecordMachine>(ActionFail, CanEdit);
            SetPageParametersWhenConditionChange();
        }

        private void ActionFail(DcRecordMachine obj)
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定要添加此记录到报废记录中吗？"))
            {
                return;
            }
            if (obj != null)
            {
                try
                {
                    using (var service = new FailureService.FailureServiceClient())
                    {
                        int check_exist_count = service.GetFailuresCountByProductID(obj.VHPPlanLot);

                        if (check_exist_count == 0)
                        {
                            var model = VMHelper.FailureVMHelper.GetNewFailure();
                            model.ProductID = obj.VHPPlanLot;
                            model.Stage = "加工";
                            model.Composition = obj.Composition;
                            model.Details = obj.PMINumber;
                            model.Problem = "加工不成功";
                            model.Remark = obj.Defects;
                            model.Process = "无";

                            service.AddFailure(model);
                            PMSDialogService.Show("添加成功");

                        }
                        else
                        {
                            PMSDialogService.ShowWarning("报废库已存在");
                        }
                    }
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }

        public void SetSearch(string vhpnumber)
        {
            SearchVHPPlanLot = vhpnumber;
            SetPageParametersWhenConditionChange();
        }


        private bool CanQuickAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMachine);
        }

        private void ActionQuickAdd()
        {
            PMSHelper.ViewModels.PlanSelect.SetRequestView(PMSViews.RecordMachine);
            PMSHelper.ViewModels.PlanSelect.RefreshData();
            NavigationService.GoTo(PMSViews.PlanSelect);
        }

        private void ActionLabel(DcRecordMachine model)
        {
            if (model != null)
            {

                var sb = new StringBuilder();
                sb.AppendLine(model.Composition);
                sb.AppendLine(model.Dimension);
                sb.AppendLine("加工");
                sb.AppendLine(model.VHPPlanLot);

                var mainContent = sb.ToString();

                var pageTitle = "热压毛坯标签打印输出";
                var tips = @"点击打开模板按钮，粘贴不同内容到模板合适位置，热压编号是自动生成的，可能不正确，请再自行修改，然后打印标签";
                var template = "毛坯标签";
                var helpimage = "productionlabel.png";
                PMSHelper.ToolViewModels.LabelOutPut.SetAllParameters(PMSViews.RecordMachine, pageTitle,
                    tips, template, mainContent, helpimage);
                NavigationService.GoTo(PMSViews.LabelOutPut);
            }
        }

        private void ActionTempRecordSheet()
        {
            try
            {
                var filePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "Doc", "加工临时记录单.docx");
                var tempPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Resource", "DocTemplate", "Doc", "加工临时记录单_temp.docx");
                if (System.IO.File.Exists(filePath))
                {
                    if (System.IO.File.Exists(tempPath))
                    {
                        System.IO.File.Delete(tempPath);
                    }
                    System.IO.File.Copy(filePath, tempPath);
                    System.Diagnostics.Process.Start(tempPath);
                }

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private bool CanDuplicate(DcRecordMachine arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMachine);
        }

        private bool CanEdit(DcRecordMachine arg)
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMachine);
        }

        private bool CanAdd()
        {
            return PMSHelper.CurrentSession.IsAuthorized(PMSAccess.EditRecordMachine);
        }

        public void RefreshData()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionDuplicate(DcRecordMachine model)
        {
            if (PMSDialogService.ShowYesNo("请问", "确定复用这条记录？"))
            {
                PMSHelper.ViewModels.RecordMachineEdit.SetByDuplicate(model);
                GoToEditView();
            }

        }

        private void ActionAll()
        {
            SearchVHPPlanLot = "";
            SetPageParametersWhenConditionChange();
        }

        private void ActionSearch()
        {
            SetPageParametersWhenConditionChange();
        }

        private void ActionEdit(DcRecordMachine model)
        {
            PMSHelper.ViewModels.RecordMachineEdit.SetEdit(model);
            GoToEditView();
        }

        private static void GoToEditView()
        {
            NavigationService.GoTo(PMSViews.RecordMachineEdit);
        }

        private void ActionAdd()
        {
            PMSHelper.ViewModels.RecordMachineEdit.SetNew();
            GoToEditView();
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 30;
            var service = new RecordMachineServiceClient();
            RecordCount = service.GetRecordMachineCountByVHPPlanLot(SearchVHPPlanLot);
            service.Close();
            ActionPaging();
        }
        private void ActionPaging()
        {
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var service = new RecordMachineServiceClient();
            var models = service.GetRecordMachinesByVHPPlanLot(skip, take, SearchVHPPlanLot);
            service.Close();
            RecordMachines.Clear();
            models.ToList().ForEach(o => RecordMachines.Add(o));
        }


        private string searchVHPPlanLot;
        public string SearchVHPPlanLot
        {
            get { return searchVHPPlanLot; }
            set { searchVHPPlanLot = value; RaisePropertyChanged(nameof(SearchVHPPlanLot)); }
        }

        public ObservableCollection<DcRecordMachine> RecordMachines { get; set; }

        public RelayCommand Add { get; set; }
        public RelayCommand<DcRecordMachine> Edit { get; set; }
        public RelayCommand<DcRecordMachine> Duplicate { get; set; }
        public RelayCommand<DcRecordMachine> Fail { get; set; }
        public RelayCommand TempRecordSheet { get; set; }

        public RelayCommand<DcRecordMachine> Label { get; set; }
        public RelayCommand QuickAdd { get; set; }
    }
}
