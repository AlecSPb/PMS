using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace PMSClient
{
    public static class PMSOuputHelper
    {
        public static string GetPlanOutput(IList<DcPlanWithMisson> models)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            models.ToList().ForEach(o =>
            {
                #region 需要导出的数据列
                sb.Append(o.Plan.PlanDate.ToString("yyMMdd"));
                sb.Append(",");
                sb.Append(o.Plan.PlanLot);
                sb.Append(",");
                sb.Append(o.Plan.VHPDeviceCode);
                sb.Append(",");
                sb.Append(o.Plan.PlanType);
                sb.Append(",");
                sb.Append(o.Misson.CompositionStandard);
                sb.Append(",");
                sb.Append(o.Misson.PMINumber);
                sb.Append(",");
                sb.Append(o.Plan.MoldType);
                sb.Append(",");
                sb.Append(o.Plan.MoldDiameter);
                sb.Append(",");
                sb.Append(o.Plan.Thickness);
                sb.Append(",");
                sb.Append(o.Plan.Quantity);
                sb.Append(",");
                sb.Append(o.Plan.CalculationDensity);
                sb.Append(",");
                sb.Append(o.Plan.GrainSize);
                sb.Append(",");
                sb.Append(o.Plan.SingleWeight);
                sb.Append(",");
                sb.Append(o.Plan.AllWeight);
                sb.Append(",");
                sb.Append(o.Plan.RoomTemperature);
                sb.Append(",");
                sb.Append(o.Plan.RoomHumidity);
                sb.Append(",");
                sb.Append(o.Plan.PreTemperature);
                sb.Append(",");
                sb.Append(o.Plan.PrePressure);
                sb.Append(",");
                sb.Append(o.Plan.Temperature);
                sb.Append(",");
                sb.Append(o.Plan.Pressure);
                sb.Append(",");
                sb.Append(o.Plan.Vaccum);
                sb.Append(",");
                sb.Append(o.Plan.KeepTempTime);
                sb.Append(",");
                sb.Append(o.Plan.ProcessCode);
                sb.Append(",");
                sb.Append(o.Plan.MillingRequirement);
                sb.Append(",");
                sb.Append(o.Plan.FillingRequirement);
                sb.Append(",");
                sb.Append(o.Plan.MachineRequirement);
                sb.Append(",");
                sb.Append(o.Plan.SpecialRequirement);
                sb.Append(",");
                sb.Append(o.Plan.Creator);
                sb.Append(",");
                sb.Append(o.Plan.CreateTime.ToString());
                sb.AppendLine();
                #endregion
            });
            return sb.ToString();
        }
    }
}
