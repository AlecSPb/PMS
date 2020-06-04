using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEOrder.Model;
using Newtonsoft.Json;

namespace PMSEOrder.Service
{
    public class ImportService
    {
        public static bool SaveToDB(string json_str)
        {
            Order model= JsonConvert.DeserializeObject<Order>(json_str);
            try
            {
                if (model != null)
                {
                    new DataService().AddOrder(model);
                }
                return true;
            }
            catch (Exception e)
            {
                XSHelper.XS.MessageBox.ShowWarning(e.Message);
                return false;
            }

        }
    }
}
