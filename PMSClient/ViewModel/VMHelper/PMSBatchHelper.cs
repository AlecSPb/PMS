using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.ViewModel
{
    public static class PMSBatchHelper
    {
        public static  void SetRecordTestBatchEnable(bool flag)
        {
            PMSHelper.ViewModels.RecordTestSelect.IsNewBatch = flag;
        }
        public static void SetRecordBondingBatchEnable(bool flag)
        {
            PMSHelper.ViewModels.RecordBondingSelect.IsNewBatch = flag;
        }

        public static void SetPlateBatchEnable(bool flag)
        {
            PMSHelper.ViewModels.PlateSelect.IsNewBatch = flag;
        }

        public static void SetProductBatchEnable(bool flag)
        {
            PMSHelper.ViewModels.ProductSelect.IsNewBatch = flag;
        }




    }
}
