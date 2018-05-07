using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;
using System.Data.Entity;
using PMSWCFService.DataContracts.Model;

namespace PMSWCFService
{
    public partial class PMSService : IMaterialInventoryService
    {
        public int AddMaterialInventoryIn(DcMaterialInventoryIn model)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryIn, MaterialInventoryIn>());
                    var item = Mapper.Map<MaterialInventoryIn>(model);
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
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryOut, MaterialInventoryOut>());
                    var item = Mapper.Map<MaterialInventoryOut>(model);
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

        public int GetMaterialInventoryInCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.MaterialInventoryIns.Where(o => o.State != PMSCommon.InventoryState.作废.ToString()).Count();
                }
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
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
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
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryOut>, List<DcMaterialInventoryOut>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.MaterialInventoryOuts.Where(o => o.State != PMSCommon.SimpleState.作废.ToString()).Count();
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
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryIn, MaterialInventoryIn>());
                    var item = Mapper.Map<MaterialInventoryIn>(model);
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
                    Mapper.Initialize(cfg => cfg.CreateMap<DcMaterialInventoryOut, MaterialInventoryOut>());
                    var item = Mapper.Map<MaterialInventoryOut>(model);
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

        public List<DcMaterialInventoryIn> GetMaterialInventoryInsBySearch(int skip, int take, string supplier, string composition, string batchnumber, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && o.Supplier.Contains(supplier)
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryInCountBySearch(string supplier, string composition, string batchnumber, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && o.Supplier.Contains(supplier)
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialInventoryInByUID(DcMaterialInventoryIn model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddMaterialInventoryIn(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialInventoryInByUID(DcMaterialInventoryIn model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateMaterialInventoryIn(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOutsBySearch(int skip, int take, string receiver, string composition, string batchnumber, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && o.Receiver.Contains(receiver)
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryOut>, List<DcMaterialInventoryOut>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCountBySearch(string receiver, string composition, string batchnumber, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && o.Receiver.Contains(receiver)
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddMaterialInventoryOutByUID(DcMaterialInventoryOut model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddMaterialInventoryOut(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateMaterialInventoryOutByUID(DcMaterialInventoryOut model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateMaterialInventoryOut(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcMaterialInventoryIn model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialInventoryIn, MaterialInventoryInHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<MaterialInventoryInHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.MaterialInventoryInHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        private void SaveHistory(DcMaterialInventoryOut model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcMaterialInventoryOut, MaterialInventoryOutHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<MaterialInventoryOutHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.MaterialInventoryOutHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryInByYear(int skip, int take, int year)
        {
            try
            {
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                 && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryInCountByYear(int year)
        {
            try
            {
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.MaterialInventoryIns
                                where o.State != PMSCommon.InventoryState.作废.ToString()
                                && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOutsByYear(int skip, int take, int year)
        {
            try
            {
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return Mapper.Map<List<MaterialInventoryOut>, List<DcMaterialInventoryOut>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryOutCountByYear(int year)
        {
            try
            {
                var date = new DateTime(year, 1, 1);
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>());

                    var query = from o in dc.MaterialInventoryOuts
                                where o.State != PMSCommon.SimpleState.作废.ToString()
                                && DbFunctions.DiffYears(o.CreateTime, date) == 0
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryInUnCompleted(int skip, int take, string supplier, string composition, string batchnumber, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>());

                    var query = from o in dc.MaterialInventoryIns
                                where o.State == PMSCommon.InventoryState.库存.ToString()
                                && o.Supplier.Contains(supplier)
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime
                                select o;
                    return Mapper.Map<List<MaterialInventoryIn>, List<DcMaterialInventoryIn>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetMaterialInventoryInCountUnCompleted(string supplier, string composition, string batchnumber, string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from o in dc.MaterialInventoryIns
                                where o.State == PMSCommon.InventoryState.库存.ToString()
                                && o.Supplier.Contains(supplier)
                                && o.Composition.Contains(composition)
                                && o.MaterialLot.Contains(batchnumber)
                                && o.PMINumber.Contains(pminumber)
                                orderby o.CreateTime descending
                                select o;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int CheckMaterialIn(string pmiNumber)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    result = dc.MaterialInventoryIns.Where(i => i.PMINumber == pmiNumber
                    && i.State != PMSCommon.InventoryState.作废.ToString()).Count();
                }
                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int CheckMaterialOut(string pmiNumber)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    result = dc.MaterialInventoryOuts.Where(i => i.PMINumber == pmiNumber
                    && i.State != PMSCommon.InventoryState.作废.ToString()).Count();
                }
                return result;
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }


        public List<PMSReadyOutMaterialModel> GetReadyOutMaterialList(int take)
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<List<PMSReadyOutMaterialModelRaw>, List<PMSReadyOutMaterialModel>>();
                    cfg.CreateMap<MaterialInventoryIn, DcMaterialInventoryIn>();
                    cfg.CreateMap<MaterialInventoryOut, DcMaterialInventoryOut>();
                    cfg.CreateMap<RecordMilling, DcRecordMilling>();
                });

                var rmList = dc.RecordMillings.Where(i => i.MaterialType == PMSCommon.CustomData.MaterialTypes[0])
                  .OrderByDescending(i => i.CreateTime).Take(take);
                var result = from rm in rmList
                             join mi in dc.MaterialInventoryIns
                             on rm.PMINumber equals mi.PMINumber
                             join mo in dc.MaterialInventoryOuts
                             on rm.PMINumber equals mo.PMINumber
                             select new PMSReadyOutMaterialModelRaw
                             {
                                 RecordMillingModel = rm,
                                 MaterialInModel = mi,
                                 MaterialOutModel = mo
                             };

                return Mapper.Map<List<PMSReadyOutMaterialModelRaw>,
                    List<PMSReadyOutMaterialModel>>(result.ToList());
            }
        }
    }
}