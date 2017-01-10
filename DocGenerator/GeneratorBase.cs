using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DocGenerator
{
    /// <summary>
    /// 所有Generator的基类
    /// </summary>
    public class GeneratorBase
    {
        public void CopyTemplate(string source, string target)
        {
            if (File.Exists(target))
            {
                File.Delete(target);
            }

            File.Copy(source, target);
        }
    }
}
