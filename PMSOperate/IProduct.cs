using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSModel;

namespace PMSIService
{
    public interface IProduct
    {
        IList<Product> GetAll();
        IList<Product> GetBySearch();
        int Add(Product product);
        int Update(Product product);
        int Disable(Guid id);

    }
}
