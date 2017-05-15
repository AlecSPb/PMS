using PMSClient.SanjieService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.BasicService;

namespace PMSClient
{
    /// <summary>
    /// 生成ComboBox用的基本数据源
    /// 来自枚举
    /// 来自数据库
    /// 来自文件
    /// </summary>
    public static class PMSBasicDataService
    {
        #region 基本数据处理方法
        /// <summary>
        /// 复制源字符串列表到目标字符串列表
        /// </summary>
        /// <param name="source"></param>
        /// <param name="target"></param>
        public static void SetListDS<T>(List<T> source, List<T> target)
        {
            if (target != null)
            {
                target.Clear();
                source.ForEach(i => target.Add(i));
            }
        }
        public static void SetListDS(List<int> source, int count)
        {
            if (source != null)
            {
                source.Clear();
                for (int i = 0; i < count; i++)
                {
                    source.Add(i + 1);
                }
            }
        }
        public static void SetListDS<T>(List<string> ds)
        {
            if (ds != null)
            {
                ds.Clear();
                GetEnumNames<T>().ToList().ForEach(i => ds.Add(i));
            }
        }
        /// <summary>
        /// 传入基础对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="target"></param>
        /// <param name="transfer"></param>
        public static void SetListDS<T>(List<T> source, List<string> target, Func<T, string> transfer) where T : class
        {
            if (target != null)
            {
                target.Clear();
                source.ForEach(item => target.Add(transfer(item)));
            }
        }
        /// <summary>
        /// 获取枚举名称
        /// </summary>
        private static List<string> GetEnumNames<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }
        #endregion







        //From Enums
        public static string[] OrderStates
        {
            get
            {
                var states = Enum.GetNames(typeof(PMSCommon.OrderState));
                return states;
            }
        }
        public static string[] CommonStates
        {
            get
            {
                var states = Enum.GetNames(typeof(PMSCommon.CommonState));
                return states;
            }
        }
        public static string[] SimpleStates
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.SimpleState));
            }
        }
        public static string[] VHPPlanStates
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.VHPPlanState));
            }
        }
        public static string[] OrderPriorities
        {
            get
            {
                var states = Enum.GetNames(typeof(PMSCommon.OrderPriority));
                return states;
            }
        }
        public static string[] OrderPolicyTypes
        {
            get
            {
                var states = Enum.GetNames(typeof(PMSCommon.OrderPolicyType));
                return states;
            }
        }
        public static string[] TestTypes
        {
            get
            {
                var productTypes = Enum.GetNames(typeof(PMSCommon.TestType));
                return productTypes;
            }
        }
        public static string[] OrderProductTypes
        {
            get
            {
                var productTypes = Enum.GetNames(typeof(PMSCommon.OrderProductType));
                return productTypes;
            }
        }
        public static string[] PackNumbers
        {
            get
            {
                var numbers = new List<string>();
                for (int i = 0; i < 10; i++)
                {
                    numbers.Add((i + 1).ToString());
                }
                return numbers.ToArray();
            }
        }
        public static string[] ProductTypes
        {
            get
            {
                var productTypes = Enum.GetNames(typeof(PMSCommon.ProductType));
                return productTypes;
            }
        }
        public static string[] ProductStates
        {
            get
            {
                var productTypes = Enum.GetNames(typeof(PMSCommon.InventoryState));
                return productTypes;
            }
        }
        public static string[] Countries
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.CountryRegion));
            }
        }
        public static string[] PackageTypes
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.PackageType));
            }
        }
        public static string[] GoodPositions
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.GoodPosition));
            }
        }

        public static string[] QuickVHPMessages
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.QuickVHPMessege));
            }
        }
        public static string[] OrderUnits
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.OrderUnit));
            }
        }
        public static string[] MillingTimes
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.MillingTime));
            }
        }
        public static string[] MillingTools
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.MillingTool));
            }
        }
        public static string[] MillingGases
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.MillingGas));
            }
        }

        //From Services
        public static DcBDCustomer[] Customers
        {
            get
            {
                using (var service = new CustomerServiceClient())
                {
                    return service.GetCustomer().OrderBy(i => i.CustomerName).ToArray();
                }
            }
        }
        public static DcBDCompound[] Compounds
        {
            get
            {
                using (var service = new CompoundServiceClient())
                {
                    return service.GetAllCompounds().OrderBy(i => i.MaterialName).ToArray();
                }
            }
        }
        public static DcBDVHPDevice[] VHPDevices
        {
            get
            {
                using (var service = new VHPDeviceServiceClient())
                {
                    return service.GetVHPDevice().OrderBy(i => i.CodeName).ToArray();
                }
            }
        }
        public static DcBDVHPProcess[] VHPProcesses
        {
            get
            {
                using (var service = new VHPProcessServiceClient())
                {
                    return service.GetVHPProcess().OrderBy(i => i.CodeName).ToArray();
                }
            }
        }
        public static DcBDVHPMold[] VHPMolds
        {
            get
            {
                using (var service = new VHPMoldServiceClient())
                {
                    return service.GetVHPMold().OrderBy(i => i.InnerDiameter).ToArray();
                }
            }
        }




    }
}
