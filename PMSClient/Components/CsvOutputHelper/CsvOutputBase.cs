using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.CsvOutputHelper
{
    class CsvOutputBase
    {
        public CsvOutputBase()
        {
            OutputPosition = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }
        /// <summary>
        /// 输出路径
        /// 默认桌面
        /// </summary>
        public string OutputPosition { get; set; }

        public virtual void Output()
        {

        }
    }
}
