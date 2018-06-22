using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using PMSClient.MainService;

namespace PMSClient.DataProcess.ScanInput
{
    /// <summary>
    /// 处理扫描输入
    /// </summary>
    public class ProcessScanInput
    {
        public ProcessScanInput()
        {
            uid = PMSHelper.CurrentSession.CurrentUser.UserName;
            Lots = new List<LotModel>();
        }

        private string uid;

        public List<LotModel> Lots;

        public string TargetTable = string.Empty;

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
        private TableSource GetDSType(string lot)
        {

            if (lot.Length >= 9)
            {
                //180116-AB-1  180616-A-1  180616-A-1A
                string pattern = @"-\w{1,2}-";
                var match = Regex.Match(lot, pattern);
                if (match.Success)
                {
                    string code = match.Value.Replace("-", "");
                    switch (code.ToUpper())
                    {
                        case "OS":
                            return TableSource.Outsource;
                        case "BP":
                            return TableSource.Plate;
                        default:
                            return TableSource.Test;
                    }
                }
                else
                {
                    return TableSource.Unknown;
                }
            }
            else
            {
                return TableSource.Unknown;
            }
        }

        /// <summary>
        /// 拆分整体多行批号到数组
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string[] SplitLots(string text)
        {
            return text.Split(Environment.NewLine.ToCharArray(),
                                    StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="lot_string"></param>
        public void Intialize(string lot_string)
        {
            Lots.Clear();
            SplitLots(lot_string).ToList().ForEach(i =>
            {
                LotModel lot = new LotModel()
                {
                    Lot = i.Trim(),
                    IsValid = IsValid(i.Trim()),
                    DataTableSource = GetDSType(i.Trim()),
                    ExceptionMessage = (IsValid(i.Trim()) ? "OK" : "格式不正确")
                };
                Lots.Add(lot);
            });
        }

        public string FromLotsToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in Lots)
            {
                sb.Append(line.IsValid);
                sb.AppendLine(" " + line.Lot + " " + line.DataTableSource.ToString());
            }
            return sb.ToString();
        }


        /// <summary>
        /// 批量检查数据
        /// </summary>
        public void CheckValid()
        {
            foreach (var item in Lots)
            {

            }
        }

        /// <summary>
        /// 批量读取并插入数据
        /// </summary>
        public void Process()
        {
            foreach (var item in Lots)
            {
                switch (GetDSType(item.Lot))
                {
                    case TableSource.Test:
                        break;
                    case TableSource.Plate:
                        break;
                    case TableSource.Outsource:
                        break;
                    case TableSource.Unknown:
                        break;
                    default:
                        break;
                }
            }
        }

        #endregion

        #region 从各种库中找到记录
        //从检测记录中查找
        private List<DcRecordTest> FindInRecordTest(string lot)
        {
            using (var service = new RecordTestServiceClient())
            {
                return service.GetRecordTestByProductID(lot).ToList();
            }
        }
        //从绑定记录中查找
        private List<DcRecordBonding> FindInRecordBonding(string lot)
        {
            using (var service = new RecordBondingServiceClient())
            {
                return service.GetRecordBondingByProductID(lot).ToList();
            }
        }
        //从背板记录中查找
        private List<DcPlate> FindInRecordPlate(string lot)
        {
            using (var service = new PlateServiceClient())
            {
                return service.GetPlateByPlateID(lot).ToList();
            }
        }
        //从产品库存中查找
        private List<DcProduct> FindInProduct(string lot)
        {
            using (var service = new ProductServiceClient())
            {
                return service.GetProductByProductID(lot).ToList();
            }
        }
        #endregion

        #region 插入记录到各种库中
        private void InsertToRecordBonding(DcRecordBonding model)
        {
            using (var service = new RecordBondingServiceClient())
            {
                service.AddRecordBongdingByUID(model, uid);
            }
        }

        private void InsertToProduct(DcProduct model)
        {
            using (var service = new ProductServiceClient())
            {
                service.AddProductByUID(model, uid);
            }
        }

        private void InsertToDeliveryItem(DcDeliveryItem model)
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
