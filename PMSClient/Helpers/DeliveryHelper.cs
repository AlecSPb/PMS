﻿using PMSClient.MainService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public static class DeliveryHelper
    {
        /// <summary>
        /// 按照产品ID从测试记录中获取背板编号
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static string GetBPLotFromTest(string productid)
        {
            try
            {
                using (var s_recordTest = new RecordTestServiceClient())
                {
                    var test = s_recordTest.GetRecordTestByProductID(productid).FirstOrDefault();
                    if (test != null)
                    {
                        string bp_lot = test.BackingPlateLot;
                        return bp_lot;
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
            return string.Empty;
        }

        /// <summary>
        /// 按照产品ID从绑定记录当中获取背板编号
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public static string GetBPLotFromBonding(string productid)
        {
            try
            {
                using (var s_bonding = new RecordBondingServiceClient())
                {
                    var bonding = s_bonding.GetRecordBondingByProductID(productid)
                        .FirstOrDefault();
                    return bonding.PlateLot;
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
            return string.Empty;
        }


    }
}
