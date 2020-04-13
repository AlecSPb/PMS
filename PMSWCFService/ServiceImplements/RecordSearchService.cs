using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSCommon;
using AutoMapper;

namespace PMSWCFService
{
    public class RecordSearchService : IRecordSearchService
    {
        public List<DcDeliveryItem> GetDeliveryItemBySearchLot(string searchLot)
        {
            throw new NotImplementedException();
        }

        public List<DcProduct> GetProductBySearchLot(string searchLot)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordBonding> GetRecordBondingBySearchLot(string searchLot)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordDeMold> GetRecordDeMoldBySearchLot(string searchLot)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordMachine> GetRecordMachineBySearchLot(string searchLot)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordMilling> GetRecordMillingBySearchLot(string searchLot)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMilling, DcRecordMilling>());
                    var query = from r in dc.RecordMillings
                                where r.State != PMSCommon.SimpleState.作废.ToString()
                                && r.VHPPlanLot.Contains(searchLot)
                                orderby r.CreateTime descending
                                select r;
                    return Mapper.Map<List<RecordMilling>, List<DcRecordMilling>>(query.ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordTest> GetRecordTestBySearchLot(string searchLot)
        {
            try
            {
                XS.RunLog();
                using (var dc = new PMSDbContext())
                {
                    var query = from t in dc.RecordTests
                                where t.ProductID.Contains(searchLot)
                                && t.State != CommonState.作废.ToString()
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
    }
}