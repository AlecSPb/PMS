using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEOrder.Model;

namespace PMSEOrder.Service
{
    public class TextService
    {
        public static string GetOrderText(Order order)
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
            sb.AppendLine(order.OrderState??"");


            return sb.ToString();
        }
    }
}
