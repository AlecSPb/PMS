using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSCommon
{
    public static class CustomData
    {
        public static List<string> FailureStage
        {
            get
            {
                var data = new List<string>
                {
                    "熔炼",
                    "制粉",
                    "热压",
                    "加工",
                    "绑定",
                    "测试",
                    "入库",
                    "发货"
                };
                return data;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public static List<String> MaterialTypes
        {
            get
            {
                var data = new List<string>
                {
                    "新料",
                    "回收料",
                    "陈旧料",
                    "外来料",
                    "其他"
                };
                return data;
            }
        }

        public static List<String> InformationSources
        {
            get
            {
                var data = new List<string>();
                data.Add("维基百科");
                data.Add("三杰");
                data.Add("资料书");
                data.Add("其他");
                return data;
            }
        }
        public static List<string> MillingTime
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                data.Add("x");
                data.Add("10x");
                data.Add("20x");
                data.Add("30x");
                data.Add("40x");
                data.Add("50x");
                data.Add("60x");
                data.Add("70x");
                data.Add("80x");
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
                data.Add("x");
                data.Add("65gx1+15gx2");
                data.Add("65gx1+15gx3");
                data.Add("15gx2");
                data.Add("15gx3");
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
                data.Add("-325");
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
                data.Add("粗抽凸高");
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
                data.Add(105);
                data.Add(128);
                data.Add(155);
                data.Add(168);
                data.Add(205);
                data.Add(206);
                data.Add(233);
                data.Add(255);
                data.Add(303);
                data.Add(319);
                data.Add(336);
                data.Add(450);
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
                data.Add("+Sb2Te3-");
                data.Add("+Na2S-");
                data.Add("+KF-");
                data.Add("+NaF-");
                data.Add("+RbF-");
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
                data.Add("SiC隔板");
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
                data.Add("表面粗糙度Ra");
                data.Add("直径偏差");
                data.Add("厚度偏差");
                data.Add("参考加工图纸");
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

        public static List<string> TargetDimension
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                data.Add("50.8mm OD x3.175mm");
                data.Add("50.8mm OD x4.317mm");
                data.Add("76.2mm OD x4mm");
                data.Add("124.5mm OD x3mm");
                data.Add("124.5mm OD x4mm");
                data.Add("230mm OD x 4mm");
                #endregion
                return data;
            }
        }

        public static List<string> PlateDimension
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                data.Add("50.8mm OD x 2mm");
                data.Add("50.8mm OD x 3.175mm");
                data.Add("76.2mm OD x 2mm");
                data.Add("237mm OD x 11mm");
                data.Add("158mm OD x 11mm");
                data.Add("82.855mm OD x 3.048mm for 50.8mm");
                data.Add("160mm OD x 5mm for 124.5mm");
                data.Add("132mm OD for Sony101");
                #endregion
                return data;
            }
        }

        public static List<string> PlateSupplier
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("广汉");
                data.Add("西安");
                data.Add("瑞典");
                data.Add("美国");
                data.Add("台湾");
                data.Add("日本");
                data.Add("其他");
                #endregion
                return data;
            }
        }

        public static List<string> PlateLastWeldMaterial
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                data.Add("铟");
                data.Add("锡");
                data.Add("导电胶");
                #endregion
                return data;
            }
        }

        public static List<string> PlateDefect
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("无");
                data.Add("生锈");
                data.Add("磕碰");
                data.Add("翘曲");
                #endregion
                return data;
            }
        }

        public static List<string> BondingDefects
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("裂缝");
                data.Add("花纹");
                data.Add("小缺口");
                data.Add("大缺口");
                data.Add("分层");
                data.Add("未焊合");
                #endregion
                return data;
            }
        }

        public static List<string> PlateTypes
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("新背板230");
                data.Add("旧背板230");
                data.Add("钼背板230");
                data.Add("镍背板230");
                data.Add("小背板50.8");
                data.Add("小背板50");
                data.Add("小背板76.2");
                data.Add("小背板186");

                #endregion
                return data;
            }
        }

        public static List<string> ProcessCode
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("F1");
                data.Add("W1");
                data.Add("W2");
                data.Add("W3");
                data.Add("W4");
                data.Add("KaoLiao");
                data.Add("UnKnown");
                #endregion
                return data;
            }
        }
        public static List<string> VHPDevice
        {
            get
            {
                var data = new List<string>();
                #region 数据
                data.Add("A");
                data.Add("B");
                data.Add("C");
                data.Add("D");
                data.Add("E");
                data.Add("F");
                data.Add("G");
                data.Add("H");
                data.Add("I");
                data.Add("J");
                data.Add("K");
                data.Add("L");
                data.Add("M");
                data.Add("N");
                data.Add("O");
                data.Add("P");
                data.Add("Q");
                data.Add("R");
                data.Add("S");
                data.Add("T");
                data.Add("U");
                data.Add("V");
                data.Add("W");
                data.Add("X");
                data.Add("Y");
                data.Add("Z");

                data.Add("HIP");
                data.Add("UK");
                #endregion
                return data;
            }
        }
    }
}
