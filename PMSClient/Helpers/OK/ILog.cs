using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helpers
{
    public interface ILog
    {
        void Log(string message);
        void Error(string error);
    }
}
