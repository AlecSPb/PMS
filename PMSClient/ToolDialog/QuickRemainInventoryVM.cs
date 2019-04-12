using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight;
using System.Collections.ObjectModel;
using PMSClient.MainService;
using PMSClient.ExtraService;

namespace PMSClient.ToolDialog
{
    public class QuickRemainInventoryVM : ViewModelBase
    {
        public QuickRemainInventoryVM()
        {
            searchProductID = "";
            statusMessage = "";
            sb = new StringBuilder();
            sb.Clear();
            PlanWithMissons = new ObservableCollection<DcPlanWithMisson>();
            Search = new RelayCommand(ActionSearch, CanSearch);
            Add = new RelayCommand<DcPlanWithMisson>(ActionAdd, CanAdd);
            ActionPaging();
        }

        private void ActionAdd(DcPlanWithMisson obj)
        {
            if (obj == null) return;

            if (PMSDialogService.ShowYesNo("请问", "确定要添加此条记录到储备库当中吗？"))
            {
                var model = new DcRemainInventory();
                model.ID = Guid.NewGuid();
                model.CreateTime = DateTime.Now;
                model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.State = PMSCommon.InventoryState.库存.ToString();

                model.ProductID = obj.Plan.SearchCode+"-1";
                model.Composition = obj.Misson.CompositionStandard;
                model.Details = obj.Misson.PMINumber;
                model.Dimension = obj.Misson.Dimension;

                try
                {
                    string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                    var service = new RemainInventoryServiceClient();
                    service.AddRemainInventory(model);

                    service.Close();
                    PMSHelper.ViewModels.RemainInventory.RefreshData();
                    AddToStatus($"{obj.Plan.SearchCode}-1={obj.Misson.CompositionStandard}添加完毕");
                }
                catch (Exception ex)
                {
                    PMSHelper.CurrentLog.Error(ex);
                }
            }
        }
        private StringBuilder sb;
        private void AddToStatus(string msg)
        {
            sb.Insert(0, msg+Environment.NewLine);
            StatusMessage = sb.ToString();
        }


        private bool CanAdd(DcPlanWithMisson arg)
        {
            return true;
        }

        private void ActionSearch()
        {
            ActionPaging();
        }

        private bool CanSearch()
        {
            return !(string.IsNullOrEmpty(SearchProductID));
        }


        private void ActionPaging()
        {
            int skip, take = 0;
            skip = 0;
            take = 10;
            //只显示Checked过的计划
            using (var service = new MissonServiceClient())
            {
                var models = service.GetPlanExtra(skip, take, SearchProductID, String.Empty);
                PlanWithMissons.Clear();
                models.ToList().ForEach(o => PlanWithMissons.Add(o));
            }
        }


        private string searchProductID;
        public string SearchProductID
        {
            get { return searchProductID; }
            set
            {
                if (searchProductID == value)
                    return;
                searchProductID = value;
                RaisePropertyChanged(() => SearchProductID);
            }
        }

        private string statusMessage;
        public string StatusMessage
        {
            get
            {
                return statusMessage;
            }
            set
            {
                if (statusMessage == value) return;
                statusMessage = value;
                RaisePropertyChanged(() => StatusMessage);
            }
        }


        public ObservableCollection<DcPlanWithMisson> PlanWithMissons { get; set; }
        #region 命令
        public RelayCommand Search { get; set; }
        public RelayCommand<DcPlanWithMisson> Add { get; set; }
        #endregion
    }
}
