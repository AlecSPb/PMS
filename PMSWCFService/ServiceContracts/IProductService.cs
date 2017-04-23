using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using PMSWCFService.DataContracts;

namespace PMSWCFService.ServiceContracts
{
    [ServiceContract]
    public interface IProductService
    {

        [OperationContract]
        List<DcProduct> GetProducts(int skip, int take, string productid, string composition);

        [OperationContract]
        int GetProductCount(string productid, string composition);
        [OperationContract]
        int AddProduct(DcProduct model);
        [OperationContract]
        int UpdateProduct(DcProduct model);
        [OperationContract]
        int AddProductByUID(DcProduct model,string uid);
        [OperationContract]
        int UpdateProductByUID(DcProduct model,string uid);

        [OperationContract]
        int DeleteProduct(Guid id);

    }
}
