using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;

namespace UsefulPackage
{
    public static class PMSTranslate
    {

        public static string PMINumber()
        {
            return $"{DateTime.Now.ToString("yyMMdd")}-A";
        }

        public static string PlateLot()
        {
            return $"{DateTime.Now.ToString("yyMMdd")}";
        }

        /// <summary>
        /// 生成当日热压靶材的Lot号码
        /// </summary>
        public static string PlanLot()
        {
            return DateTime.Now.ToString("yyMMdd") + "-M-1";
        }
        /// <summary>
        /// 利用传递日期和代码来构造
        /// </summary>
        /// <param name="date"></param>
        /// <param name="devicecode"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string PlanLot(DateTime date, string devicecode, string suffix)
        {
            string dateStr = date.ToString("yyMMdd");
            string deviceStr = devicecode;
            return $"{dateStr}-{deviceStr}-{suffix}";
        }
        /// <summary>
        /// 生成ABCD代码
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="suffix"></param>
        /// <returns></returns>
        public static string PlanLot(DcPlanWithMisson plan, string suffix)
        {
            if (plan == null)
            {
                return PlanLot();
            }
            string dateStr = plan.Plan.PlanDate.ToString("yyMMdd");
            string deviceStr = plan.Plan.VHPDeviceCode;
            return $"{dateStr}-{deviceStr}-{suffix}";
        }
        /// <summary>
        /// 翻译成MNO代码
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        public static string PlanLot(DcPlanWithMisson plan)
        {
            if (plan == null)
            {
                return PlanLot();
            }

            string deviceStr = plan.Plan.VHPDeviceCode;
            return PlanLot(plan, "1", code =>
            {
                string CodeName = "";
                switch (code)
                {
                    case "A":
                        CodeName = "M";
                        break;
                    case "B":
                        CodeName = "N";
                        break;
                    case "C":
                        CodeName = "O";
                        break;
                    case "D":
                        CodeName = "D";
                        break;
                    default:
                        CodeName = "A";
                        break;
                }
                return CodeName;
            });
        }
        /// <summary>
        /// 带有设备代码转换委托
        /// </summary>
        /// <param name="plan"></param>
        /// <param name="suffix"></param>
        /// <param name="DeviceCodeTransfer"></param>
        /// <returns></returns>
        public static string PlanLot(DcPlanWithMisson plan, string suffix, Func<string, string> DeviceCodeTransfer)
        {
            if (plan == null)
            {
                return PlanLot();
            }
            string dateStr = plan.Plan.PlanDate.ToString("yyMMdd");
            string deviceStr = DeviceCodeTransfer(plan.Plan.VHPDeviceCode);
            return $"{dateStr}-{deviceStr}-{suffix}";
        }



        public static double Average(double w1,double w2,double w3=0,double w4=0)
        {
            double sum = 0;
            double count = 0;
            if (w1>0)
            {
                sum += w1;
                count++;
            }
            if (w2> 0)
            {
                sum += w2;
                count++;
            }
            if (w3 > 0)
            {
                sum += w3;
                count++;
            }
            if (w4 > 0)
            {
                sum += w4;
                count++;
            }
            if (count==0)
            {
                count = 1;
            }
            return sum / count;
        }
        /// <summary>
        ///输入取模数据，得到毛坯尺寸
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Dimension(DcRecordDeMold model)
        {
            double diameter = Average(model.Diameter1, model.Diameter2);
            double thickness = Average(model.Thickness1, model.Thickness2, model.Thickness3, model.Thickness4);
            return $"{diameter.ToString("F2")}mm ODx {thickness.ToString("F2")}mm";
        }
        /// <summary>
        ///输入加工数据，得到靶材尺寸
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string Dimension(DcRecordMachine model)
        {
            double diameter = Average(model.Diameter1, model.Diameter2);
            double thickness = Average(model.Thickness1, model.Thickness2, model.Thickness3, model.Thickness4);
            return $"{diameter.ToString("F2")}mm ODx {thickness.ToString("F2")}mm";
        }
    }
}
