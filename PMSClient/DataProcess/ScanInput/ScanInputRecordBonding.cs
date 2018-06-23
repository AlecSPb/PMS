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
    public class ScanInputRecordBonding : ScanInputBase
    {
        public ScanInputRecordBonding()
        {

        }






        public override void Check()
        {
            try
            {
                foreach (var item in Lots)
                {
                    //默认有效，多次否决
                    //检查是否规范
                    CheckIsStandard(item);
                    //检查Lot是否存在于检测记录中
                    CheckInRecordTest(item);
                    //检查Lot是否已存在于绑定记录中
                    CheckInRecordBonding(item);

                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }

        }

        public override void Process()
        {
            Check();
            try
            {
                foreach (var item in Lots)
                {
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
                        item.AppendMessage("测试记录中不存在");
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
                        item.AppendMessage("绑定记录中已存在");
                    }
                }
            }
        }


    }
}
