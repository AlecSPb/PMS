using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;

namespace PMSClient.ViewModel
{
    public class OutSourceEditVM : BaseViewModelEdit
    {
        public OutSourceEditVM()
        {
            States = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderState>(States);

            OutSourceTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OutSourceType>(OutSourceTypes);

            Suppliers = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OutSourceSupplier>(Suppliers);

            PaidStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OutSourcePaidState>(PaidStates);

            InitializeCommands();
        }


        private void InitializeCommands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
        }

        public void SetNew()
        {
            IsNew = true;
            var model = new DcOutSource();
            #region 初始化
            IsNew = true;
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            model.State = PMSCommon.OrderState.未完成.ToString();
            model.OrderType = PMSCommon.OrderProductType.靶材.ToString();
            model.Dimension = "";
            model.QuantityUnit = "片";
            model.Quantity = 1;
            model.Supplier = "";
            model.OrderLot = $"WG{DateTime.Now.ToString("yyMMdd")}";
            model.OrderName = "";
            model.Cost = 0;
            model.Remark = "";
            model.PaidState = PMSCommon.OutSourcePaidState.未付款.ToString();
            model.FinishTime = DateTime.Now.AddDays(30);

            #endregion
            CurrentOutSource = model;
        }
        public void SetDuplicate(DcOutSource model)
        {
            if (model != null)
            {
                IsNew = true;
                CurrentOutSource = model;
                CurrentOutSource.ID = Guid.NewGuid();
                CurrentOutSource.CreateTime = DateTime.Now;
                CurrentOutSource.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                CurrentOutSource.State = PMSCommon.OrderState.未完成.ToString();
                CurrentOutSource.OrderType = model.OrderType;
                CurrentOutSource.OrderName = model.OrderName;
                CurrentOutSource.OrderLot = model.OrderLot;
                CurrentOutSource.Supplier = model.Supplier;
                CurrentOutSource.Dimension = model.Dimension;
                CurrentOutSource.Quantity = model.Quantity;
                CurrentOutSource.QuantityUnit = model.QuantityUnit;
                CurrentOutSource.Cost = model.Cost;
                CurrentOutSource.Remark = model.Remark;
                CurrentOutSource.FinishTime = CurrentOutSource.CreateTime.AddDays(30);
                CurrentOutSource.PaidState = model.PaidState;
            }
        }
        public void SetEdit(DcOutSource model)
        {
            if (model != null)
            {
                IsNew = false;
                CurrentOutSource = model;
            }
        }

        private  void GoBack()
        {
            NavigationService.GoTo(PMSViews.OutSource);
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }
            if (CurrentOutSource.State == "作废")
            {
                if (!PMSDialogService.ShowYesNo("请问", "确定要作废吗？"))
                {
                    return;
                }
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new OutSourceServiceClient();
                if (IsNew)
                {
                    service.AddOutSource(CurrentOutSource, uid);
                }
                else
                {
                    if (CurrentOutSource.State==PMSCommon.OrderState.完成.ToString())
                    {
                        CurrentOutSource.FinishTime = DateTime.Now;
                    }
                    service.UpdateOutSource(CurrentOutSource, uid);
                }
                service.Close();
                PMSHelper.ViewModels.OutSource.RefreshData();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public List<string> OutSourceTypes { get; set; }
        public List<string> PaidStates { get; set; }
        public List<string> States { get; set; }
        public List<string> Suppliers { get; set; }

        private DcOutSource currentOutSource;
        public DcOutSource CurrentOutSource
        {
            get { return currentOutSource; }
            set
            {
                currentOutSource = value;
                RaisePropertyChanged(nameof(CurrentOutSource));
            }
        }

    }
}
