using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{

    public class PMSSettingService : IPMSSettingService
    {
        public void AddSettings(string key, string value)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    db.PMSSettings.Add(new PMSSetting() { Key = key, Value = value });
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public string GetValueByKey(string key)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from s in db.PMSSettings
                                where s.Key == key
                                select s;

                    return query.FirstOrDefault().Value;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return "";
            }
        }

        public void UpdateSettings(string key, string newValue)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var entity = db.PMSSettings.Where(i => i.Key == key).FirstOrDefault();
                    if (db != null)
                    {
                        entity.Value = newValue;
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }
    }
}