using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using System.Timers;
using PMSClient.CustomControls;
using System.IO;
using System.Xml.Linq;

namespace PMSClient
{
    /// <summary>
    /// 用到的各类通知
    /// </summary>
    public static class PMSNotice
    {
        static PMSNotice()
        {
            Notices = new List<string>();
            CurrentCount = new Dictionary<string, int>();
            LastTimeCount = new Dictionary<string, int>();
        }

        private static Dictionary<string, int> CurrentCount;
        private static Dictionary<string, int> LastTimeCount;
        public static List<string> Notices { get; set; }

        private const string Order = "销售订单";
        private const string MaterialOrder = "原料订单";
        private const string Plan = "生产计划";
        private const string Delivery = "发货计划";

        public static bool HasNewNotice
        {
            get
            {
                return Notices.Count > 0;
            }
        }
        public static void CheckIt()
        {
            try
            {
                GetAllCurrentCount();
                GetAllLastTimeCount();
                CheckNotices();
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
        public static void SaveCurrentCount()
        {
            SetXMLCounter(Order, CurrentCount[Order]);
            SetXMLCounter(MaterialOrder, CurrentCount[MaterialOrder]);
            SetXMLCounter(Plan, CurrentCount[Plan]);
            SetXMLCounter(Delivery, CurrentCount[Delivery]);
            Notices.Clear();
        }
        private static void CheckNotices()
        {
            Notices.Clear();
            CheckCounter(Order);
            CheckCounter(MaterialOrder);
            CheckCounter(Plan);
            CheckCounter(Delivery);
        }
        private static void CheckCounter(string keyName)
        {
            int last = LastTimeCount[keyName];
            int current = CurrentCount[keyName];
            if (current > last)
            {
                if (PMSHelper.Language == "zh-cn")
                {
                    Notices.Add($"有{current - last}个新{keyName}");
                }
                else
                {
                    Notices.Add($"There are  {current - last} New {keyName}");
                }
            }
        }
        private static void GetAllCurrentCount()
        {
            CurrentCount.Clear();
            using (var service = new OrderServiceClient())
            {
                int counter = service.GetOrderCountBySearch(string.Empty, string.Empty);
                CurrentCount.Add(Order, counter);
            }

            using (var service = new MaterialOrderServiceClient())
            {
                int counter = service.GetMaterialOrderCountBySearch("", "");
                CurrentCount.Add(MaterialOrder, counter);
            }

            using (var service = new MissonServiceClient())
            {
                int counter = service.GetPlanExtraCount("", "");
                CurrentCount.Add(Plan, counter);
            }

            using (var service = new DeliveryServiceClient())
            {
                int counter = service.GetDeliveryCountBySearch("");
                CurrentCount.Add(Delivery, counter);
            }

        }
        private static void GetAllLastTimeCount()
        {
            LastTimeCount.Clear();
            LastTimeCount.Add(Order, GetXMLCount(Order));
            LastTimeCount.Add(MaterialOrder, GetXMLCount(MaterialOrder));
            LastTimeCount.Add(Plan, GetXMLCount(Plan));
            LastTimeCount.Add(Delivery, GetXMLCount(Delivery));
        }

        private static string xmlPath = Path.Combine(Environment.CurrentDirectory, "Resource", "NoticeData.xml");
        private static int GetXMLCount(string keyName)
        {
            try
            {
                XElement xe = XElement.Load(xmlPath);
                var query = from i in xe.Elements("Counter")
                            where i.Attribute("Name").Value == keyName
                            select i;
                return int.Parse(query.FirstOrDefault().Element("LastTimeValue").Value);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
                return -1;
            }
        }
        private static void SetXMLCounter(string keyName, int value)
        {
            try
            {
                XElement xe = XElement.Load(xmlPath);
                var query = from i in xe.Elements("Counter")
                            where i.Attribute("Name").Value == keyName
                            select i;
                if (query.Count() > 0)
                {
                    query.First().SetElementValue("LastTimeValue", value);
                }
                xe.Save(xmlPath);
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
