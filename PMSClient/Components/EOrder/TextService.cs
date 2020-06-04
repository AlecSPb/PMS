using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.EOrder
{
    public class TextService
    {
        public static string GetOrderText(Order order)
        {
            if (order == null) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("[GUID]:");
            sb.AppendLine(order.GUIDID.ToString());
            sb.Append("[客户名称]:");
            sb.AppendLine(order.CustomerName ?? "空");
            sb.Append("[PO]:");
            sb.AppendLine(order.PO ?? "空");
            sb.Append("[成分]:");
            sb.AppendLine(order.Composition ?? "空");
            sb.Append("[成分细节]:");
            sb.AppendLine(order.CompositionDetail ?? "空");
            sb.Append("[产品类型]:");
            sb.AppendLine(order.ProductType ?? "空");
            sb.Append("[纯度]:");
            sb.AppendLine(order.Purity ?? "空");
            sb.Append("[数量]:");
            sb.AppendLine(order.Quantity.ToString());
            sb.Append("[单位]:");
            sb.AppendLine(order.QuantityUnit ?? "空");
            sb.Append("[尺寸]:");
            sb.AppendLine(order.Dimension ?? "空");
            sb.Append("[尺寸细节]:");
            sb.AppendLine(order.DimensionDetails ?? "空");
            sb.Append("[客户样品]:");
            sb.AppendLine(order.SampleNeed ?? "空");
            sb.Append("[客户样品备注]:");
            sb.AppendLine(order.SampleNeedRemark ?? "空");
            sb.Append("[分析样品]:");
            sb.AppendLine(order.SampleForAnlysis ?? "空");
            sb.Append("[分析样品备注]:");
            sb.AppendLine(order.SampleForAnlysisRemark ?? "空");
            sb.Append("[最后期限]:");
            sb.AppendLine(order.DeadLine.ToString("yyyy-MM-dd"));
            sb.Append("[最低要求]:");
            sb.AppendLine(order.MinimumAcceptDefect ?? "空");
            sb.Append("[发货目的地]");
            sb.AppendLine(order.ShipTo ?? "空");
            sb.Append("[背板]:");
            sb.AppendLine(order.WithBackingPlate ?? "空");
            sb.Append("[背板图纸]:");
            sb.AppendLine(order.PlateDrawing ?? "空");
            sb.Append("[特殊要求]:");
            sb.AppendLine(order.SpecialRequirement ?? "空");
            sb.Append("[绑定要求]:");
            sb.AppendLine(order.BondingRequirement ?? "空");
            sb.Append("[PartNumber]:");
            sb.AppendLine(order.PartNumber ?? "空");
            sb.Append("[备注]:");
            sb.AppendLine(order.Remark ?? "空");
            sb.Append("[创建人]:");
            sb.AppendLine(order.Creator?? "空");
            sb.Append("[创建时间]:");
            sb.AppendLine(order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("[订单状态]:");
            sb.AppendLine(order.OrderState ?? "空");


            return sb.ToString();
        }
    }
}
