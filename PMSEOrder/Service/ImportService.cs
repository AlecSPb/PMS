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
            Order model = JsonConvert.DeserializeObject<Order>(json_str);
            try
            {
                if (model != null)
                {
                    var dbservice = new DataService();
                    if (dbservice.GetAllOrder().Where(i => i.GUIDID == model.GUIDID).Count() == 0)
                    {
                        dbservice.AddOrder(model);
                    }
                    else
                    {
                        XSHelper.XS.MessageBox.ShowInfo("repeat guid");
                    }
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
