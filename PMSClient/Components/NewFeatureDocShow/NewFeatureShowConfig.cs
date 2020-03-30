using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.NewFeatureDocShow
{
    public class NewFeatureShowConfig
    {
        public bool IsWorking { get; set; } = true;//总的决定要不要启用新功能介绍
        public string HelpFileName { get; set; } = "新功能使用介绍200402.docx";
        public int HelpFileEdition { get; set; } = 8;//通过和每个人本地存储的Config中的文件对比来判断是否需要弹出新功能提醒
        public string[] HelpGroupNames { get; set; } = new string[] { "管理员", "统筹组", "测试组", "生产经理", 
            "热压组", "制粉组", "发货组", "财务组" };//谁能看到更新提示
    }
}
