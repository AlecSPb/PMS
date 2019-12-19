using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using PMSClient.MainService;

namespace PMSClient.IntegratedSearch
{
    public class IntegratedSearchService
    {
        /// <summary>
        /// 获取测试记录
        /// </summary>
        /// <param name="productid"></param>
        /// <returns></returns>
        public DataResultModel GetRecordTestString(string productid)
        {
            DcRecordTest model;
            using (var service = new RecordTestServiceClient())
            {
                model = service.GetRecordTestByProductID(productid).FirstOrDefault();
            }
            DataResultModel result = new DataResultModel();
            if (model != null)
            {
                result.IsSucceed = true;
                result.Result.AppendLine("测试记录如下：");
                result.Result.AppendLine($"{model.ProductID}-{model.Composition}");
                result.Result.AppendLine($"客户:{model.Customer}-{model.PO}");
                result.Result.AppendLine($"重量:{model.Weight}");
                result.Result.AppendLine($"密度:{model.Density}");
                result.Result.AppendLine($"电阻率:{model.Resistance}");
                result.Result.AppendLine($"要求尺寸:{model.Dimension}");
                result.Result.AppendLine($"实际尺寸:{model.DimensionActual}");
                result.Result.AppendLine($"粗糙度:{model.Roughness}");
                result.Result.AppendLine($"超声:{model.CScan}");
                result.Result.AppendLine($"缺陷:{model.Dimension}");
                result.Result.AppendLine($"备注:{model.Remark}");


                result.SearchProductID = model.ProductID;
            }
            else
            {
                result.IsSucceed = false;
                result.Result.AppendLine("没有找到记录");
            }
            return result;
        }
        /// <summary>
        /// 获取订单信息
        /// </summary>
        /// <param name="pminumber"></param>
        /// <returns></returns>
        public DataResultModel GetOrderString(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetMaterialOrderString(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }


        public DataResultModel GetMaterialInventoryString(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetRecordMillingString(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetRecordVHPString(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetRecordDemold(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetRecordMachine(string pminumber)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetRecordBonding(string productid)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetDelivery(string productid)
        {
            DataResultModel result = new DataResultModel();


            return result;
        }

        public DataResultModel GetPlanString(string pminumber)
        {
            DataResultModel result = new DataResultModel();

            return result;
        }




    }
}
