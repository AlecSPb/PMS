using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;

namespace PMSWCFService
{
    public partial class PMSService : IOutSourceService
    {
        public int AddOutSource(DcOutSource model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOutSource, OutSource>());
                    var entity = Mapper.Map<OutSource>(model);
                    dc.OutSources.Add(entity);
                    SaveHistory(model, uid);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public int DeleteOutSource(Guid id, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.OutSources.Find(id);
                    dc.OutSources.Remove(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public List<DcOutSource> GetOutSources(int s, int t, string orderlot, string ordername, string supplier)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<OutSource, DcOutSource>());
                    var query = from i in dc.OutSources
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                && i.OrderLot.Contains(orderlot)
                                && i.OrderName.Contains(ordername)
                                && i.Supplier.Contains(supplier)
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<OutSource>, List<DcOutSource>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public int GetOutSourcesCount(string orderlot, string ordername, string supplier)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.OutSources
                                where i.State != PMSCommon.OrderState.作废.ToString()
                                && i.OrderLot.Contains(orderlot)
                                && i.OrderName.Contains(ordername)
                                && i.Supplier.Contains(supplier)
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }

        public int UpdateOutSource(DcOutSource model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcOutSource, OutSource>());
                    var entity = Mapper.Map<OutSource>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    SaveHistory(model, uid);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw;
            }
        }



        private void SaveHistory(DcOutSource model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcOutSource, OutSourceHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<OutSourceHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.OutSourceHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }
    }
}