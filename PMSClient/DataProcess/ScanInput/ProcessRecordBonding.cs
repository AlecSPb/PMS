using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient.DataProcess.ScanInput
{
    /// <summary>
    /// 用于RecordBonding
    /// </summary>
    public class ProcessRecordBonding : ProcessBase
    {
        public ProcessRecordBonding()
        {

        }

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
                    CheckInRecordBonding(item);


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
                    //检查
                    CheckIsStandard(item);
                    CheckInRecordTest(item);
                    CheckInRecordBonding(item);

                    //有效继续
                    if (item.IsValid)
                    {
                        DcRecordBonding model = null;
                        //读取测试记录并转换
                        using (var ss = new RecordTestServiceClient())
                        {
                            var record_test = ss.GetRecordTestByProductID(item.Lot).FirstOrDefault();
                            model = ModelHelper.GetRecordBonding(record_test);
                        }

                        //插入到绑定记录
                        using (var service = new RecordBondingServiceClient())
                        {
                            if (model != null)
                            {
                                service.AddRecordBongdingByUID(model, uid);
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

        private void CheckInRecordBonding(LotModel item)
        {
            if (item.IsValid)
            {
                using (var service = new RecordBondingServiceClient())
                {
                    int count = service.GetRecordBondingByProductID(item.Lot).Count();
                    if (count > 0)
                    {
                        item.IsValid = false;
                        item.AppendMessage("[绑定记录]中已存在");
                    }
                }
            }
        }


    }
}
