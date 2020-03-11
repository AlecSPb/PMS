using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using PMSClient.OutsideProcessService;

namespace PMSClient.DataProcess.ScanInput
{
    /// <summary>
    /// 产品扫描结果处理
    /// </summary>
    public class ProcessOutsideProcess : ProcessBase
    {
        public ProcessOutsideProcess()
        {

        }

        public string OutisideProcosser { get; set; } = PMSCommon.OutsideProcessor.成都炬科光学.ToString();
        public override void Check(Action<double> DoSomething)
        {
            try
            {
                double progressValue = 0;
                double count = 0;
                foreach (var item in Lots)
                {
                    //默认有效，多次否决
                    CheckIsStandard(item);
                    CheckInRecordTest(item);
                    //CheckInProduct(item);


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
                double progressValue = 0;
                double count = 0;
                foreach (var item in Lots)
                {
                    //默认有效，多次否决
                    CheckIsStandard(item);
                    CheckInRecordTest(item);
                    //CheckInProduct(item);

                    //有效继续
                    if (item.IsValid)
                    {
                        DcOutsideProcess model = null;
                        //读取测试记录并转换
                        using (var ss = new RecordTestServiceClient())
                        {
                            var record_test = ss.GetRecordTestByProductID(item.Lot).FirstOrDefault();
                            model = ModelHelper.GetOutsideProcess(record_test);
                        }

                        //插入到绑定记录
                        using (var service = new OutsideProcessServiceClient())
                        {
                            if (model != null)
                            {
                                service.Add(model);
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


        private void CheckInRecordTest(LotModel item)
        {
            if (item.IsValid)
            {
                using (var service = new RecordTestServiceClient())
                {
                    //这里增加一个服务？
                    int count = service.GetRecordTestByProductID(item.Lot).Count();
                    if (count == 0)
                    {
                        item.IsValid = false;
                        item.AppendMessage("[测试记录]中不存在");
                    }
                }
            }
        }

        //private void CheckInProduct(LotModel item)
        //{
        //    if (item.IsValid)
        //    {
        //        using (var service = new ProductServiceClient())
        //        {
        //            //这里增加一个服务？
        //            int count = service.GetProductByProductID(item.Lot).Count();
        //            if (count > 0)
        //            {
        //                item.IsValid = false;
        //                item.AppendMessage("[产品记录]中已存在");
        //            }
        //        }
        //    }
        //}





    }
}
