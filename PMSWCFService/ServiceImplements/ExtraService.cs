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
    public partial class ExtraService : ICheckListService, IItemDebitService, IFeedBackService, IEnvironmentInfoService,
        INoticeService, IToDoService

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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                                && i.ItemName.Contains(itemName)
                                && i.Creditor.Contains(creditor)
                                orderby i.CreateTime descending
                                select i;
                    return Mapper.Map<List<ItemDebit>, List<DcItemDebit>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
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
                XS.Current.Error(ex);
            }
        }


        public int AddFeedBack(DcFeedBack model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcFeedBack, FeedBack>());
                    var entity = Mapper.Map<FeedBack>(model);
                    dc.FeedBacks.Add(entity);
                    return dc.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int DeleteFeedBack(Guid id, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.FeedBacks.Find(id);
                    dc.FeedBacks.Remove(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcFeedBack> GetFeedBack(int s, int t, string productId, string composition, string customer)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<FeedBack, DcFeedBack>());
                    var query = from i in dc.FeedBacks
                                where i.State != PMSCommon.SimpleState.作废.ToString()
                                && i.ProductID.Contains(productId)
                                && i.Composition.Contains(composition)
                                && i.Customer.Contains(customer)
                                orderby i.CreateTime descending
                                select i;

                    return Mapper.Map<List<FeedBack>, List<DcFeedBack>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetFeedBackCount(string productId, string composition, string customer)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.FeedBacks
                                where i.State != PMSCommon.SimpleState.作废.ToString()
                                && i.ProductID.Contains(productId)
                                && i.Composition.Contains(composition)
                                && i.Customer.Contains(customer)
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

        public int UpdateFeedBack(DcFeedBack model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcFeedBack, FeedBack>());
                    var entity = Mapper.Map<FeedBack>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcEnvironmentInfo GetEnvironmentInfo(string position)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<EnvironmentInfo, DcEnvironmentInfo>());
                    var query = dc.EnvironmentInfos.Where(i => i.Position == position).FirstOrDefault();
                    return Mapper.Map<DcEnvironmentInfo>(query);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateEnvironmentInfor(DcEnvironmentInfo model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcEnvironmentInfo, EnvironmentInfo>());
                    var entity = Mapper.Map<EnvironmentInfo>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public DcNotice GetCurrentNotice()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Notice, DcNotice>());
                    var query = dc.Notices.FirstOrDefault();
                    return Mapper.Map<DcNotice>(query);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateNotice(DcNotice model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcNotice, Notice>());
                    var entity = Mapper.Map<Notice>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public List<DcToDo> GetToDo(string title, string personInCharge, int s, int t)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<ToDo, DcToDo>());
                    var query = from i in dc.ToDoes
                                where i.Title.Contains(title)
                                && i.PersonInCharge.Contains(personInCharge)
                                && i.Status != PMSCommon.ToDoStatus.作废.ToString()
                                orderby i.Progress ascending, i.CreateTime descending
                                select i;

                    return Mapper.Map<List<ToDo>, List<DcToDo>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetToDoCount(string title, string personInCharge)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from i in dc.ToDoes
                                where i.Title.Contains(title)
                                && i.PersonInCharge.Contains(personInCharge)
                                && i.Status != PMSCommon.ToDoStatus.作废.ToString()
                                orderby i.CreateTime descending
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

        public int AddToDo(DcToDo model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcToDo, ToDo>());
                    var entity = Mapper.Map<ToDo>(model);
                    dc.ToDoes.Add(entity);
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateToDo(DcToDo model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcToDo, ToDo>());
                    var entity = Mapper.Map<ToDo>(model);
                    dc.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    return dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int DeleteToDo(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = dc.ToDoes.Find(id);
                    dc.ToDoes.Remove(entity);
                    return dc.SaveChanges();
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