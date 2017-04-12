using PMSClient.MainService;
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
    /// 来自枚举，来自数据库，来自文件
    /// </summary>
    public static class BasicData
    {
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
        public static string[] ProductTypes
        {
            get
            {
                var productTypes = Enum.GetNames(typeof(PMSCommon.ProductType));
                return productTypes;
            }
        }
        public static string[] Countries
        {
            get
            {
                return Enum.GetNames(typeof(PMSCommon.Country));
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
        //From Services
        public static DcBDCustomer[] Customers
        {
            get
            {
                var service = new CustomerServiceClient();
                var customers = service.GetCustomer().OrderBy(i => i.CustomerName).ToArray();
                return customers;
            }
        }




    }
}
