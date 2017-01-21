using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;

namespace PMSWCFService.ServiceImplements
{
    public class RecordVHPService : IRecordVHPService
    {
        public int AddRecordVHP(DcRecordVHP model)
        {
            throw new NotImplementedException();
        }

        public int AddRecordVHPItem(DcRecordVHPItem model)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordVHP(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteRecordVHPItem(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DcRecordVHP> GetRecordVHP(int skip, int take, string searchVHPID)
        {
            throw new NotImplementedException();
        }

        public int UpdateReocrdVHP(DcRecordVHP model)
        {
            throw new NotImplementedException();
        }

        public int UpdateReocrdVHPItem(DcRecordVHPItem model)
        {
            throw new NotImplementedException();
        }
    }
}