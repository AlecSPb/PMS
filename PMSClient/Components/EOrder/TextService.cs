using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.EOrder
{
    public class TextService
    {
        public static string GetEnglishOrderText(Order order)
        {
            if (order == null) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("[GUID]:");
            sb.AppendLine(order.GUIDID.ToString());
            sb.Append("[CustomerName]:");
            sb.AppendLine(order.CustomerName ?? "");
            sb.Append("[PO]:");
            sb.AppendLine(order.PO ?? "");
            sb.Append("[Composition]:");
            sb.AppendLine(order.Composition ?? "");
            sb.Append("[CompositionDetail]:");
            sb.AppendLine(order.CompositionDetail ?? "");
            sb.Append("[ProductType]:");
            sb.AppendLine(order.ProductType ?? "");
            sb.Append("[Purity]:");
            sb.AppendLine(order.Purity ?? "");
            sb.Append("[Quantity]:");
            sb.AppendLine(order.Quantity.ToString());
            sb.Append("[QuantityUnit]:");
            sb.AppendLine(order.QuantityUnit ?? "");
            sb.Append("[Dimension]:");
            sb.AppendLine(order.DimensionDetails ?? "");
            sb.Append("[Drawing]:");
            sb.AppendLine(order.SampleNeed);
            sb.Append("[SampleNeedRemark]:");
            sb.AppendLine(order.SampleNeedRemark ?? "");
            sb.Append("[SampleForAnlysis]:");
            sb.AppendLine(order.SampleForAnlysis ?? "");
            sb.Append("[SampleForAnlysisRemark]:");
            sb.AppendLine(order.SampleForAnlysisRemark ?? "");
            sb.Append("[DeadLine]:");
            sb.AppendLine(order.DeadLine.ToString("yyyy-MM-dd"));
            sb.Append("[MinimumAcceptDefect]:");
            sb.AppendLine(order.MinimumAcceptDefect ?? "");
            sb.Append("[ShipTo]");
            sb.AppendLine(order.ShipTo ?? "");
            sb.Append("[WithBackingPlate]:");
            sb.AppendLine(order.WithBackingPlate ?? "");
            sb.Append("[PlateDrawing]:");
            sb.AppendLine(order.PlateDrawing ?? "");
            sb.Append("[SpecialRequirement]:");
            sb.AppendLine(order.SpecialRequirement ?? "");
            sb.Append("[BondingRequirement]:");
            sb.AppendLine(order.BondingRequirement ?? "");
            sb.Append("[PartNumber]:");
            sb.AppendLine(order.PartNumber ?? "");
            sb.Append("[Remark]:");
            sb.AppendLine(order.Remark ?? "");
            sb.Append("[Creator]:");
            sb.AppendLine(order.Creator ?? "");
            sb.Append("[CreateTime]:");
            sb.AppendLine(order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("[OrderState]:");
            sb.AppendLine(order.OrderState ?? "");

            sb.AppendLine("==============================================");
            return sb.ToString();
        }
        public static string GetChineseOrderText(Order order)
        {
            if (order == null) return "";
            StringBuilder sb = new StringBuilder();
            sb.Append("[GUID]:");
            sb.AppendLine(order.GUIDID.ToString());
            sb.Append("[客户名称]:");
            sb.AppendLine(order.CustomerName ?? "");
            sb.Append("[PO]:");
            sb.AppendLine(order.PO ?? "");
            sb.Append("[成分]:");
            sb.AppendLine(order.Composition ?? "");
            sb.Append("[成分细节]:");
            sb.AppendLine(order.CompositionDetail ?? "");
            sb.Append("[产品类型]:");
            sb.AppendLine(order.ProductType ?? "");
            sb.Append("[纯度]:");
            sb.AppendLine(order.Purity ?? "");
            sb.Append("[数量]:");
            sb.AppendLine(order.Quantity.ToString());
            sb.Append("[单位]:");
            sb.AppendLine(order.QuantityUnit ?? "");
            sb.Append("[尺寸]:");
            sb.AppendLine(order.Dimension ?? "");
            sb.Append("[尺寸细节]:");
            sb.AppendLine(order.DimensionDetails ?? "");
            sb.Append("[客户样品]:");
            sb.AppendLine(order.SampleNeed ?? "");
            sb.Append("[客户样品备注]:");
            sb.AppendLine(order.SampleNeedRemark ?? "");
            sb.Append("[分析样品]:");
            sb.AppendLine(order.SampleForAnlysis ?? "");
            sb.Append("[分析样品备注]:");
            sb.AppendLine(order.SampleForAnlysisRemark ?? "");
            sb.Append("[最后期限]:");
            sb.AppendLine(order.DeadLine.ToString("yyyy-MM-dd"));
            sb.Append("[最低要求]:");
            sb.AppendLine(order.MinimumAcceptDefect ?? "");
            sb.Append("[发货目的地]");
            sb.AppendLine(order.ShipTo ?? "");
            sb.Append("[背板]:");
            sb.AppendLine(order.WithBackingPlate ?? "");
            sb.Append("[背板图纸]:");
            sb.AppendLine(order.PlateDrawing ?? "");
            sb.Append("[特殊要求]:");
            sb.AppendLine(order.SpecialRequirement ?? "");
            sb.Append("[绑定要求]:");
            sb.AppendLine(order.BondingRequirement ?? "");
            sb.Append("[PartNumber]:");
            sb.AppendLine(order.PartNumber ?? "");
            sb.Append("[备注]:");
            sb.AppendLine(order.Remark ?? "");
            sb.Append("[创建人]:");
            sb.AppendLine(order.Creator?? "");
            sb.Append("[创建时间]:");
            sb.AppendLine(order.CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            sb.Append("[订单状态]:");
            sb.AppendLine(order.OrderState ?? "");

            sb.AppendLine("==============================================");

            return sb.ToString();
        }
    }
}
