using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.ServiceReference;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class PlanVM:ViewModelBase
    {
        public PlanVM()
        {
            IntitializeCommands();
            IntitializeProperties();
            SetPageParametersWhenConditionChange();

            Messenger.Default.Register<string>(this, "PlanVHPRefresh", ActionRefresh);
        }
        public override void Cleanup()
        {
            Messenger.Default.Unregister(this);
            base.Cleanup();
        }

        private void ActionRefresh(string obj)
        {
            SetPageParametersWhenConditionChange();
        }

        private void IntitializeProperties()
        {
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();
        
        }

        private void IntitializeCommands()
        {
            Navigate = new RelayCommand(() => NavigationService.GoTo("NavigationView"));
            AddNewPlan = new RelayCommand(() => NavigationService.GoTo("OrderSelectView"));
            EditPlan = new RelayCommand<ServiceReference.DcPlanVHP>(ActionEditPlan);

            PageChanged = new RelayCommand(ActionPaging);
        }

        private void ActionEditPlan(DcPlanVHP obj)
        {
            if (obj!=null)
            {
                var nModel = new NavigationObject();
                nModel.ViewName = "PlanEditView";
                nModel.ModelObject = obj;
                NavigationService.GoToWithParameter(nModel);
            }
        }

        private void SetPageParametersWhenConditionChange()
        {
            PageIndex = 1;
            PageSize = 20;
            var service = new MissonWithPlanServiceClient();
            RecordCount = service.GetMissonWithPlanCount();
            ActionPaging();
        }
        /// <summary>
        /// 分页动作的时候读入数据
        /// </summary>
        private void ActionPaging()
        {
            var service = new MissonWithPlanServiceClient();
            int skip, take = 0;
            skip = (PageIndex - 1) * PageSize;
            take = PageSize;
            var orders = service.GetMissonWithPlan(skip, take);
            MissonWithPlans.Clear();
            orders.ToList<DcMissonWithPlan>().ForEach(o => MissonWithPlans.Add(o));
        }





        #region PagingProperties
        private int pageIndex;
        public int PageIndex
        {
            get { return pageIndex; }
            set
            {
                pageIndex = value;
                RaisePropertyChanged(nameof(PageIndex));
            }
        }

        private int pageSize;
        public int PageSize
        {
            get { return pageSize; }
            set
            {
                pageSize = value;
                RaisePropertyChanged(nameof(PageSize));
            }
        }

        private int recordCount;
        public int RecordCount
        {
            get { return recordCount; }
            set
            {
                recordCount = value;
                RaisePropertyChanged(nameof(RecordCount));
            }
        }
        public RelayCommand PageChanged { get; private set; }
        #endregion

        #region Commands
        public RelayCommand Navigate { get; set; }
        public RelayCommand AddNewPlan { get; set; }

        public RelayCommand<DcPlanVHP> EditPlan { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }
        #endregion

    }
}
