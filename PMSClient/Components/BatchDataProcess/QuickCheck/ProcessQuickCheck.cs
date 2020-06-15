using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.Components.CscanImageProcess;
using PMSClient.DataProcess.ScanInput;
using PMSClient.MainService;
using PMSClient.ReportsHelper;
using PMSClient.ReportsHelperNew;

namespace PMSClient.DataProcess.QuickCheck
{
    public class ProcessQuickCheck : ProcessBase
    {
        public ProcessQuickCheck()
        {

        }
        private DcDelivery delivery;
        public void SetDelivery(DcDelivery delivery)
        {
            this.delivery = delivery;
        }

        public override void Check(Action<double> DoSomething)
        {
            try
            {
                ReSet();
                double progressValue = 0;
                double count = 0;
                foreach (var item in Lots)
                {
                    //默认有效，多次否决
                    CheckIsStandard(item);
                    CheckInDeliverySheetItems(item, delivery.ID);

                    count++;
                    progressValue = count * 100 / Lots.Count;
                    if (DoSomething != null)
                    {
                        DoSomething(progressValue);
                        //System.Threading.Thread.Sleep(50);
                    }
                    item.HasProcessed = true;
                }
                //弹出总数核验
                CheckTotalCount();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public override void Process(Action<double> DoSomething)
        {
            try
            {
                ReSet();
                double progressValue = 0;
                double count = 0;
                foreach (var item in Lots)
                {
                    //检查每一项
                    CheckIsStandard(item);
                    CheckInDeliverySheetItems(item, delivery.ID);

                    count++;
                    progressValue = count * 100 / Lots.Count;
                    if (DoSomething != null)
                    {
                        DoSomething(progressValue);
                        //System.Threading.Thread.Sleep(50);
                    }
                    item.HasProcessed = true;
                }

                //弹出总数核验
                CheckTotalCount();
                //保存核验结果到Delivery
                StringBuilder sb = new StringBuilder();
                Lots.ForEach(i =>
                {
                    sb.Append($"{i.Lot};");
                });

                using (var service = new DeliveryServiceClient())
                {
                    delivery.LastCheckIDCollection = sb.ToString();
                    delivery.LastUpdateTime = DateTime.Now;
                    service.UpdateDeliveryByUID(delivery, PMSHelper.CurrentSession.CurrentUser.UserName);
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

        private void CheckTotalCount()
        {
            try
            {
                using (var service = new DeliveryServiceClient())
                {
                    int count_shouldbe = service.GetDeliveryItemByDeliveryID(delivery.ID).Count();
                    int count_real = Lots.Count;
                    if (count_shouldbe != count_real)
                    {
                        PMSDialogService.ShowWarning($"靶材清单数目{count_shouldbe}和实际扫描数目{count_real}不一样");
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);

            }
        }

        private void CheckInDeliverySheetItems(LotModel item, Guid deliveryid)
        {
            if (item.IsValid)
            {
                using (var service = new DeliveryServiceClient())
                {

                    if (service.CheckDeliveryItemExistByProductID(deliveryid, item.Lot))
                    {
                        item.IsValid = true;
                        item.AppendMessage("OK");
                    }
                    else
                    {
                        item.IsValid = false;
                        item.AppendMessage("靶材记录中不存在");
                    }
                }
            }
        }
    }
}
