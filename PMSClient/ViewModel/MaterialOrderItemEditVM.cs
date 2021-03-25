using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using PMSClient.MainService;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight.Messaging;
using PMSClient.Components.MaterialOrder;

namespace PMSClient.ViewModel
{
    public class MaterialOrderItemEditVM : BaseViewModelEdit
    {
        public MaterialOrderItemEditVM()
        {
            checkResult = "";
            OrderStates = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaterialOrderItemState>(OrderStates);
            Priorities = new List<string>();
            PMSBasicDataService.SetListDS<PMSCommon.MaterialOrderItemPriority>(Priorities);

            InitialCommmands();
        }


        private void CheckProvideMaterialItem()
        {
            if (currentMaterialOrderID == null) return;
            try
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    //获取所有的项目
                    var orderItems = service.GetMaterialOrderItembyMaterialID(currentMaterialOrderID);

                    List<SimpleMaterialCheckResult> collections = new List<SimpleMaterialCheckResult>();
                    foreach (var item in orderItems)
                    {
                        var result = new SimpleMaterialCheckResult();
                        result.ItemCode = $"{item.OrderItemNumber} 提供原料 ";

                        var material = SimpleMaterialHelper.StrToSimpleMaterial(item.ProvideRawMaterial);
                        if (material != null && material.Count != 0)
                        {
                            foreach (var ii in material)
                            {
                                if (ii.UnitPrice <= 0)
                                {
                                    result.CheckResult = $"{ii.ElementName}单价为0?";
                                }
                                if (ii.Weight <= 0)
                                {
                                    result.CheckResult += $" {ii.ElementName}重量为0?";
                                }
                            }
                        }
                        else
                        {
                            result.CheckResult = "无需提供原料?";
                        }
                        collections.Add(result);
                    }


                    StringBuilder sb = new StringBuilder();

                    foreach (var item in collections)
                    {
                        if (!string.IsNullOrEmpty(item.CheckResult))
                        {
                            sb.Append(item.ItemCode);
                            sb.Append("=");
                            sb.AppendLine(item.CheckResult);
                        }
                    }

                    if (!string.IsNullOrEmpty(sb.ToString()))
                    {
                        var dialog = new ToolWindow.PlainTextWindow();
                        dialog.ContentText = sb.ToString();
                        dialog.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }
        }


        /// <summary>
        /// 更新主订单的Remark
        /// </summary>
        private void RefreshMaterialOrderRemark()
        {
            if (currentMaterialOrderID == null) return;
            try
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    //获取所有的项目
                    var orderItems = service.GetMaterialOrderItembyMaterialID(currentMaterialOrderID);

                    List<SimpleMaterial> collections = new List<SimpleMaterial>();
                    foreach (var item in orderItems)
                    {
                        var material = SimpleMaterialHelper.StrToSimpleMaterial(item.ProvideRawMaterial);
                        if (material != null && material.Count != 0)
                        {
                            collections.AddRange(material);
                        }
                    }

                    var query = from i in collections
                                group i by i.ElementName into g
                                select new { Key = g.Key, Weight = g.Sum(i => i.Weight) };

                    StringBuilder sb = new StringBuilder();
                    foreach (var item in query)
                    {
                        sb.Append(item.Key);
                        sb.Append("+");
                        sb.Append(item.Weight);
                        sb.Append(";");
                    }

                    var currentMaterialOrder = service.GetMaterialOrderByID(currentMaterialOrderID);
                    if (currentMaterialOrder != null)
                    {
                        currentMaterialOrder.Remark = sb.ToString();
                        service.UpdateMaterialOrderByUID(currentMaterialOrder, PMSHelper.CurrentSession.CurrentUser.UserName);
                        XSHelper.XS.MessageBox.ShowInfo("主订单备注原材料信息已经更新,请点击[全部]刷新");
                    }
                }
            }
            catch (Exception ex)
            {
                XSHelper.XS.MessageBox.ShowError(ex.Message);
            }
        }

        /// <summary>
        /// 计算原材料价格
        /// </summary>
        private void CalculateMaterialPrice()
        {
            if (CurrentMaterialOrderItem != null)
            {
                double cost = SimpleMaterialHelper.GetAllMaterialPrice(CurrentMaterialOrderItem.ProvideRawMaterial);
                CurrentMaterialOrderItem.MaterialPrice = cost;
            }
        }

        /// <summary>
        /// 保存对应材料订单ID
        /// </summary>
        private Guid currentMaterialOrderID;

        public DcMaterialOrder CurrentMaterialOrder { get; set; }
        public void SetNew(DcMaterialOrder order)
        {
            if (order != null)
            {
                CurrentMaterialOrder = order;
                currentMaterialOrderID = order.ID;
                IsNew = true;

                var item = new DcMaterialOrderItem();
                #region 初始化
                item.ID = Guid.NewGuid();
                item.MaterialOrderID = order.ID;
                item.State = PMSCommon.MaterialOrderItemState.未完成.ToString();
                item.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                item.CreateTime = DateTime.Now;
                var prefix = order.OrderPO;
                item.OrderItemNumber = prefix + (GetNowItemCount(order) + 1).ToString();
                item.Composition = "需求成分";
                item.PMINumber = Helpers.DefaultHelper.DefaultPMINumber();
                item.Purity = "5N";
                item.Description = "";
                item.ProvideRawMaterial = "A+0+0;B+0+0;";
                item.UnitPrice = 0;
                item.Weight = 0;
                item.DeliveryDate = DateTime.Now.AddDays(7);
                item.Priority = PMSCommon.MaterialOrderItemPriority.普通.ToString();
                item.SJIngredient = "无";
                item.HowManyTargets = "1pcs";
                item.Remark = "";
                #endregion

                CurrentMaterialOrderItem = item;
                CheckResult = "";
            }

        }
        private int GetNowItemCount(DcMaterialOrder order)
        {
            if (order != null)
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    return service.GetMaterialOrderItemCountByMaterialID(order.ID);
                }
            }
            return 1;
        }
        public void SetEdit(DcMaterialOrderItem item)
        {
            if (item != null)
            {
                IsNew = false;
                CurrentMaterialOrderItem = item;
                CheckResult = "";

                currentMaterialOrderID = CurrentMaterialOrderItem.MaterialOrderID;
            }
        }

        public void SetBySelect(DcMaterialNeed need)
        {
            if (need != null)
            {
                CurrentMaterialOrderItem.Composition = need.Composition;
                CurrentMaterialOrderItem.PMINumber = need.PMINumber;
                CurrentMaterialOrderItem.Weight = need.Weight;
                CurrentMaterialOrderItem.HowManyTargets = need.HowManyTargets;
                CurrentMaterialOrderItem.Remark = need.SpecialNeeds;
                //RaisePropertyChanged(nameof(CurrentMaterialOrderItem));
            }
        }


        private void InitialCommmands()
        {
            GiveUp = new RelayCommand(GoBack);
            Save = new RelayCommand(ActionSave);
            Select = new RelayCommand(ActionSelect);
            Calculator = new RelayCommand(ActionCalculator);
            CheckOrderItemNumberExist = new RelayCommand(ActionCheck);
        }

        private void ActionCheck()
        {
            try
            {
                using (var service = new MaterialOrderServiceClient())
                {
                    if (CurrentMaterialOrderItem != null)
                    {
                        var result = service.CheckOrderItemNumberExist(CurrentMaterialOrderItem.OrderItemNumber);
                        CheckResult = result ? "被占用" : "可以用";
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void ActionCalculator()
        {
            PMSHelper.ToolViewModels.MaterialNeedCalcualtion.SetRequestView(PMSViews.MaterialOrderItemEdit);
            NavigationService.GoTo(PMSViews.MaterialNeedCalcuationTool);
        }
        public void SetByCalculate(double weight)
        {
            //克到千克的转换
            CurrentMaterialOrderItem.Weight = weight / 1000;
            //RaisePropertyChanged(nameof(CurrentMaterialNeed));
        }
        private void ActionSelect()
        {
            PMSHelper.ViewModels.MaterialNeedSelect.SetRequestView(PMSViews.MaterialOrderItemEdit);
            PMSBatchHelper.SetMaterialNeedBatchEnable(IsNew);
            PMSHelper.ViewModels.MaterialNeedSelect.RefreshData();
            NavigationService.GoTo(PMSViews.MaterialNeedSelect);
        }

        private void ActionSave()
        {
            if (CurrentMaterialOrderItem.Composition.Contains("Si"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Si],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("O"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[O],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("B"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[B],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("C"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[C],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("F"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[F],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("Na"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Na],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("K"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[K],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("Cs"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Cs],确定使用这个成分？"))
                {
                    return;
                };
            }
            if (CurrentMaterialOrderItem.Composition.Contains("Cl"))
            {
                if (!PMSDialogService.ShowYesNo("请注意", "请注意成分中含有[Cl],确定使用这个成分？"))
                {
                    return;
                };
            }


            var check_result = VMHelper.MaterialOrderVMHelper.CheckTheComposition(CurrentMaterialOrderItem.Composition);
            if (!check_result.IsOK)
            {
                if (!PMSDialogService.ShowYesNo("请注意", check_result.Message))
                {
                    return;
                };
            }

            if (!PMSDialogService.ShowYesNo("请问", "确定保存这条记录？"))
            {
                return;
            }

            bool isCheck = XSHelper.XS.MessageBox.ShowYesNo("要自动进行\r\n[提供单质总价值计算-仅本项]\r\n[检查提供单质价格和重量-所有项]\r\n[汇总所有提供材料到备注]\r\n吗?");
            try
            {
                string uid = PMSHelper.CurrentSession.CurrentUser.UserName;
                var service = new MaterialOrderServiceClient();
                if (isCheck)
                {
                    //计算材料价值
                    CalculateMaterialPrice();
                }

                if (IsNew)
                {
                    CurrentMaterialOrderItem.Creator = PMSHelper.CurrentSession.CurrentUser.UserName;
                    service.AddMaterialOrderItemByUID(CurrentMaterialOrderItem, uid);
                }
                else
                {
                    service.UpdateMaterialOrderItemByUID(CurrentMaterialOrderItem, uid);
                }
                service.Close();

                if (isCheck)
                {
                    //自动检查原料项目
                    CheckProvideMaterialItem();
                    //自动汇总原材料和汇总
                    RefreshMaterialOrderRemark();
                }
                PMSHelper.ViewModels.MaterialOrder.RefreshDataItem();
                GoBack();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private static void GoBack()
        {
            NavigationService.GoTo(PMSViews.MaterialOrder);
        }

        public List<string> OrderStates { get; set; }
        public List<string> Priorities { get; set; }


        private DcMaterialOrderItem currentMaterialOrderItem;
        public DcMaterialOrderItem CurrentMaterialOrderItem
        {
            get { return currentMaterialOrderItem; }
            set
            {
                Set(ref currentMaterialOrderItem, value);
            }
        }

        private string checkResult;

        public string CheckResult
        {
            get { return checkResult; }
            set { checkResult = value; RaisePropertyChanged(nameof(CheckResult)); }
        }



        public RelayCommand Select { get; set; }

        public RelayCommand Calculator { get; set; }


        public RelayCommand CheckOrderItemNumberExist { get; set; }

    }
}
