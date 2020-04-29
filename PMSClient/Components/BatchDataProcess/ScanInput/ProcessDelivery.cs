using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.DataProcess.ScanInput
{
    public class ProcessDelivery : ProcessBase
    {
        public ProcessDelivery(DcDelivery model)
        {
            currentDelivery = model;
        }

        private DcDelivery currentDelivery;
        public int Number { get; set; } = 1;
        public string DeliveryType { get; set; } = PMSCommon.DeliveryType.最终发货.ToString();

        public bool CheckExist { get; set; } = true;

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
                    if (GetDSType(item.Lot) == TableSource.Plate)
                    {
                        CheckInPlate(item);
                    }
                    else
                    {
                        CheckInProduct(item);
                    }
                    //检查是否已存在
                    if (CheckExist)
                    {
                        CheckInDeliveryItem(item);
                    }

                    count++;
                    progressValue = count * 100 / Lots.Count;
                    if (DoSomething != null)
                    {
                        DoSomething(progressValue);
                        System.Threading.Thread.Sleep(50);
                    }
                }
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
                    //默认有效，多次否决
                    CheckIsStandard(item);
                    if (GetDSType(item.Lot) == TableSource.Plate)
                    {
                        CheckInPlate(item);
                    }
                    else
                    {
                        CheckInProduct(item);
                    }
                    //检查是否已存在
                    if (CheckExist)
                    {
                        CheckInDeliveryItem(item);
                    }

                    //有效继续
                    if (item.IsValid)
                    {
                        DcDeliveryItem model = null;

                        if (GetDSType(item.Lot) == TableSource.Plate)
                        {
                            //读取背板记录并转换
                            using (var ss = new PlateServiceClient())
                            {
                                var record = ss.GetPlateByPlateID(item.Lot).FirstOrDefault();

                                //确定是否将库存置为发货
                                if (record != null)
                                {

                                    record.State = PMSCommon.InventoryState.发货.ToString();
                                    ss.UpdatePlateByUID(record, uid);
                                }
                                model = ModelHelper.GetDeliveryItem(record, Number, DeliveryType);
                            }
                        }
                        else
                        {
                            //读取产品记录并转换
                            using (var ss = new ProductServiceClient())
                            {
                                var record = ss.GetProductByProductID(item.Lot).FirstOrDefault();

                                //确定是否将库存置为发货
                                if (record != null)
                                {

                                    record.State = PMSCommon.InventoryState.发货.ToString();
                                    ss.UpdateProductByUID(record, uid);
                                }

                                model = ModelHelper.GetDeliveryItem(record, Number, DeliveryType);
                            }
                        }





                        //插入到绑定记录
                        using (var service = new DeliveryServiceClient())
                        {
                            if (model != null)
                            {
                                //确定发货单ID
                                model.DeliveryID = currentDelivery.ID;

                                service.AddDeliveryItemByUID(model, uid);
                                item.HasProcessed = true;
                            }
                        }
                    }
                    count++;
                    progressValue = count * 100 / Lots.Count;
                    if (DoSomething != null)
                    {
                        DoSomething(progressValue);
                        System.Threading.Thread.Sleep(50);
                    }

                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        private void CheckInProduct(LotModel item)
        {
            if (item.IsValid)
            {
                using (var service = new ProductServiceClient())
                {
                    //这里增加一个服务？
                    int count = service.GetProductByProductID(item.Lot).Count();
                    if (count == 0)
                    {
                        item.IsValid = false;
                        item.AppendMessage("[产品记录]中不存在");
                    }
                }
            }
        }

        private void CheckInPlate(LotModel item)
        {
            if (item.IsValid)
            {
                using (var service = new PlateServiceClient())
                {
                    //这里增加一个服务？
                    int count = service.GetPlateByPlateID(item.Lot).Count();
                    if (count == 0)
                    {
                        item.IsValid = false;
                        item.AppendMessage("[背板记录]中不存在");
                    }
                }
            }
        }

        private void CheckInDeliveryItem(LotModel item)
        {
            if (item.IsValid)
            {
                using (var service = new DeliveryServiceClient())
                {
                    //这里增加一个服务？
                    int count = service.GetDeliveryItemByProductID(item.Lot).Count();
                    if (count > 0)
                    {
                        item.IsValid = false;
                        item.AppendMessage("[发货流水记录]中已存在");
                    }
                }
            }
        }


    }

}
