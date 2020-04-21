using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSWCFService.ServiceImplements.Helpers;

namespace PMSWCFService
{
    public class SimpleMaterialService : ISimpleMaterialService
    {
        public void AddSimpleMaterial(DcSimpleMaterial model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcSimpleMaterial, SimpleMaterial>());
                    var entity = Mapper.Map<SimpleMaterial>(model);
                    dc.SimpleMaterials.Add(entity);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcSimpleMaterial> GetSimpleMaterial(int s, int t, string composition)
        {
            try
            {

                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<SimpleMaterial, DcSimpleMaterial>());
                    var query = from i in dc.SimpleMaterials
                                where i.State == PMSCommon.ToolState.正常.ToString()
                                && i.ElementName.Contains(composition)
                                orderby i.ElementName ascending
                                select i;
                    return Mapper.Map<List<SimpleMaterial>, List<DcSimpleMaterial>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetSimpleMaterialCount(string composition)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.SimpleMaterials
                                where i.State == PMSCommon.ToolState.正常.ToString()
                                && i.ElementName.Contains(composition)
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcSimpleMaterial GetSimpleMaterialByComposition(string composition)
        {
            try
            {

                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<SimpleMaterial, DcSimpleMaterial>());
                    var query = from i in dc.SimpleMaterials
                                where i.State == PMSCommon.ToolState.正常.ToString()
                                && i.ElementName == composition
                                select i;
                    return Mapper.Map<SimpleMaterial, DcSimpleMaterial>(query.FirstOrDefault());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateSimpleMaterial(DcSimpleMaterial model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcSimpleMaterial, SimpleMaterial>());
                    var entity = Mapper.Map<SimpleMaterial>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}