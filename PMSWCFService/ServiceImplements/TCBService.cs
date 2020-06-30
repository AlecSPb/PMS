using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using PMSWCFService.ServiceImplements.Helpers;
using AutoMapper;

namespace PMSWCFService
{
    public class TCBService : ITCBService
    {
        public void AddDeliveryItemTCB(DcDeliveryItemTCB model)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcDeliveryItemTCB, DeliveryItemTCB>());
                    var entity = Mapper.Map<DeliveryItemTCB>(model);
                    db.DeliveryItemTCBs.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDelivery> GetDelivery(int s, int t, string deliveryname)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.DeliveryName.Contains(deliveryname)
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<Delivery, DcDelivery>();
                    });

                    var records = Mapper.Map<List<Delivery>, List<DcDelivery>>(query.Skip(s).Take(t).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetDeliveryCount(string deliveryname)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.Deliverys
                                where d.State != PMSCommon.DeliveryState.作废.ToString()
                                && d.DeliveryName.Contains(deliveryname)
                                select d;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDeliveryItemTCB> GetDeliveryItemTCB(int s, int t, string productid, string composition, string po,
            string customer, string bondingpo)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItemTCBs
                                where d.State != PMSCommon.DeliveryItemTCBState.Deleted.ToString()
                                && d.ProductID.Contains(productid)
                                && d.PO.Contains(po)
                                && d.Customer.Contains(customer)
                                && d.BondingPO.Contains(bondingpo)
                                && d.Composition.Contains(searchItem.Item1)
                                && d.Composition.Contains(searchItem.Item2)
                                && d.Composition.Contains(searchItem.Item3)
                                && d.Composition.Contains(searchItem.Item4)
                                orderby d.CreateTime descending
                                select d;
                    Mapper.Initialize(cfg =>
                    {
                        cfg.CreateMap<DeliveryItemTCB, DcDeliveryItemTCB>();
                    });

                    var records = Mapper.Map<List<DeliveryItemTCB>, List<DcDeliveryItemTCB>>(query.Skip(s).Take(t).ToList());
                    return records;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDeliveryItemTCB> GetDeliveryItemTCBByDeliveryID(Guid id)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DeliveryItem, DcDeliveryItem>());

                    var result = dc.DeliveryItemTCBs
                        .Where(i => i.DeliveryID == id && i.State != PMSCommon.DeliveryItemTCBState.Deleted.ToString())
                        .OrderByDescending(i => i.CreateTime)
                        .ToList();
                    return Mapper.Map<List<DeliveryItemTCB>, List<DcDeliveryItemTCB>>(result);
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetDeliveryItemTCBCount(string productid, string composition, string po, string customer, string bondingpo)
        {
            try
            {
                XS.RunLog();
                var searchItem = CompositionHelper.GetSearchItems(composition);
                using (var dc = new PMSDbContext())
                {
                    var query = from d in dc.DeliveryItemTCBs
                                where d.State != PMSCommon.DeliveryItemTCBState.Deleted.ToString()
                                && d.ProductID.Contains(productid)
                                && d.PO.Contains(po)
                                && d.Customer.Contains(customer)
                                && d.BondingPO.Contains(bondingpo)
                                && d.Composition.Contains(searchItem.Item1)
                                && d.Composition.Contains(searchItem.Item2)
                                && d.Composition.Contains(searchItem.Item3)
                                && d.Composition.Contains(searchItem.Item4)
                                select d;

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateDeliveryItemTCB(DcDeliveryItemTCB model)
        {
            try
            {
                XS.RunLog();

                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg =>
                    {
                        cfg.CreateMap<DcDeliveryItemTCB, DeliveryItemTCB>();
                    });
                    var mapper = config.CreateMapper();
                    var entity = mapper.Map<DeliveryItemTCB>(model);
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