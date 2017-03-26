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
    public static class BDInstance
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
                var productTypes = Enum.GetNames(typeof(PMSCommon.OrderProductType));
                return productTypes;
            }
        }

        //From Services
        public static DcBDCustomer[] CustomerNames
        {
            get
            {
                var service = new CustomerServiceClient();
                var customerNames = service.GetCustomer();
                return customerNames;
            }
        }




    }
}
