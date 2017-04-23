using PMSWCFService.ServiceContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using AutoMapper;
using PMSDAL;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IPlateService
    {
        public int AddPlate(DcPlate model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPlate, Plate>());
                    var product = Mapper.Map<Plate>(model);
                    dc.Plates.Add(product);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddPlateByUID(DcPlate model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddPlate(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeletePlate(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var product = dc.Plates.Find(id);
                    if (product != null)
                    {
                        dc.Plates.Remove(product);
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetPlateCount(string platelot, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.Plates
                                where p.PlateLot.Contains(platelot)
                                && p.Supplier.Contains(supplier)
                                && p.State != SimpleState.作废.ToString()
                                orderby p.CreateTime descending
                                select p;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcPlate> GetPlates(int skip, int take, string platelot, string supplier)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from p in dc.Plates
                                where p.PlateLot.Contains(platelot)
                                && p.Supplier.Contains(supplier)
                                && p.State != SimpleState.作废.ToString()
                                orderby p.CreateTime descending
                                select p;
                    Mapper.Initialize(cfg => cfg.CreateMap<Plate, DcPlate>());
                    var products = Mapper.Map<List<Plate>, List<DcPlate>>(query.Skip(skip).Take(take).ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdatePlate(DcPlate model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcPlate, Plate>());
                    var product = Mapper.Map<Plate>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdatePlateByUID(DcPlate model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdatePlate(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcPlate model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlate, PlateHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<PlateHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.PlateHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

    }
}