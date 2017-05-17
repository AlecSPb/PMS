using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml.Linq;
using PMSClient.SanjieService;

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

        private const string MaterialOrder = "原料订单";

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
            SetXMLCounter(MaterialOrder, CurrentCount[MaterialOrder]);
            Notices.Clear();
        }
        private static void CheckNotices()
        {
            Notices.Clear();
            CheckCounter(MaterialOrder);
        }
        private static void CheckCounter(string keyName)
        {
            int last = LastTimeCount[keyName];
            int current = CurrentCount[keyName];
            if (current > last)
            {
                Notices.Add($"有{current - last}个新{keyName}");
            }
        }
        private static void GetAllCurrentCount()
        {
            CurrentCount.Clear();
            using (var service = new SanjieServiceClient())
            {
                int counter = service.GetMaterialOrderCount("");
                CurrentCount.Add(MaterialOrder, counter);
            }


        }
        private static void GetAllLastTimeCount()
        {
            LastTimeCount.Clear();
            LastTimeCount.Add(MaterialOrder, GetXMLCount(MaterialOrder));
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
