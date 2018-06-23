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
                    if (item.IsValid)
                    {
                        //检查Lot是否存在于检测记录中
                        CheckInRecordTest(item);
                        //检查Lot是否已存在于绑定记录中
                        CheckInRecordBonding(item);
                    }
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
                    using (var service=new RecordBondingServiceClient())
                    {

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
            using (var service = new RecordTestServiceClient())
            {

                int count = service.GetRecordTestByProductID(item.Lot).Count();
                if (count == 0)
                {
                    item.IsValid = false;
                    item.AppendMessage("测试记录中不存在");
                }
            }
        }

        private void CheckInRecordBonding(LotModel item)
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
