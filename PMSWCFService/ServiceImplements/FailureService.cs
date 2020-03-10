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
    public class FailureService : IFailureService
    {
        private Log logger;

        public FailureService()
        {
            logger = new Log();
        }
        public int AddFailure(DcFailure model)
        {
            try
            {
                XS.Run();
                Mapper.Initialize(cfg => cfg.CreateMap<DcFailure, Failure>());
                var entity = Mapper.Map<Failure>(model);
                int result = 0;
                using (var db = new PMSDbContext())
                {
                    db.Failures.Add(entity);
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public List<DcFailure> GetFailures(int s, int t, string stage)
        {
            try
            {
                XS.Run();
                Mapper.Initialize(cfg => cfg.CreateMap<Failure, DcFailure>());
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Failures
                                where m.Stage.Contains(stage) && m.State != "作废"
                                orderby m.CreateTime descending
                                select m;

                    var list = Mapper.Map<List<Failure>, List<DcFailure>>(query.Skip(s).Take(t).ToList());
                    return list;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public List<DcFailure> GetFailuresBySearch(int s, int t, string productid, string composition, string stage)
        {
            try
            {
                XS.Run();
                Mapper.Initialize(cfg => cfg.CreateMap<Failure, DcFailure>());
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Failures
                                where 
                                m.Stage.Contains(stage)
                                && m.State.Contains(productid)
                                && m.State.Contains(composition)
                                && m.State != "作废"
                                orderby m.CreateTime descending
                                select m;

                    var list = Mapper.Map<List<Failure>, List<DcFailure>>(query.Skip(s).Take(t).ToList());
                    return list;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public int GetFailuresCount(string stage)
        {
            try
            {
                XS.Run();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Failures
                                where m.Stage.Contains(stage) && m.State != "作废"
                                select m;


                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public int GetFailuresCountByProductID(string productid)
        {
            try
            {
                XS.Run();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Failures
                                where m.ProductID == productid && m.State != "作废"
                                select m;


                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }

        public int UpdateFailure(DcFailure model)
        {
            try
            {
                XS.Run();
                Mapper.Initialize(cfg => cfg.CreateMap<DcFailure, Failure>());
                var entity = Mapper.Map<Failure>(model);
                int result = 0;
                using (var db = new PMSDbContext())
                {
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    result = db.SaveChanges();
                }
                return result;
            }
            catch (Exception ex)
            {
                logger.Error(ex);
                throw ex;
            }
        }
    }
}