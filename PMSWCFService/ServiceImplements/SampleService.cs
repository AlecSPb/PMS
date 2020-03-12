using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;

namespace PMSWCFService
{
    public class SampleService : ISampleService
    {
        public void AddSample(DcSample model)
        {
            try
            {
                XS.Run();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcSample, Sample>());
                    var entity = Mapper.Map<Sample>(model);
                    db.Samples.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public List<DcSample> GetSampleAll(int s, int t, string productid, string composition)
        {
            try
            {
                XS.Run();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Sample, DcSample>());
                    var query = from m in db.Samples
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.ProductID.Contains(productid)
                                && m.Composition.Contains(composition)
                                orderby m.CreateTime descending
                                select m;
                    return Mapper.Map<List<Sample>, List<DcSample>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return new List<DcSample>();
            }
        }

        public int GetSampleAllCount(string productid, string composition)
        {
            try
            {
                XS.Run();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Samples
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.ProductID.Contains(productid)
                                && m.Composition.Contains(composition)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;
            }
        }

        public void UpdateSample(DcSample model)
        {
            try
            {
                XS.Run();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcSample, Sample>());
                    var entity = Mapper.Map<Sample>(model);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }
    }
}