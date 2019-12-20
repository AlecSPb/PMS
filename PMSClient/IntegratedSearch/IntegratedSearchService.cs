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
        //公共结果存储
        private DataResultModel result;
        public IntegratedSearchService()
        {
            result = new DataResultModel();
        }

        /// <summary>
        /// 获取搜索结果
        /// </summary>
        /// <param name="productid">产品ID</param>
        /// <returns></returns>
        public string GetSearchResult(string productid)
        {
            //测试记录
            GetRecordTestString(productid);
            //只有测试记录搜索成功的前提下，才能搜索其他的东西
            if (result.IsSucceed)
            {
                //订单信息
                GetOrderString(result.SearchPMINumber);
                if (result.IsSucceed)
                {
                    GetPlanString(result.SearchOrderId);                  
                }
                GetMaterialOrderString(result.SearchPMINumber);
                GetMaterialInventoryString(result.SearchPMINumber);


            }
            return result.Result.ToString();
        }

        private string NotFound(string msg)
        {
            return $"没有找到相关[{msg}]记录，请检查输入ID是否正确";
        }


        public void GetRecordTestString(string productid)
        {
            DcRecordTest model;
            using (var service = new RecordTestServiceClient())
            {
                model = service.GetRecordTestByProductID(productid).FirstOrDefault();
            }
            if (model != null)
            {
                result.IsSucceed = true;
                //记录索引字段
                result.SearchProductID = model.ProductID;
                result.SearchPMINumber = model.PMINumber;

                result.Result.AppendLine("[检测数据]如下：");
                result.Result.AppendLine($"产品ID:{model.ProductID}");
                result.Result.AppendLine($"成分:{model.Composition}");
                result.Result.AppendLine($"客户:{model.Customer}");
                result.Result.AppendLine($"PO:{model.PO}");
                result.Result.AppendLine($"PMI Number：{model.PMINumber}");
                result.Result.AppendLine($"重量:{model.Weight}");
                result.Result.AppendLine($"密度:{model.Density}");
                result.Result.AppendLine($"电阻率:{model.Resistance}");
                result.Result.AppendLine($"要求尺寸:{model.Dimension}");
                result.Result.AppendLine($"实际尺寸:{model.DimensionActual}");
                result.Result.AppendLine($"粗糙度:{model.Roughness}");
                result.Result.AppendLine($"超声:{model.CScan}");
                result.Result.AppendLine($"缺陷:{model.Dimension}");
                result.Result.AppendLine($"备注:{model.Remark}");


                result.Result.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                result.Result.AppendLine("没有找到记录");
            }
        }

        public void GetOrderString(string pminumber)
        {
            DcOrder order;
            using (var service = new OrderServiceClient())
            {
                order = service.GetOrderByPMINumber(pminumber);
            }

            if (order != null)
            {
                result.IsSucceed = true;
                //记录索引字段
                result.SearchOrderId = order.ID;
                result.Result.AppendLine("[订单数据]如下:");
                result.Result.AppendLine($"客户:{order.CustomerName}");
                result.Result.AppendLine($"成分:{order.CompositionStandard}");
                result.Result.AppendLine($"PMI Number:{order.PMINumber}");
                result.Result.AppendLine($"PO:{order.PO}");
                result.Result.AppendLine($"订单日期:{order.CreateTime.ToString()}");
                result.Result.AppendLine($"交付日期:{order.DeadLine.ToString()}");
                result.Result.AppendLine($"纯度:{order.Purity}");
                result.Result.AppendLine($"数量:{order.Quantity}{order.QuantityUnit}");
                result.Result.AppendLine($"加工尺寸:{order.Dimension}");
                result.Result.AppendLine($"加工细节:{order.DimensionDetails}");
                result.Result.AppendLine($"自样品要求:{order.SampleForAnlysis}");
                result.Result.AppendLine($"客户样品要求:{order.SampleNeed}");
                result.Result.AppendLine($"特殊要求:{order.SpecialRequirement}");

                result.Result.AppendLine();

            }
            else
            {
                result.IsSucceed = false;
                result.Result.AppendLine("没有找到记录");
            }
        }

        /// <summary>
        /// 获取热压计划
        /// </summary>
        /// <param name="pminumber"></param>
        /// <returns></returns>
        public void GetPlanString(Guid orderid)
        {
            DcPlanVHP[] plans;
            using (var s = new PlanVHPServiceClient())
            {
                plans = s.GetVHPPlansByOrderID(orderid);
            }

            if (plans != null)
            {
                result.IsSucceed = true;
                result.Result.AppendLine("[热压计划数据]如下:");
                foreach (var p in plans)
                {
                    result.Result.AppendLine($"日期:{p.PlanDate.ToString("yyMMdd")}");
                    result.Result.AppendLine($"压机:{p.VHPDeviceCode}");
                    result.Result.AppendLine($"类型:{p.PlanType}");
                    result.Result.AppendLine($"工艺:{p.ProcessCode}");
                    result.Result.AppendLine($"模具:{p.MoldType}");
                    result.Result.AppendLine($"内径:{p.MoldDiameter}");
                    result.Result.AppendLine($"厚度:{p.Thickness}");
                    result.Result.AppendLine($"熟练:{p.Quantity}");
                    result.Result.AppendLine($"单片:{p.SingleWeight}");
                    result.Result.AppendLine($"全部:{p.AllWeight}");
                    result.Result.AppendLine($"温度:{p.Temperature}");
                    result.Result.AppendLine($"压力:{p.Pressure}");
                    result.Result.AppendLine($"保温:{p.KeepTempTime}");
                    result.Result.AppendLine($"制粉:{p.MillingRequirement}");
                    result.Result.AppendLine($"热压:{p.VHPRequirement}");
                    result.Result.AppendLine($"机加:{p.MachineRequirement}");
                    result.Result.AppendLine($"特殊:{p.SpecialRequirement}");

                    result.Result.AppendLine();
                }
                result.Result.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("热压计划");
            }

        }

        public void GetMaterialOrderString(string pminumber)
        {
            DcMaterialOrderItemExtra[] items;
            using (var s=new MaterialOrderServiceClient())
            {
                items = s.GetMaterialOrderItemExtras(0, 50, "", pminumber, "", "");
            }

            if (items != null)
            {
                result.IsSucceed = true;
                result.Result.AppendLine("[原料订单数据]如下：");
                foreach (var item in items)
                {
                    result.Result.AppendLine($"供应商:{item.MaterialOrder.Supplier}");
                    result.Result.AppendLine($"创建时间:{item.MaterialOrderItem.CreateTime}");
                    result.Result.AppendLine($"优先级:{item.MaterialOrderItem.Priority}");
                    result.Result.AppendLine($"成分:{item.MaterialOrderItem.Composition}");
                    result.Result.AppendLine($"纯度:{item.MaterialOrderItem.Purity}");
                    result.Result.AppendLine($"重量:{item.MaterialOrderItem.Weight}");
                    result.Result.AppendLine($"备注:{item.MaterialOrderItem.Remark}");
                    result.Result.AppendLine($"交付日期:{item.MaterialOrderItem.DeliveryDate}");
                }

                result.Result.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("材料订单");
            }


        }

        public void GetMaterialInventoryString(string pminumber)
        {

        }

        public void GetRecordMillingString(string pminumber)
        {

        }

        public void GetRecordVHPString(string pminumber)
        {

        }

        public void GetRecordDemold(string pminumber)
        {

        }

        public void GetRecordMachine(string pminumber)
        {

        }

        public void GetRecordBonding(string productid)
        {

        }

        public void GetDelivery(string productid)
        {

        }




    }
}
