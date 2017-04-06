using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;

namespace PMSWCFService
{
    public partial class PMSService : IMaterialInventoryService
    {
        public int AddMaterialInventoryIn(DcMaterialInventoryIn model)
        {
            try
            {
                int result = 0;
                using (var dc=new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryIn, PMSMaterialInventoryIn>());
                    var item = Mapper.Map<PMSMaterialInventoryIn>(model);
                    dc.MaterialInventoryIns.Add(item);
                    result = dc.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialInventoryOut(DcMaterialInventoryOut model)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryOut, PMSMaterialInventoryOut>());
                    var item = Mapper.Map<PMSMaterialInventoryOut>(model);
                    dc.MaterialInventoryOuts.Add(item);
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteMaterialInventoryIn(Guid id)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    var item = dc.MaterialInventoryIns.Find(id);
                    dc.MaterialInventoryIns.Remove(item);
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteMaterialInventoryOut(Guid id)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    var item = dc.MaterialInventoryOuts.Find(id);
                    dc.MaterialInventoryOuts.Remove(item);
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSMaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.CommonState.Deleted.ToString()
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<PMSMaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
                }

            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<PMSMaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.CommonState.Deleted.ToString()
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<PMSMaterialInventoryOut>, List<DcMaterialInventoryOut>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialInventoryIn(DcMaterialInventoryIn model)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryIn, PMSMaterialInventoryIn>());
                    var item = Mapper.Map<PMSMaterialInventoryIn>(model);
                    dc.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialInventoryOut(DcMaterialInventoryOut model)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryOut, PMSMaterialInventoryOut>());
                    var item = Mapper.Map<PMSMaterialInventoryOut>(model);
                    dc.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
    }
}