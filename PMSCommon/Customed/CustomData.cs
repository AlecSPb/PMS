using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    public static class CustomData
    {
        public static List<string> MillingTime
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                data.Add("10minx2");
                data.Add("20minx2");
                data.Add("30minx2");
                data.Add("40minx2");
                #endregion
                return data;
            }
        }
        public static List<string> OrderSampleNeeds
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无需样品");
                data.Add("65gx1+15gx2");
                data.Add("65gX1,15gx3");
                data.Add("15gx2");
                data.Add("1cm大小x2");
                #endregion
                return data;
            }
        }

        public static List<string> Purity
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("3N");
                data.Add("4N");
                data.Add("5N");
                data.Add("6N");
                #endregion
                return data;
            }
        }
        public static List<string> GrainSize
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("-80");
                data.Add("-100");
                data.Add("-200");
                data.Add("-300");
                data.Add("-400");
                #endregion
                return data;
            }
        }
        public static List<string> VHPQuickMessage
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("粗抽开扩散凸高");
                data.Add("关门");
                data.Add("凸高");
                data.Add("粗抽");
                data.Add("开启旋片泵");
                data.Add("开启扩散泵加热");
                data.Add("开启罗茨泵");
                data.Add("转为扩散泵");
                data.Add("开始加温");
                data.Add("升温到");
                data.Add("开始加压");
                data.Add("结束加压");
                data.Add("结束加压");
                data.Add("关闭扩散泵");
                data.Add("关闭罗茨泵");
                data.Add("关闭旋片泵");
                data.Add("提压头");
                data.Add("充气");
                data.Add("开门");
                data.Add("取出模具");
                data.Add("结束");
                #endregion
                return data;
            }
        }
        public static List<double> MoldDiameter
        {
            get
            {
                var data = new List<double>();
                #region 数据
                data.Add(80);
                data.Add(125);
                data.Add(155);
                data.Add(205);
                data.Add(206);
                data.Add(233);
                data.Add(303);
                data.Add(455);
                #endregion
                return data;
            }
        }
        public static List<string> TestDefectsTypes
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无缺陷");
                data.Add("花纹");
                data.Add("斑点");
                data.Add("裂纹");
                data.Add("崩边");
                data.Add("缺口");
                data.Add("污物");
                data.Add("表面浅坑");
                data.Add("密度低");
                data.Add("花纹+斑点");
                data.Add("崩边+缺口");
                data.Add("崩边+划痕");
                data.Add("黑点");
                data.Add("臭味");
                data.Add("水渍");
                #endregion
                return data;
            }
        }

        public static List<string> MillingRequirement
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("+Sb2Te3");
                data.Add("+Na2S");
                data.Add("+KF");
                data.Add("+NaF");
                data.Add("注意防潮");
                data.Add("N气保护");
                data.Add("Ar气保护");
                #endregion
                return data;
            }
        }

        public static List<string> FillingRequirement
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("裸板");
                data.Add("BN");
                data.Add("BN+石墨纸");
                data.Add("BN+石墨纸+Al2O3纸");
                data.Add("BN+Al2O3纸");
                #endregion
                return data;
            }
        }

        public static List<string> MachineRequirement
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无需加工");
                data.Add("切割成");
                data.Add("表面粗糙度");
                data.Add("直径偏差");
                data.Add("厚度偏差");
                #endregion
                return data;
            }
        }

        public static List<string> SpecialRequirement
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                #endregion
                return data;
            }
        }
    }
}
