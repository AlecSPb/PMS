using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSClient.Helper
{
    public interface ILog
    {
        void Log(string message);
        void Error(Exception ex);
        void Error(Exception ex, string position);
    }
}
