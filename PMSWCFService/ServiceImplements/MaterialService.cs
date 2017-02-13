using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSCommon;

namespace PMSWCFService
{
    public partial class PMSService : IMaterialNeedService, IMaterialOrderService
    {
        public int AddMaterialNeed(DcMaterialNeed model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialNeed, PMSMaterialNeed>());
                var mapper = config.CreateMapper();
                var materialNeed = mapper.Map<PMSMaterialNeed>(model);
                dc.MaterialNeeds.Add(materialNeed);
                result = dc.SaveChanges();

                return result;
            }
        }

        public int AddMaterialOrder(DcMaterialOrder model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialOrder, PMSMaterialOrder>());
                var mapper = config.CreateMapper();
                var materialOrder = mapper.Map<PMSMaterialOrder>(model);
                dc.MaterialOrders.Add(materialOrder);
                result = dc.SaveChanges();

                return result;
            }
        }
        public int AddMaterialOrderItem(DcMaterialOrderItem model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialOrderItem, PMSMaterialOrderItem>());
                var mapper = config.CreateMapper();
                var materialOrderItem = mapper.Map<PMSMaterialOrderItem>(model);
                dc.MaterialOrderItems.Add(materialOrderItem);
                result = dc.SaveChanges();

                return result;
            }
        }

        public int DeleteMaterialNeed(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var model = dc.MaterialNeeds.Find(id);
                if (model != null)
                {
                    model.State = OrderState.Deleted.ToString();
                    result = dc.SaveChanges();
                }

                return result;
            }
        }

        public int DeleteMaterialOrder(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var model = dc.MaterialOrders.Find(id);
                if (model != null)
                {
                    model.State = OrderState.Deleted.ToString();
                    result = dc.SaveChanges();
                }
                return result;
            }
        }

        public int DeleteMaterialOrderItem(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var model = dc.MaterialOrderItems.Find(id);
                if (model != null)
                {
                    model.State = OrderState.Deleted.ToString();
                    result = dc.SaveChanges();
                }
                return result;
            }
        }

        public List<DcMaterialNeed> GetMaterialNeedBySearchInPage(int skip, int take, string composition)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSMaterialNeed, DcMaterialNeed>());
                var mapper = config.CreateMapper();
                var result = dc.MaterialNeeds.Where(m => m.Composition.Contains(composition) && m.State!=OrderState.Deleted.ToString())
                    .OrderByDescending(m => m.CreateTime)
                    .Skip(skip).Take(take)
                    .ToList();
                return mapper.Map<List<PMSMaterialNeed>, List<DcMaterialNeed>>(result);
            }
        }

        public int GetMaterialNeedCountBySearch(string composition)
        {
            using (var dc = new PMSDbContext())
            {
                return dc.MaterialNeeds.Where(m => m.Composition.Contains(composition) && m.State!=OrderState.Deleted.ToString()).Count();
            }
        }

        public List<DcMaterialOrder> GetMaterialOrderBySearchInPage(int skip, int take, string orderPo, string supplier)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<PMSMaterialOrder, DcMaterialOrder>();
                    cfg.CreateMap<PMSMaterialOrderItem, DcMaterialOrderItem>();
                });
                var mapper = config.CreateMapper();
                var result = dc.MaterialOrders.Include("MaterialOrderItems").Where(m => m.OrderPO.Contains(orderPo) && m.Supplier.Contains(supplier)
                && m.State!=OrderState.Deleted.ToString())
                    .OrderByDescending(m => m.CreateTime).Skip(skip).Take(take).ToList();
                return mapper.Map<List<PMSMaterialOrder>, List<DcMaterialOrder>>(result);

            }
        }

        public int GetMaterialOrderCountBySearch(string orderPo, string supplier)
        {
            using (var dc = new PMSDbContext())
            {
                return dc.MaterialOrders.Where(m => m.OrderPO.Contains(supplier) && m.State!=OrderState.Deleted.ToString()).Count();
            }
        }

        public List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id)
        {
            using (var dc = new PMSDbContext())
            {
                var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSMaterialOrderItem, DcMaterialOrderItem>());
                var mapper = config.CreateMapper();
                var result = dc.MaterialOrderItems.Where(m => m.MaterialOrderID == id).OrderByDescending(m => m.CreateTime).ToList();
                return mapper.Map<List<PMSMaterialOrderItem>, List<DcMaterialOrderItem>>(result);
            }
        }

        public int UpdateMaterialNeed(DcMaterialNeed model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialNeed, PMSMaterialNeed>());
                var mapper = config.CreateMapper();
                var materialNeed = mapper.Map<PMSMaterialNeed>(model);
                dc.Entry(materialNeed).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }

        public int UpdateMaterialOrder(DcMaterialOrder model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialOrder, PMSMaterialOrder>());
                var mapper = config.CreateMapper();
                var materialOrder = mapper.Map<PMSMaterialOrder>(model);
                dc.Entry(materialOrder).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
                return result;
            }
        }

        public int UpdateMaterialOrderItem(DcMaterialOrderItem model)
        {
            using (var dc = new PMSDbContext())
            {
                int result = 0;
                var config = new MapperConfiguration(cfg => cfg.CreateMap<DcMaterialOrderItem, PMSMaterialOrderItem>());
                var mapper = config.CreateMapper();
                var materialOrderItem = mapper.Map<PMSMaterialOrderItem>(model);
                dc.Entry(materialOrderItem).State = System.Data.Entity.EntityState.Modified;
                dc.SaveChanges();
                return result;
            }
        }
    }
}