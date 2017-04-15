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
    public partial class PMSService : IRecordBondingService
    {
        public int AddRecordBongding(DcRecordBonding model)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordBongding(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetRecordBondingCount(string TargetLot, string PlateLot)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordBonding> GetRecordBondings(int skip, int take, string TargetLot, string PlateLot)
        {
            throw new NotImplementedException();
        }

        public int UpdateRecordBongding(DcRecordBonding model)
        {
            throw new NotImplementedException();
        }
    }
}