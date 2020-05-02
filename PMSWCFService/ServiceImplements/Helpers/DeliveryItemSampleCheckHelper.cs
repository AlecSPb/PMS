using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;

namespace PMSWCFService.ServiceImplements.Helpers
{
    public class DeliveryItemSampleCheckHelper
    {

        public static RecordTest GetPMINumber(string productid)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var query = from t in db.RecordTests
                                where t.ProductID == productid
                                select t;
                    return query.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        public static List<Sample> CheckSample(string pminumber)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var query = from t in db.Samples
                                where t.PMINumber == pminumber
                                select t;
                    return query.ToList();
                }
            }
            catch (Exception)
            {
                return new List<Sample>();
            }
        }

    }
}