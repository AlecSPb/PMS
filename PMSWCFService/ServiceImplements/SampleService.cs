using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using PMSDAL;
using AutoMapper;
using PMSWCFService.ServiceImplements.Helpers;

namespace PMSWCFService
{
    public class SampleService : ISampleService
    {
        public void AddSample(DcSample model)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcSample, Sample>());
                    var entity = Mapper.Map<Sample>(model);
                    db.Samples.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public List<DcDeliveryItemSampleCheckModel> CheckDeliveryItemSampleStatus()
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.DeliveryItems
                                join mm in db.Deliverys on m.DeliveryID equals mm.ID
                                where (m.ProductType == PMSCommon.ProductType.靶材.ToString()
                                || m.ProductType == PMSCommon.ProductType.绑定.ToString())
                                && m.DeliveryType == PMSCommon.DeliveryType.最终发货.ToString()
                                && m.State == PMSCommon.SimpleState.正常.ToString()
                                && mm.State == PMSCommon.DeliveryState.未完成.ToString()
                                orderby m.CreateTime descending
                                select new { DeliveryItem = m, Delivery = mm };

                    List<DcDeliveryItemSampleCheckModel> sampleChecks = new List<DcDeliveryItemSampleCheckModel>();
                    foreach (var item in query.ToList())
                    {
                        var sample = new DcDeliveryItemSampleCheckModel();
                        sample.DeliveryTime = item.DeliveryItem.CreateTime;
                        sample.ProductID = item.DeliveryItem.ProductID;
                        sample.Composition = item.DeliveryItem.Composition;
                        sample.Customer = item.DeliveryItem.Customer;

                        //获取PMINumber
                        var test_record = DeliveryItemSampleCheckHelper.GetPMINumber(item.DeliveryItem.ProductID);
                        if (!string.IsNullOrEmpty(test_record.PMINumber))
                        {
                            sample.PMINumber = test_record.PMINumber;

                            //获取样品记录信息
                            var sample_list = DeliveryItemSampleCheckHelper.CheckSample(sample.PMINumber);
                            //只记录找到样品记录的
                            if (sample_list.Count > 0)
                            {
                                string s_information = "", s_deliveryInformation = "";
                                foreach (var s in sample_list)
                                {
                                    s_information += $"[{s.OriginalRequirement}]";
                                    s_deliveryInformation += $"[{s.PMINumber}-{s.SampleID}-{s.TrackingStage}]";
                                }
                                sample.SampleInformation = s_information;
                                sample.SampleDeliveryInformation = s_deliveryInformation;
                                //查询对应信息
                                sampleChecks.Add(sample);
                            }

                        }

                    }

                    return sampleChecks;
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return new List<DcDeliveryItemSampleCheckModel>();
            }

        }

        public List<DcSample> GetSampleAll(int s, int t, string pminumber, string productid, string composition, string trackingstage)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<Sample, DcSample>());
                    var query = from m in db.Samples
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.PMINumber.Contains(pminumber)
                                && m.ProductID.Contains(productid)
                                && m.Composition.Contains(composition)
                                && m.TrackingStage.Contains(trackingstage)
                                orderby m.CreateTime descending
                                select m;
                    return Mapper.Map<List<Sample>, List<DcSample>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return new List<DcSample>();
            }
        }

        public int GetSampleAllCount(string pminumber, string productid, string composition, string trackingstage)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Samples
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.PMINumber.Contains(pminumber)
                                && m.ProductID.Contains(productid)
                                && m.Composition.Contains(composition)
                                && m.TrackingStage.Contains(trackingstage)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;
            }
        }

        public int GetSampleByPMINumberCount(string pminumber)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.Samples
                                where m.State != PMSCommon.SimpleState.作废.ToString()
                                && m.PMINumber.Contains(pminumber)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;
            }
        }

        public void UpdateSample(DcSample model)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcSample, Sample>());
                    var entity = Mapper.Map<Sample>(model);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }
    }
}