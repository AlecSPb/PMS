using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDesktopClient
{
    /// <summary>
    /// 用来消息传递的Object
    /// </summary>
    public class MsgObject
    {
        public MsgObject()
        {
            GoToToken = "";
            StringMessage = "";
        }
        public string GoToToken { get; set; }
        public ModelObject Model { get; set; }
        public ModelObject ModelExtra { get; set; }
        public string StringMessage { get; set; }




        //后期去除
        public object ModelObject { get; set; }
        public bool IsAdd { get; set; }//判定是否添加
    }
    /// <summary>
    /// 传入的Object
    /// </summary>
    public class ModelObject
    {
        public bool IsNew { get; set; }
        public object Model { get; set; }
    }
}
