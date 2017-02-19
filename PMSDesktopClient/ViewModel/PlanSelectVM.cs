using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDesktopClient.PMSMainService;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace PMSDesktopClient.ViewModel
{
    public class PlanSelectVM : ViewModelBase
    {
        private DcRecordTestResult testresult;
        public PlanSelectVM()
        {
            testresult = new PMSMainService.DcRecordTestResult();
            testresult.ID = Guid.NewGuid();
            testresult.CreateTime = DateTime.Now;
            testresult.Creator = (App.Current as App).CurrentUser.UserName;
            testresult.TestType = PMSCommon.TestType.Product.ToString();
            testresult.State = "Checked";
            testresult.Weight = "0";
            testresult.Remark = "";
            testresult.Resistance = "";
            testresult.Sample = "";
            testresult.CompositionXRF = "";
            testresult.Density = "0";


            IntitializeCommands();
            IntitializeProperties();
            SetPageParametersWhenConditionChange();
        }


        private void ActionSelect(DcMissonWithPlan obj)
        {
            testresult.Composition = obj.CompositionStandard;
            testresult.CompositionAbbr = obj.CompositoinAbbr;
            testresult.Customer = obj.CustomerName;
            testresult.Dimension = obj.Dimension;
            testresult.ProductID = obj.PlanDate.ToString("yyMMdd") + "-" + obj.VHPDeviceCode + "-" + 1;
            testresult.PO = obj.PO;
            testresult.DimensionActual = testresult.Dimension;

            MsgObject msg = new MsgObject();
            msg.MsgToken = VT.RecordTestResultEdit;
            msg.MsgModel = new ModelObject() { IsNew = true, Model = testresult };
            NavigationService.GoTo(msg);
        }

        private void IntitializeProperties()
        {
            MissonWithPlans = new ObservableCollection<DcMissonWithPlan>();

        }

        private void IntitializeCommands()
        {
            GiveUp = new RelayCommand(() => NavigationService.GoTo(VT.RecordTestResult.ToString()));
            PageChanged = new RelayCommand(ActionPaging);
            Select = new RelayCommand<PMSMainService.DcMissonWithPlan>(ActionSelect);
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
        public RelayCommand GiveUp { get; set; }
        public RelayCommand<DcMissonWithPlan> Select { get; set; }
        #endregion

        #region Properties
        public ObservableCollection<DcMissonWithPlan> MissonWithPlans { get; set; }
        #endregion

    }
}
