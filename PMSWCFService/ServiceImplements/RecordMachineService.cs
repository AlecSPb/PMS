using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSWCFService.DataContracts;

namespace PMSWCFService
{
    public partial class PMSService : IRecordMachineService
    {
        public int AddRecordMachine(DcRecordMachine model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordMachine, RecordMachine>());
                    var temp = Mapper.Map<RecordMachine>(model);
                    dc.RecordMachines.Add(temp);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int AddRecordMachineByUID(DcRecordMachine model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return AddRecordMachine(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int DeleteRecordMachine(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result=0;
                    var model = dc.RecordMachines.Find(id);
                    dc.RecordMachines.Remove(model);
                    result = dc.SaveChanges();
                    return result;
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetRecordMachineCount()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    return dc.RecordMachines.Where(r => r.State != PMSCommon.SimpleState.作废.ToString()).Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int GetRecordMachineCountByVHPPlanLot(string vhpplanlot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var query = from r in dc.RecordMachines
                                where r.VHPPlanLot.Contains(vhpplanlot) && r.State != PMSCommon.SimpleState.作废.ToString()
                                select r;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMachine> GetRecordMachines(int skip, int take)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordMachines.Where(r => r.State != PMSCommon.SimpleState.作废.ToString())
                        .OrderByDescending(i => i.VHPPlanLot).Skip(skip).Take(take).ToList();

                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMachine, DcRecordMachine>());
                    return Mapper.Map<List<RecordMachine>, List<DcRecordMachine>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public List<DcRecordMachine> GetRecordMachinesByPMINumber(string pminumber)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var result = dc.RecordMachines.Where(r => r.State != PMSCommon.SimpleState.作废.ToString()
                    &&r.PMINumber==pminumber)
                        .OrderByDescending(i => i.VHPPlanLot).ToList();

                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMachine, DcRecordMachine>());
                    return Mapper.Map<List<RecordMachine>, List<DcRecordMachine>>(result);
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public List<DcRecordMachine> GetRecordMachinesByVHPPlanLot(int skip, int take, string vhpplanlot)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<RecordMachine, DcRecordMachine>());
                    var query = from r in dc.RecordMachines
                                where r.VHPPlanLot.Contains(vhpplanlot) && r.State != PMSCommon.SimpleState.作废.ToString()
                                orderby r.VHPPlanLot descending
                                select r;
                    return Mapper.Map<List<RecordMachine>, List<DcRecordMachine>>(query.Skip(skip).Take(take).ToList());
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }

        public int UpdateRecordMachine(DcRecordMachine model)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    int result = 0;
                    Mapper.Initialize(cfg => cfg.CreateMap<DcRecordMachine, RecordMachine>());
                    var temp = Mapper.Map<RecordMachine>(model);
                    dc.Entry(temp).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                    return result;
                };
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }

        }

        public int UpdateRecordMachineByUID(DcRecordMachine model, string uid)
        {
            try
            {
                SaveHistory(model, uid);
                return UpdateRecordMachine(model);
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
                throw ex;
            }
        }
        private void SaveHistory(DcRecordMachine model, string uid)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var config = new MapperConfiguration(cfg => cfg.CreateMap<DcRecordMachine, RecordMachineHistory>());
                    var mapper = config.CreateMapper();
                    var history = mapper.Map<RecordMachineHistory>(model);
                    history.OperateTime = DateTime.Now;
                    history.Operator = uid;
                    history.HistoryID = Guid.NewGuid();
                    dc.RecordMachineHistorys.Add(history);
                    dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                LocalService.CurrentLog.Error(ex);
            }
        }
    }
}