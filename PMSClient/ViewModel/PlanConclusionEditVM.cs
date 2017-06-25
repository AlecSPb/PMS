using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.BasicService;
using PMSClient.MainService;
using System.Collections.ObjectModel;

namespace PMSClient.ViewModel
{
    public class PlanConclusionEditVM : BaseViewModelEdit
    {
        public PlanConclusionEditVM()
        {
            Grades = new List<int>();
            Grades.Clear();
            Grades.Add(100);
            Grades.Add(80);
            Grades.Add(60);
            Grades.Add(30);

            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetEdit(DcPlanVHP plan)
        {
            if (plan != null)
            {
                IsNew = false;
                CurrentPlan = plan;
            }
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentPlan.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定作废这条记录？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new PlanVHPServiceClient();
                if (IsNew)
                {
                    service.AddVHPPlanByUID(CurrentPlan, uid);
                }
                else
                {
                    service.UpdateVHPPlanByUID(CurrentPlan, uid);
                }
                service.Close();
                PMSHelper.ViewModels.Misson.RefreshData();
                NavigationService.Status("保存成功，请刷新列表");
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.PlanConclusion);
        }
        public List<int> Grades { get; set; }

        private DcPlanVHP currentPlan;
        public DcPlanVHP CurrentPlan
        {
            get
            {
                return currentPlan;
            }
            set
            {
                Set<DcPlanVHP>(ref currentPlan, value);
            }
        }

    }
}
