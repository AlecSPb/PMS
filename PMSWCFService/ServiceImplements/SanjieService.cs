using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSDAL;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;


namespace PMSWCFService
{
    public class SanjieService : ISanjieService
    {
        public List<DcMaterialInventoryIn> GetMaterialInventoryIn(int skip, int take, string orderlot, string composition)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialInventoryInCount(int skip, int take, string orderlot, string composition)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialInventoryOut> GetMaterialInventoryOut(int skip, int take, string orderlot, string composition)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialInventoryOutCount(int skip, int take, string orderlot, string composition)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialOrder> GetMaterialOrder(int skip, int take, string orderPo)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialOrderCount(string orderPo)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialOrderItem> GetMaterialOrderItem(int skip, int take, string orderPo)
        {
            throw new NotImplementedException();
        }

        public List<DcMaterialOrderItem> GetMaterialOrderItembyMaterialID(Guid id)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialOrderItemCount(string orderPo)
        {
            throw new NotImplementedException();
        }

        public int GetMaterialOrderItemCountByMaterialID(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}