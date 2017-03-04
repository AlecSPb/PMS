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

        public int DeleteRecordMachine(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetRecordMachineCount()
        {
            using (var dc = new PMSDbContext())
            {
                return dc.RecordMachines.Count();
            }
        }

        public List<DcRecordMachine> GetRecordMachines(int skip, int take)
        {
            using (var dc=new PMSDbContext())
            {
                var result = dc.RecordMachines.OrderByDescending(i => i.CreateTime).Skip(skip).Take(take).ToList();

                Mapper.Initialize(cfg => cfg.CreateMap<RecordMachine, DcRecordMachine>());
                return Mapper.Map<List<RecordMachine>, List<DcRecordMachine>>(result);
            }
        }

        public int UpdateRecordMachine(DcRecordMachine model)
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
    }
}