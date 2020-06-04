using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.CheckLogic;
using PMSClient.NewService;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using PMSClient.Components.EOrder;
using XSHelper;

namespace PMSClient.ViewModel
{
    public class OrderEditVM : BaseViewModelEdit
    {
        public OrderEditVM()
        {
            InitializeProperties();
            InitializeCommands();
        }

        private void InitializeCommands()
        {
            Save = new RelayCommand(ActionSave, CanSave);
            GiveUp = new RelayCommand(GoBack);
            CheckPMINumber = new RelayCommand(ActionCheckPMINumber);
            Input = new RelayCommand(ActionInput);
        }

        private void ActionInput()
        {
            var result = XSHelper.XS.Dialog.ShowOpenDialog(XSHelper.XS.File.GetDesktopPath(), "json文件(*.json)|*.json");
            if (result.HasSelected)
            {
                string jsonFile = result.SelectPath;
                string jsonStr = XS.File.ReadText(jsonFile);
                var order = JsonConvert.DeserializeObject<Order>(jsonStr);
                #region 赋值
                CurrentOrder.ID = order.GUIDID;
                CurrentOrder.CreateTime = order.CreateTime;
                CurrentOrder.CustomerName = order.CustomerName;
                CurrentOrder.CompositionOriginal = order.Composition.Replace("-", "").Replace(" ", "");
                CurrentOrder.PO = order.PO;

                if (order.ProductType.Contains("Target"))
                {
                    CurrentOrder.ProductType = PMSCommon.ProductType.靶材.ToString();
                }
                else
                {
                    CurrentOrder.QuantityUnit = order.QuantityUnit;
                }

                CurrentOrder.Purity = order.Purity;
                CurrentOrder.Quantity = order.Quantity;
                if (order.ProductType.Contains("pcs"))
                {
                    CurrentOrder.QuantityUnit = PMSCommon.OrderUnit.片.ToString();
                }
                else
                {
                    CurrentOrder.QuantityUnit = order.QuantityUnit;
                }


                #endregion

                CurrentOrder.Dimension = order.Dimension;
                CurrentOrder.DimensionDetails = order.DimensionDetails;
                CurrentOrder.Drawing = order.Drawing;
                CurrentOrder.SampleNeed = order.SampleNeed;
                CurrentOrder.SampleNeedRemark = order.SampleNeedRemark;
                CurrentOrder.SampleForAnlysis = order.SampleForAnlysis;
                CurrentOrder.SampleForAnlysisRemark = order.SampleForAnlysisRemark;
                CurrentOrder.DeadLine = order.DeadLine;
                CurrentOrder.ShipTo = order.ShipTo;
                CurrentOrder.WithBackingPlate = order.WithBackingPlate;
                CurrentOrder.PlateDrawing = order.PlateDrawing;
                CurrentOrder.SpecialRequirement = order.SpecialRequirement;
                CurrentOrder.PartNumber = order.PartNumber;


                //产生窗口
                var win = new TextWindow();
                win.MainText.Text = TextService.GetOrderText(order);
                win.Show();

            }
        }

        private void ActionCheckPMINumber()
        {
            if (CurrentOrder != null)
            {
                using (var service = new NewServiceClient())
                {
                    CanUseThisPMINumber = service.CheckOrderPMINumberExist(CurrentOrder.PMINumber) ? "被占用" : "可以用";
                }
            }
        }

        public void InitializeProperties()
        {
            canUseThisPMINumber = "";

            ProductTypes = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderProductType>(ProductTypes);

            OrderUnits = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.OrderUnit>(OrderUnits);

            SampleNeeds = new List<string>();
            PMSBasicDataService.SetListDS(PMSCommon.CustomData.OrderSampleNeeds, SampleNeeds);

            CustomerNames = new List<string>();
            PMSBasicDataService.SetListDS(BasicData.Customers, CustomerNames, i => i.CustomerName);
        }

        public void SetNew()
        {
            IsNew = true;
            var order = new DcOrder();
            #region 初始化order
            order.ID = Guid.NewGuid();
            order.CustomerName = CustomerNames.FirstOrDefault();
            order.PO = DateTime.Now.ToString("yyMMdd");
            order.CompositionOriginal = "CuInGaSe";
            order.CompositionStandard = "";
            order.CompositionAbbr = "";
            order.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            order.CreateTime = DateTime.Now;
            order.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
            order.ProductType = PMSCommon.OrderProductType.靶材.ToString();
            order.Purity = "99.990%";
            order.Quantity = 1;
            order.QuantityUnit = PMSCommon.OrderUnit.片.ToString();
            order.Dimension = "230mm OD x  4mm";
            order.DimensionDetails = "无";
            order.SampleNeed = "无需样品";
            order.SampleNeedRemark = "无";
            order.MinimumAcceptDefect = "通常";
            order.DeadLine = DateTime.Now.AddDays(30);
            order.PolicyType = PMSCommon.OrderPolicyType.VHP.ToString();
            order.State = PMSCommon.OrderState.未核验.ToString();
            order.Priority = PMSCommon.OrderPriority.普通.ToString();
            order.Reviewer = "";
            order.ReviewTime = DateTime.Now;
            order.FinishTime = DateTime.Now;
            order.ProductionIndex = 0;
            order.MaterialIndex = 0;
            order.WithBackingPlate = "无";
            order.PlateDrawing = "无";//200327添加
            order.Drawing = "默认";
            order.SampleForAnlysis = "无需样品";
            order.SampleForAnlysisRemark = "无";
            order.ShipTo = "未定";
            order.SpecialRequirement = "无";
            order.OrderRemark = "";
            //2020-3-18 添加
            order.PartNumber = "无";
            order.SecondMachineDetails = "无";
            order.SecondMachineDimension = "无";
            #endregion
            CurrentOrder = order;
        }
        public void SetEdit(DcOrder order)
        {
            if (order != null)
            {
                IsNew = false;
                CurrentOrder = order;
            }
        }

        public void SetDuplicate(DcOrder order)
        {
            if (order != null)
            {
                IsNew = true;
                CurrentOrder = order;
                CurrentOrder.ID = Guid.NewGuid();
                CurrentOrder.State = PMSCommon.OrderState.未核验.ToString();
                CurrentOrder.CreateTime = DateTime.Now;
                CurrentOrder.FinishTime = DateTime.Now;
                CurrentOrder.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
            }
        }

        private bool CanSave()
        {
            return true;
        }

        private void ActionSave()
        {
            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }

            //check
            string remark = CurrentOrder.Remark;
            string requirement = CurrentOrder.DimensionDetails;
            CheckResult is_outsource = OrderCheckLogic.OutSourceCheck(new string[] { remark, requirement });

            if (!is_outsource.IsCheckOK)
            {
                PMSDialogService.ShowWarning(is_outsource.Message);
            }
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new NewServiceClient();
                if (IsNew)
                {
                    if (CurrentOrder != null)
                    {
                        //检查PMINumber是否符合格式规范
                        VMHelper.CommonVMHelper.CheckPMINumber(CurrentOrder.PMINumber);

                        //检查PMINumber是否被占用
                        if (service.CheckOrderPMINumberExist(CurrentOrder.PMINumber))
                        {
                            PMSDialogService.ShowWarning($"PMI Number【{CurrentOrder.PMINumber}】" +
                                $"已经被占用,无法保存");
                            return;
                        }


                    }
                    service.AddOrder(CurrentOrder, uid);
                }
                else
                {
                    if (CurrentOrder.State == PMSCommon.OrderState.最终完成.ToString())
                    {
                        CurrentOrder.FinishTime = DateTime.Now;
                    }

                    service.UpdateOrder(CurrentOrder, uid);
                }
                service.Close();
                //PMSHelper.ViewModels.Order.RefreshData();
                GoBack();
                NavigationService.Status("保存成功，请刷新列表");

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.Order);
        }

        private DcOrder currentOrder;
        public DcOrder CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged(nameof(CurrentOrder));
            }
        }
        private string canUseThisPMINumber;

        public string CanUseThisPMINumber
        {
            get { return canUseThisPMINumber; }
            set { canUseThisPMINumber = value; RaisePropertyChanged(nameof(CanUseThisPMINumber)); }
        }

        public RelayCommand CheckPMINumber { get; set; }
        public RelayCommand Input { get; set; }

        public List<string> CustomerNames { get; set; }
        public List<string> ProductTypes { get; set; }
        public List<string> OrderUnits { get; set; }
        public List<string> SampleNeeds { get; set; }
    }
}
