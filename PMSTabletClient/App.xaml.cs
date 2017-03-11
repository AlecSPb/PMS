using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using PMSTabletClient.PMSUserAccessService;


namespace PMSTabletClient
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            this.Startup += App_Startup;
        }

        private void App_Startup(object sender, StartupEventArgs e)
        {
            CurrentUser = new PMSUserAccessService.DcUser()
            {
                ID = Guid.NewGuid(),
                UserName = "xs.zhou",
                RealName = "周新生",
                CreateTime = DateTime.Now,
                Email = "xs.zhou@outlook.com",
                Phone = "13540781789"
            };
        }

        public DcUser CurrentUser { get; set; }
    }
}
