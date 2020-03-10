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
    public class EFModel
    {
        public DeliveryItem Delivery { get; set; }
        public RecordBonding Bond { get; set; }
        public RecordTest Test { get; set; }
    }


    public class OutputService : IOutputService
    {
        public List<PMS230DataModel> GetAll230Data(int s, int t)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var query = from dd in db.DeliveryItems
                                join tt in db.RecordTests on dd.ProductID equals tt.ProductID into bb
                                from bbdata in bb.DefaultIfEmpty()
                                join b in db.RecordBondings on dd.ProductID equals b.TargetProductID into cc
                                from ccdata in cc.DefaultIfEmpty()
                                where dd.State == "正常" && bbdata.State == "已核验" && ccdata.State == "完成"
                                && (dd.Customer == "Midsummer" || dd.Customer == "Chaozhou")
                                && dd.Dimension.Contains("230")
                                orderby dd.ProductID descending
                                select new EFModel
                                {
                                    Delivery = dd,
                                    Test = bbdata,
                                    Bond = ccdata
                                };

                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<EFModel, PMS230DataModel>();
                        cfg.CreateMap<DeliveryItem, DcDeliveryItem>();
                        cfg.CreateMap<RecordTest, DcRecordTest>();
                        cfg.CreateMap<RecordBonding, DcRecordBonding>();
                    });

                    var new_models = query.Skip(s).Take(t).ToList();

                    var models = Mapper.Map<List<EFModel>, List<PMS230DataModel>>(new_models);
                    return models;
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetAll230DataCount()
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var query = from d in db.DeliveryItems
                                join t in db.RecordTests on d.ProductID equals t.ProductID into bb
                                from bbdata in bb.DefaultIfEmpty()
                                join b in db.RecordBondings on d.ProductID equals b.TargetProductID into cc
                                from ccdata in cc.DefaultIfEmpty()
                                where d.State == "正常" && bbdata.State == "已核验" && ccdata.State == "完成"
                                && (d.Customer == "Midsummer" || d.Customer == "Chaozhou")
                                && d.Dimension.Contains("230")
                                select new
                                {
                                    Delivery = d,
                                    Test = bbdata,
                                    Bond = ccdata
                                };


                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LS.CurrentLog.Error(ex);
                throw ex;
            }


        }
    }
}