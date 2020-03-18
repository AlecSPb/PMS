using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Components.NewFeatureDocShow
{
    public class NewFeatureShowConfig
    {
        public string HelpFileName { get; set; } = "新功能使用介绍.docx";
        public int HelpFileEdition { get; set; } = 1;
        public string[] HelpGroupNames { get; set; } = new string[] { "管理员", "统筹组", "测试组" };
    }
}
