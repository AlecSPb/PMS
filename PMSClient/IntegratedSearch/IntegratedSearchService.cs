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
            try
            {
                //测试记录
                GetRecordTestString(productid);
                //只有测试记录搜索成功的前提下，才能搜索其他的东西
                if (result.IsSucceed)
                {
                    AddMessage($"===========以下信息为测试记录【{productid}】关联的所有信息，请核实===========");
                    //订单信息
                    GetOrderString(result.SearchPMINumber);
                    if (result.IsSucceed)
                    {
                        GetPlanString(result.SearchOrderId);
                        GetMaterialOrderString(result.SearchPMINumber);
                        GetMaterialInventoryString(result.SearchPMINumber);
                        GetRecordMillingString(result.SearchPMINumber);
                        GetRecordDemold(result.SearchPMINumber);
                        GetRecordMachine(result.SearchPMINumber);
                    }
                    GetRecordBonding(productid);
                    GetDelivery(productid);
                }
                return result.Content.ToString();
            }
            catch (Exception ex)
            {
                return "检索出错" + "\r\n" + ex.Message;
            }
        }

        private void AddMessage(string msg)
        {
            result.Content.AppendLine(msg);
        }

        private string NotFound(string msg)
        {
            return $"没有找到相关【{msg}】记录，请检查输入ID是否正确";
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

                result.Content.AppendLine("【检测数据】如下：");
                result.Content.AppendLine($"产品ID:{model.ProductID}");
                result.Content.AppendLine($"成分:{model.Composition}");
                result.Content.AppendLine($"客户:{model.Customer}");
                result.Content.AppendLine($"PO:{model.PO}");
                result.Content.AppendLine($"内部编号：{model.PMINumber}");
                result.Content.AppendLine($"重量:{model.Weight}g");
                result.Content.AppendLine($"密度:{model.Density}g/cm3");
                result.Content.AppendLine($"电阻率:{model.Resistance}ohmcm");
                result.Content.AppendLine($"要求尺寸:{model.Dimension}");
                result.Content.AppendLine($"实际尺寸:{model.DimensionActual}");
                result.Content.AppendLine($"粗糙度:{model.Roughness}");
                result.Content.AppendLine($"超声:{model.CScan}");
                result.Content.AppendLine($"缺陷:{model.Dimension}");
                result.Content.AppendLine($"备注:{model.Remark}");


                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                result.Content.AppendLine("没有找到记录");
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
                result.Content.AppendLine("【订单数据】如下:");
                result.Content.AppendLine($"客户:{order.CustomerName}");
                result.Content.AppendLine($"成分:{order.CompositionStandard}");
                result.Content.AppendLine($"内部编号:{order.PMINumber}");
                result.Content.AppendLine($"PO:{order.PO}");
                result.Content.AppendLine($"订单日期:{order.CreateTime.ToString()}");
                result.Content.AppendLine($"交付日期:{order.DeadLine.ToString()}");
                result.Content.AppendLine($"纯度:{order.Purity}");
                result.Content.AppendLine($"数量:{order.Quantity}{order.QuantityUnit}");
                result.Content.AppendLine($"加工尺寸:{order.Dimension}");
                result.Content.AppendLine($"加工细节:{order.DimensionDetails}");
                result.Content.AppendLine($"自样品要求:{order.SampleForAnlysis}");
                result.Content.AppendLine($"客户样品要求:{order.SampleNeed}");
                result.Content.AppendLine($"特殊要求:{order.SpecialRequirement}");

                result.Content.AppendLine();

            }
            else
            {
                result.IsSucceed = false;
                result.Content.AppendLine("没有找到记录");
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
                result.Content.AppendLine("【热压计划数据】如下:");
                foreach (var p in plans)
                {
                    result.Content.AppendLine($"日期:{p.PlanDate.ToString("yyMMdd")}");
                    result.Content.AppendLine($"压机:{p.VHPDeviceCode}");
                    result.Content.AppendLine($"类型:{p.PlanType}");
                    result.Content.AppendLine($"工艺:{p.ProcessCode}");
                    result.Content.AppendLine($"模具:{p.MoldType}");
                    result.Content.AppendLine($"内径:{p.MoldDiameter}");
                    result.Content.AppendLine($"厚度:{p.Thickness}");
                    result.Content.AppendLine($"熟练:{p.Quantity}");
                    result.Content.AppendLine($"单片:{p.SingleWeight}");
                    result.Content.AppendLine($"全部:{p.AllWeight}");
                    result.Content.AppendLine($"温度:{p.Temperature}");
                    result.Content.AppendLine($"压力:{p.Pressure}");
                    result.Content.AppendLine($"保温:{p.KeepTempTime}");
                    result.Content.AppendLine($"制粉:{p.MillingRequirement}");
                    result.Content.AppendLine($"热压:{p.VHPRequirement}");
                    result.Content.AppendLine($"机加:{p.MachineRequirement}");
                    result.Content.AppendLine($"特殊:{p.SpecialRequirement}");

                    result.Content.AppendLine();
                }
                result.Content.AppendLine();
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
            using (var s = new MaterialOrderServiceClient())
            {
                items = s.GetMaterialOrderItemExtras(0, 50, "", pminumber, "", "");
            }

            if (items != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【原料订单数据】如下：");
                foreach (var item in items)
                {
                    result.Content.AppendLine($"供应商:{item.MaterialOrder.Supplier}");
                    result.Content.AppendLine($"创建时间:{item.MaterialOrderItem.CreateTime}");
                    result.Content.AppendLine($"优先级:{item.MaterialOrderItem.Priority}");
                    result.Content.AppendLine($"成分:{item.MaterialOrderItem.Composition}");
                    result.Content.AppendLine($"纯度:{item.MaterialOrderItem.Purity}");
                    result.Content.AppendLine($"重量:{item.MaterialOrderItem.Weight}");
                    result.Content.AppendLine($"备注:{item.MaterialOrderItem.Remark}");
                    result.Content.AppendLine($"交付日期:{item.MaterialOrderItem.DeliveryDate}");
                    result.Content.AppendLine();
                }

                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("材料订单");
            }


        }

        public void GetMaterialInventoryString(string pminumber)
        {
            DcMaterialInventoryIn[] ins;
            DcMaterialInventoryOut[] outs;
            using (var s = new MaterialInventoryServiceClient())
            {
                ins = s.GetMaterialInventoryInsBySearch(0, 50, "", "", "", pminumber);
                outs = s.GetMaterialInventoryOutsBySearch(0, 50, "", "", "", pminumber);
            }

            //入库
            if (ins != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【原料入库数据】如下：");

                foreach (var item in ins)
                {
                    result.Content.AppendLine($"时间:{item.CreateTime}");
                    result.Content.AppendLine($"供应商:{item.Supplier}");
                    result.Content.AppendLine($"成分:{item.Composition}");
                    result.Content.AppendLine($"批号:{item.MaterialLot}");
                    result.Content.AppendLine($"内部编号:{item.PMINumber}");
                    result.Content.AppendLine($"纯度:{item.Purity}");
                    result.Content.AppendLine($"重量:{item.Weight}");
                    result.Content.AppendLine($"备注:{item.Remark}");
                    result.Content.AppendLine();

                }

                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("材料出库");
            }

            //出库
            if (outs != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【原料出库数据】如下：");
                foreach (var item in outs)
                {
                    result.Content.AppendLine($"时间:{item.CreateTime}");
                    result.Content.AppendLine($"供应商:{item.Receiver}");
                    result.Content.AppendLine($"成分:{item.Composition}");
                    result.Content.AppendLine($"批号:{item.MaterialLot}");
                    result.Content.AppendLine($"内部编号:{item.PMINumber}");
                    result.Content.AppendLine($"纯度:{item.Purity}");
                    result.Content.AppendLine($"重量:{item.Weight}");
                    result.Content.AppendLine($"备注:{item.Remark}");
                    result.Content.AppendLine();
                }

                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("材料入库");
            }



        }

        public void GetRecordMillingString(string pminumber)
        {
            DcRecordMilling[] mills;
            using (var s = new RecordMillingServiceClient())
            {
                mills = s.GetRecordMillingsByPMINumber(pminumber);
            }
            if (mills != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【制粉记录数据】如下：");
                foreach (var item in mills)
                {
                    result.Content.AppendLine($"热压编号:{item.VHPPlanLot}");
                    result.Content.AppendLine($"成分:{item.Composition}");
                    result.Content.AppendLine($"内部编号:{item.PMINumber}");
                    result.Content.AppendLine($"原料类型:{item.MaterialType}");
                    result.Content.AppendLine($"原料备注:{item.MaterialSource}");
                    result.Content.AppendLine($"回收ID:{item.RecycleID}");
                    result.Content.AppendLine($"湿度:{item.RoomHumidity}");
                    result.Content.AppendLine($"温度:{item.RoomTemperature}");
                    result.Content.AppendLine($"制粉方式:{item.MillingTool}");
                    result.Content.AppendLine($"制粉时间:{item.MillingTime}");
                    result.Content.AppendLine($"气体保护:{item.GasProtection}");
                    result.Content.AppendLine($"粒径:{item.GrainSize}");
                    result.Content.AppendLine($"制粉时长:{item.MillingTime}");
                    result.Content.AppendLine($"进料重量:{item.WeightIn}");
                    result.Content.AppendLine($"出料重量:{item.WeightOut}");
                    result.Content.AppendLine($"余料重量:{item.WeightRemain}");
                    result.Content.AppendLine($"损失率:{item.Ratio}");
                    result.Content.AppendLine($"含氧量:{item.Oxygen}");
                    result.Content.AppendLine($"熔点:{item.MeltingPoint}");
                    result.Content.AppendLine($"备注:{item.Remark}");

                    result.Content.AppendLine();
                }
                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("制粉记录");
            }
        }

        public void GetRecordVHPString(string orderid)
        {
            //暂不实现
            //TODO:获取热压数据
        }

        public void GetRecordDemold(string pminumber)
        {
            DcRecordDeMold[] demolds;
            using (var s = new RecordDeMoldServiceClient())
            {
                demolds = s.GetRecordDeMoldsByPMINumber(pminumber);
            }
            if (demolds != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【取模记录数据】如下：");

                foreach (var item in demolds)
                {
                    result.Content.AppendLine($"热压编号:{item.VHPPlanLot}");
                    result.Content.AppendLine($"内部编号:{item.PMINumber}");
                    result.Content.AppendLine($"计划类型:{item.PlanType}");
                    result.Content.AppendLine($"成分:{item.Composition}");
                    result.Content.AppendLine($"产品尺寸:{item.Dimension}");
                    result.Content.AppendLine($"设计毛坯:{item.CalculateDimension}");
                    result.Content.AppendLine($"计算密度:{item.CalculationDensity}");
                    result.Content.AppendLine($"取模温度:{item.Temperature1}");
                    result.Content.AppendLine($"脱模温度:{item.Temperature2}");
                    result.Content.AppendLine($"脱模类型:{item.DeMoldType}");
                    result.Content.AppendLine($"重量:{item.Weight}");
                    result.Content.AppendLine($"直径:{item.Diameter1}");
                    result.Content.AppendLine($"厚度1:{item.Thickness1}");
                    result.Content.AppendLine($"厚度2:{item.Thickness2}");
                    result.Content.AppendLine($"厚度3:{item.Thickness3}");
                    result.Content.AppendLine($"厚度4:{item.Thickness4}");
                    result.Content.AppendLine($"密度:{item.Density}");
                    result.Content.AppendLine($"备注:{item.Remark}");
                    result.Content.AppendLine($"创建者:{item.Creator}");
                    result.Content.AppendLine($"创建时间:{item.CreateTime}");


                    result.Content.AppendLine();
                }
                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("取模记录");

            }
        }

        public void GetRecordMachine(string pminumber)
        {
            DcRecordMachine[] machines;
            using (var s = new RecordMachineServiceClient())
            {
                machines = s.GetRecordMachinesByPMINumber(pminumber);
            }

            if (machines != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【加工记录数据】如下：");

                foreach (var item in machines)
                {
                    result.Content.AppendLine($"热压编号:{item.VHPPlanLot}");
                    result.Content.AppendLine($"内部编号:{item.PMINumber}");
                    result.Content.AppendLine($"成分:{item.Composition}");
                    result.Content.AppendLine($"产品尺寸:{item.Dimension}");
                    result.Content.AppendLine($"直径:{item.Diameter1}");
                    result.Content.AppendLine($"厚度1:{item.Thickness1}");
                    result.Content.AppendLine($"厚度2:{item.Thickness2}");
                    result.Content.AppendLine($"厚度3:{item.Thickness3}");
                    result.Content.AppendLine($"厚度4:{item.Thickness4}");
                    result.Content.AppendLine($"创建者:{item.Creator}");
                    result.Content.AppendLine($"创建时间:{item.CreateTime}");


                    result.Content.AppendLine();
                }
                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("加工记录");
            }



        }

        public void GetRecordBonding(string productid)
        {
            DcRecordBonding[] bondings;
            using (var s = new RecordBondingServiceClient())
            {
                bondings = s.GetRecordBondingByProductID(productid);
            }

            if (bondings != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【绑定记录数据】如下：");

                foreach (var item in bondings)
                {
                    result.Content.AppendLine($"靶材ID:{item.TargetProductID}");
                    result.Content.AppendLine($"靶材成分:{item.TargetComposition}");
                    result.Content.AppendLine($"靶材缩写:{item.TargetAbbr}");
                    result.Content.AppendLine($"内部编号:{item.TargetPMINumber}");
                    result.Content.AppendLine($"靶材批号:{item.TargetPO}");
                    result.Content.AppendLine($"靶材尺寸:{item.TargetDimension}");
                    result.Content.AppendLine($"背板类型:{item.PlateType}");
                    result.Content.AppendLine($"背板批号:{item.PlateLot}");
                    result.Content.AppendLine($"焊合率:{item.WeldingRate}");
                    result.Content.AppendLine($"备注:{item.Remark}");

                    result.Content.AppendLine($"创建者:{item.Creator}");
                    result.Content.AppendLine($"创建时间:{item.CreateTime}");


                    result.Content.AppendLine();
                }
                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("绑定记录");
            }

        }

        public void GetDelivery(string productid)
        {
            DcDeliveryItem[] items;
            using (var s = new DeliveryServiceClient())
            {
                items = s.GetDeliveryItems(0, 50, productid, "");
            }
            if (items != null)
            {
                result.IsSucceed = true;
                result.Content.AppendLine("【发货记录数据】如下：");

                foreach (var item in items)
                {
                    result.Content.AppendLine($"产品类型:{item.ProductType}");
                    result.Content.AppendLine($"产品ID:{item.ProductID}");
                    result.Content.AppendLine($"靶材成分:{item.Composition}");
                    result.Content.AppendLine($"靶材缩写:{item.Abbr}");
                    result.Content.AppendLine($"客户:{item.Customer}");
                    result.Content.AppendLine($"PO:{item.PO}");
                    result.Content.AppendLine($"尺寸:{item.Dimension}");
                    result.Content.AppendLine($"重量:{item.Weight}");
                    result.Content.AppendLine($"装箱号:{item.PackNumber}");
                    result.Content.AppendLine($"记录细节:{item.DetailRecord}");
                    result.Content.AppendLine($"备注:{item.Remark}");

                    result.Content.AppendLine($"创建者:{item.Creator}");
                    result.Content.AppendLine($"创建时间:{item.CreateTime}");


                    result.Content.AppendLine();
                }
                result.Content.AppendLine();
            }
            else
            {
                result.IsSucceed = false;
                NotFound("发货记录");
            }

        }




    }
}
