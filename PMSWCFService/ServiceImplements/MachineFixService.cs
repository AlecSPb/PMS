using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;
using PMSWCFService.DataContracts;
using AutoMapper;

namespace PMSWCFService
{
    public class MachineFixService : IMachineFixService
    {
        public void AddMachineFix(DcMachineFix model)
        {
            throw new NotImplementedException();
        }

        public void DeleteMachineFix(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetMachineFixeCount(string fixtype, string devicename, string partname)
        {
            throw new NotImplementedException();
        }

        public List<DcMachineFix> GetMachineFixes(int s, int t, string fixtype, string devicename, string partname)
        {
            throw new NotImplementedException();
        }

        public void UpdateMachineFix(DcMachineFix model)
        {
            throw new NotImplementedException();
        }
    }
}