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
        public void Run()
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("请问要查看发货产品对应样品状态情况吗？Y=查看 N=跳过"))
            {
                CheckDeliveryItemSample();
            }
        }

        private async void CheckDeliveryItemSample()
        {
            try
            {
                using (var s = new SampleServiceClient())
                {
                    StringBuilder sb = new StringBuilder();
                    var result = await s.CheckDeliveryItemSampleStatusAsync();
                    if (result.Count() > 0)
                    {
                        var win = new DeliveryItemSampleCheckView();
                        win.DgMain.ItemsSource = result;
                        win.ShowDialog();
                    }
                    else
                    {
                        XSHelper.XS.MessageBox.ShowInfo("没有找到相应信息");
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
