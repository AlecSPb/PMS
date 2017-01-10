using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocGenerator
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDoc<T>
    {

       void  Generate(string sourceFilePath,string targetFilePath, T reportModel);
    }
}
