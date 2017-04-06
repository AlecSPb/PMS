using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;

namespace PMSWCFService
{
    public partial class PMSService : IMaterialInventoryService
    {
        public int AddMaterialInventoryIn(DcMaterialInventoryIn model)
        {
            throw new NotImplementedException();
        }

        public int AddMaterialInventoryOut(DcMaterialInventoryOut model)
        {
            throw new NotImplementedException();
        }

        public int DeleteMaterialInventoryIn(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteMaterialInventoryOut(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryIn> GetMaterialInventoryIns(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOuts(int skip, int take)
        {
            throw new NotImplementedException();
        }

        public int UpdateMaterialInventoryIn(DcMaterialInventoryIn model)
        {
            throw new NotImplementedException();
        }

        public int UpdateMaterialInventoryOut(DcMaterialInventoryOut model)
        {
            throw new NotImplementedException();
        }
    }
}