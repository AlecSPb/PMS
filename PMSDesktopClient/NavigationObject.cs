using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDesktopClient
{
    public class NavigationObject
    {
        public string ViewName { get; set; }
        public object ModelObject { get; set; }
    }
    /// <summary>
    /// 增加一个泛型版本
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class NavigationObject<T>
    {
        public string ViewName { get; set; }
        public T ModelObject { get; set; }
    }
}
