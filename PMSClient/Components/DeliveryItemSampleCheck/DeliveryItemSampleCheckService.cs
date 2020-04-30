using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.SampleService;

namespace PMSClient.Components.DeliveryItemSampleCheck
{

    public class DeliveryItemSampleCheckService
    {
        public void Check()
        {
			try
			{
				using (var s=new SampleServiceClient())
				{

				}
			}
			catch (Exception)
			{

				throw;
			}
        }
    }
}
