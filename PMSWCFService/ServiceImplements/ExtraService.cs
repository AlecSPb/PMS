using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;

namespace PMSWCFService
{
    public class ExtraService : ICheckListService, IItemDebitService
    {
        public int AddCheckList(DcCheckList model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcCheckList, CheckList>());
                    var entity = Mapper.Map<CheckList>(model);
                    SaveHistory(model, uid);
                    dc.CheckLists.Add(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int AddItemDebit(DcItemDebit model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcItemDebit, ItemDebit>());
                    var entity = Mapper.Map<ItemDebit>(model);
                    SaveHistory(model, uid);
                    dc.ItemDebits.Add(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteCheckList(Guid id, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.CheckLists.Find(id);
                    dc.CheckLists.Remove(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteItemDebit(Guid id, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.ItemDebits.Find(id);
                    dc.ItemDebits.Remove(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcCheckList> GetCheckList(int s, int t, string title)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<CheckList, DcCheckList>());
                    var query = from i in dc.CheckLists
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                && i.Title.Contains(title)
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<CheckList>, List<DcCheckList>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetCheckListCount(string title)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.CheckLists
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                && i.Title.Contains(title)
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcItemDebit> GetItemDebit(int s, int t, string itemType, string itemName, string creditor)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ItemDebit, DcItemDebit>());
                    var query = from i in dc.ItemDebits
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                &&i.ItemName.Contains(itemName)
                                &&i.Creditor.Contains(creditor)
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<ItemDebit>, List<DcItemDebit>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int GetItemDebitCount(string itemType, string itemName, string creditor)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.ItemDebits
                                where i.State == PMSCommon.SimpleState.正常.ToString()
                                && i.ItemName.Contains(itemName)
                                && i.Creditor.Contains(creditor)
                                select i;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateCheckList(DcCheckList model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcCheckList, CheckList>());
                    var entity = Mapper.Map<CheckList>(model);
                    SaveHistory(model, uid);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateItemDebit(DcItemDebit model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcItemDebit, ItemDebit>());
                    var entity = Mapper.Map<ItemDebit>(model);
                    SaveHistory(model, uid);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcItemDebit model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcItemDebit, ItemDebitHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<ItemDebitHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.ItemDebitHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        private void SaveHistory(DcCheckList model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcCheckList, CheckListHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<CheckListHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.CheckListHistorys.Add(history);
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