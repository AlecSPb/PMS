using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using PMSDAL;

namespace PMSWCFService
{
    public class AnlysisHelper
    {
        public static string CheckRecordDemold(string searchCode)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new PMSDbContext())
            {
                var query = from m in db.RecordDeMolds
                            where m.State != PMSCommon.CommonState.作废.ToString()
                            && m.VHPPlanLot.Contains(searchCode)
                            orderby m.VHPPlanLot
                            select m;
                foreach (var item in query)
                {
                    sb.Append(item.VHPPlanLot);
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        public static string CheckRecordMachine(string searchCode)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new PMSDbContext())
            {
                var query = from m in db.RecordMachines
                            where m.State != PMSCommon.CommonState.作废.ToString()
                            && m.VHPPlanLot.Contains(searchCode)
                            orderby m.VHPPlanLot
                            select m;
                foreach (var item in query)
                {
                    sb.Append(item.VHPPlanLot);
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        public static string CheckRecordTest(string searchCode)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new PMSDbContext())
            {
                var query = from m in db.RecordTests
                            where m.State != PMSCommon.CommonState.作废.ToString()
                            && m.ProductID.Contains(searchCode)
                            orderby m.ProductID
                            select m;
                foreach (var item in query)
                {
                    sb.Append(item.ProductID);
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        public static string CheckRecordBonding(string searchCode)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new PMSDbContext())
            {
                var query = from m in db.RecordBondings
                            where m.State != PMSCommon.CommonState.作废.ToString()
                            && m.TargetProductID.Contains(searchCode)
                            orderby m.TargetProductID
                            select m;

                foreach (var item in query)
                {
                    sb.Append(item.TargetProductID);
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        public static string CheckDelivery(string searchCode)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new PMSDbContext())
            {
                var query = from m in db.DeliveryItems
                            where m.State != PMSCommon.CommonState.作废.ToString()
                            && m.ProductType == PMSCommon.ProductType.靶材.ToString()
                            && m.ProductID.Contains(searchCode)
                            && !m.ProductID.Contains("BP")
                            && !m.ProductID.Contains("OS")
                            && !m.ProductID.Contains("GN")
                            && !m.ProductID.Contains("Z")
                            && !m.ProductID.Contains("PD")
                            orderby m.ProductID
                            select m;
                foreach (var item in query)
                {
                    sb.Append(item.ProductID);
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }

        public static string CheckFailure(string searchCode)
        {
            StringBuilder sb = new StringBuilder();
            using (var db = new PMSDbContext())
            {
                var query = from m in db.Failures
                            where m.State != PMSCommon.CommonState.作废.ToString()
                            && m.ProductID.Contains(searchCode)
                            orderby m.ProductID
                            select m;
                foreach (var item in query)
                {
                    sb.Append(item.ProductID);
                    sb.Append(";");
                }
            }

            return sb.ToString();
        }
    }
}