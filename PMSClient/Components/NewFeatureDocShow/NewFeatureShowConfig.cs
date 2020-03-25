using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.NewFeatureDocShow
{
    public class NewFeatureShowConfig
    {
        public string HelpFileName { get; set; } = "新功能使用介绍200324.docx";
        public int HelpFileEdition { get; set; } = 3;
        public string[] HelpGroupNames { get; set; } = new string[] { "管理员", "统筹组", "测试组", "生产经理", "热压组" };
    }
}
