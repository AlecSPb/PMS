using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IRecordTestService
    {
        public int AddRecordTest(DcRecordTest model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTest, RecordTest>());
                    var product = Mapper.Map<RecordTest>(model);
                    dc.RecordTests.Add(product);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int AddRecordTestByUID(DcRecordTest model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    SaveHistory(model, uid);
                    return AddRecordTest(model);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int DeleteRecordTest(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var product = dc.RecordTests.Find(id);
                    if (product != null)
                    {
                        dc.RecordTests.Remove(product);
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public List<DcRecordTest> GetRecordTestByProductID(string productId)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID == productId && t.State != "作废"
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 最新使用的API
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="productId"></param>
        /// <param name="compositionStd"></param>
        /// <param name="pminumber"></param>
        /// <returns></returns>
        public List<DcRecordTest> GetRecordTestBySearch(int skip, int take, string productId, string compositionStd, string pminumber)
        {
            try
            {
                XS.RunLog();
                var searchItem = PMSWCFService.ServiceImplements.Helpers.CompositionHelper.GetSearchItems(compositionStd);
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                 && t.Composition.Contains(searchItem.Item1)
                                 && t.Composition.Contains(searchItem.Item2)
                                 && t.Composition.Contains(searchItem.Item3)
                                 && t.Composition.Contains(searchItem.Item4)
                                && t.PMINumber.Contains(pminumber)
                                && t.State != CommonState.作废.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.Skip(skip).Take(take).ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
        /// <summary>
        /// 最新使用的API
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="compositionStd"></param>
        /// <param name="pminumber"></param>
        /// <returns></returns>
        public int GetRecordTestCountBySearch(string productId, string compositionStd, string pminumber)
        {
            try
            {
                XS.RunLog();
                var searchItem = PMSWCFService.ServiceImplements.Helpers.CompositionHelper.GetSearchItems(compositionStd);
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                 && t.Composition.Contains(searchItem.Item1)
                                 && t.Composition.Contains(searchItem.Item2)
                                 && t.Composition.Contains(searchItem.Item3)
                                 && t.Composition.Contains(searchItem.Item4)
                                && t.PMINumber.Contains(pminumber)
                                && t.State != CommonState.作废.ToString()
                                select t;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        /// <summary>
        /// 最新使用的API
        /// </summary>
        /// <param name="skip"></param>
        /// <param name="take"></param>
        /// <param name="productId"></param>
        /// <param name="compositionStd"></param>
        /// <returns></returns>
        public List<DcRecordTest> GetRecordTestBySearchInPage(int skip, int take, string productId, string compositionStd)
        {
            try
            {
                XS.RunLog();
                var searchItem = PMSWCFService.ServiceImplements.Helpers.CompositionHelper.GetSearchItems(compositionStd);
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                 && t.Composition.Contains(searchItem.Item1)
                                 && t.Composition.Contains(searchItem.Item2)
                                 && t.Composition.Contains(searchItem.Item3)
                                 && t.Composition.Contains(searchItem.Item4)
                                && t.State != CommonState.作废.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.Skip(skip).Take(take).ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public List<DcRecordTest> GetRecordTestChecked(int skip, int take, string productId, string compositionStd)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(compositionStd)
                                && t.State == CommonState.已核验.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.Skip(skip).Take(take).ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 最新使用的API
        /// </summary>
        /// <param name="productId"></param>
        /// <param name="compositionStd"></param>
        /// <returns></returns>
        public int GetRecordTestCountBySearchInPage(string productId, string compositionStd)
        {
            try
            {
                XS.RunLog();
                var searchItem = PMSWCFService.ServiceImplements.Helpers.CompositionHelper.GetSearchItems(compositionStd);
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(searchItem.Item1)
                                && t.Composition.Contains(searchItem.Item2)
                                && t.Composition.Contains(searchItem.Item3)
                                && t.Composition.Contains(searchItem.Item4)
                                && t.State != CommonState.作废.ToString()
                                select t;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int GetRecordTestCountChecked(string productId, string compositionStd)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(productId)
                                && t.Composition.Contains(compositionStd)
                                && t.State == CommonState.已核验.ToString()
                                select t;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordTest> GetUnFinishedRecordTest()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.State == CommonState.未录完.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordTest> GetUnCheckedRecordTest()
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.State == CommonState.未核验.ToString()
                                orderby t.CreateTime descending
                                select t;
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordTest, DcRecordTest>());
                    var products = Mapper.Map<List<RecordTest>, List<DcRecordTest>>(query.ToList());
                    return products;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordTest(DcRecordTest model)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordTest, RecordTest>());
                    var product = Mapper.Map<RecordTest>(model);
                    dc.Entry(product).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();


                    return result;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }

        }

        public int UpdateRecordTestByUID(DcRecordTest model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    SaveHistory(model, uid);
                    return UpdateRecordTest(model);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        /// <summary>
        /// 更新备份
        /// 在更新的时候
        /// 但是会遗漏掉第一次
        /// </summary>
        /// <param name="model"></param>
        private void SaveHistory(DcRecordTest model, string uid)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcRecordTest, RecordTestHistory>();
                    });
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<RecordTestHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.RecordTestHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public DateTime GetLastUpdateTime(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var r = db.RecordTests.Find(id);
                    return r.LastUpdateTime;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return DateTime.Now;
            }
        }
    }
}