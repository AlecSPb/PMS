using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.PMSSettings;

namespace PMSClient.Express
{

    public class ExpressConfigService
    {
        public static string ReadKey(string key)
        {
			try
			{
				using (var s=new PMSSettingServiceClient())
				{
					return s.GetValueByKey(key);
				}
			}
			catch (Exception)
			{
                return "";
			}
        }
    }
}
