using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PMSClient.MainService;

namespace PMSClient.DataProcess
{
    /// <summary>
    /// 处理扫描输入
    /// </summary>
    public class ProcessScanInput
    {
        public ProcessScanInput()
        {
            uid = PMSHelper.CurrentSession.CurrentUser.UserName;
        }

        private string uid;




        #region 公用
        /// <summary>
        /// 判断lot是否符合要求
        /// </summary>
        /// <param name="lot"></param>
        /// <returns></returns>
        private bool IsValid(string lot)
        {
            string pattern = @"\d{6}-\w{1,2}-\w+";
            return Regex.IsMatch(lot, pattern, RegexOptions.IgnoreCase);
        }



        /// <summary>
        /// 返回数据源类型
        /// </summary>
        /// <param name="lot"></param>
        /// <returns></returns>
        public DTS GetDSType(string lot)
        {

            if (lot.Length >= 9)
            {
                //180116-AB-1  180616-A-1  180616-A-1A
                string mark = lot.Substring(7, 2).Replace("-", "");
                switch (mark.ToUpper())
                {
                    case "OS":
                        return DTS.Outsource;
                    case "BP":
                        return DTS.Plate;
                    default:
                        return DTS.Test;
                }
            }
            else
            {
                return DTS.Unknown;
            }
        }

        /// <summary>
        /// 拆分整体多行批号到数组
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string[] SplitLots(string text)
        {
            return text.Split(Environment.NewLine.ToCharArray(),
                                    StringSplitOptions.RemoveEmptyEntries);
        }


        public string CheckAll(string lots)
        {
            if (string.IsNullOrEmpty(lots))
                return "";

            string[] data = SplitLots(lots);
            StringBuilder sb = new StringBuilder();
            foreach (var line in data)
            {
                sb.Append(IsValid(line));
                sb.AppendLine(" " + line);
            }
            return sb.ToString();
        }

        #endregion

        #region 从各种库中找到记录
        //从检测记录中查找
        private List<DcRecordTest> FindInRecordTest(string lot)
        {
            using (var service=new RecordTestServiceClient())
            {
                return service.GetRecordTestByProductID(lot).ToList();
            }
        }
        //从绑定记录中查找
        private List<DcRecordBonding> FindInRecordBonding(string lot)
        {
            using (var service=new RecordBondingServiceClient())
            {
                return service.GetRecordBondingByProductID(lot).ToList();
            }
        }
        //从背板记录中查找
        private List<DcPlate> FindInRecordPlate(string lot)
        {
            using (var service=new PlateServiceClient())
            {
                return service.GetPlateByPlateID(lot).ToList();
            }
        }
        //从产品库存中查找
        private List<DcProduct> FindInProduct(string lot)
        {
            using (var service=new ProductServiceClient())
            {
                return service.GetProductByProductID(lot).ToList();
            }
        }
        #endregion

        #region 插入记录到各种库中
        public void InsertToRecordBonding(DcRecordBonding model)
        {
            using (var service = new RecordBondingServiceClient())
            {
                service.AddRecordBongdingByUID(model, uid);
            }
        }

        public void InsertToProduct(DcProduct model)
        {
            using (var service = new ProductServiceClient())
            {
                service.AddProductByUID(model, uid);
            }
        }

        public void InsertToDeliveryItem(DcDeliveryItem model)
        {
            using (var service = new DeliveryServiceClient())
            {
                service.AddDeliveryItemByUID(model, uid);
            }
        }
        #endregion

        #region 确定要插入的记录是否已经存在
        //判断绑定记录是否存在
        public bool IsInRecordBonding(string lot)
        {
            using (var service = new RecordBondingServiceClient())
            {
                return service.GetRecordBondingByProductID(lot).Count() > 0;
            }
        }
        //判定产品库记录是否存在
        public bool IsInProductInventory(string lot)
        {
            using (var service = new ProductServiceClient())
            {
                return service.GetProductByProductID(lot).Count() > 0;
            }
        }
        //判定发货计划是否存在
        public bool IsInDelivery(string lot)
        {
            using (var service = new DeliveryServiceClient())
            {
                return service.GetDeliveryItemByProductID(lot).Count() > 0;
            }
        }
        #endregion
    }
}
