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

        public int AddRecordBongdingPlate(DcRecordBondingPlate model)
        {
            throw new NotImplementedException();
        }

        public int AddRecordBongdingTarget(DcRecordBondingTarget model)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordBongding(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordBongdingPlate(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordBongdingTarget(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetRecordBondingCount()
        {
            throw new NotImplementedException();
        }

        public List<DcRecordBondingPlate> GetRecordBondingPlates(Guid bondingId)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordBondingPlate> GetRecordBondingPlates(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordBonding> GetRecordBondings(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordBondingTarget> GetRecordBondingTargets(Guid bongdingId)
        {
            throw new NotImplementedException();
        }

        public int UpdateRecordBongding(DcRecordBonding model)
        {
            throw new NotImplementedException();
        }

        public int UpdateRecordBongdingPlate(DcRecordBondingPlate model)
        {
            throw new NotImplementedException();
        }

        public int UpdateRecordBongdingTarget(DcRecordBondingTarget model)
        {
            throw new NotImplementedException();
        }
    }
}