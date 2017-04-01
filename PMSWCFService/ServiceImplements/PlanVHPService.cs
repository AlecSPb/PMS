using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;
using PMSCommon;



namespace PMSWCFService
{
    public partial class PMSService : IPlanVHPService
    {
        public int AddVHPPlan(DcPlanVHP model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHP>());
                    var mapper = config.CreateMapper();
                    var plan = mapper.Map<PMSPlanVHP>(model);
                    dc.VHPPlans.Add(plan);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int DeleteVHPPlan(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var plan = dc.VHPPlans.Find(id);
                    if (plan != null)
                    {
                        plan.State = OrderState.Deleted.ToString();
                        result = dc.SaveChanges();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DcPlanVHP> GetVHPPlansByOrderID(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<PMSPlanVHP, DcPlanVHP>());
                    var mapper = config.CreateMapper();

                    var plans = dc.VHPPlans.Where(p => p.OrderID == id &&
                    p.State != OrderState.Deleted.ToString()).OrderByDescending(p => p.PlanDate).ToList();

                    return mapper.Map<List<PMSPlanVHP>, List<DcPlanVHP>>(plans);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int UpdateVHPPlan(DcPlanVHP model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcPlanVHP, PMSPlanVHP>());
                    var mapper = config.CreateMapper();
                    var plan = mapper.Map<PMSPlanVHP>(model);
                    dc.Entry(plan).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}