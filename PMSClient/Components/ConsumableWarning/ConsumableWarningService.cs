using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.ConsumableService;

namespace PMSClient.Components.ConsumableWarning
{
    public class ConsumableWarningService
    {
        public void Run()
        {
            if (XSHelper.XS.MessageBox.ShowYesNo("请问要查看[消耗品库存预警]情况吗？Y=查看 N=跳过"))
            {
                CheckConsumableInventory();
            }
        }

        private async void CheckConsumableInventory()
        {
            try
            {
                using (var s = new ConsumableServiceClient())
                {
                    StringBuilder sb = new StringBuilder();
                    var result = await s.GetConsumableInventoryWarningAsync();
                    if (result.Count() > 0)
                    {
                        var win = new ConsumableWarning();
                        win.DgMain.ItemsSource = result;
                        win.Show();
                    }
                    else
                    {
                        XSHelper.XS.MessageBox.ShowInfo("没有找到相应信息");
                    }
                }
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }

    }
}
