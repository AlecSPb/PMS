-using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService
{
    public static class LocalService
    {
        static LocalService()
        {
            _log = new Log();
        }
        private static Log _log;
        public static Log CurrentLog
        {
            get { return _log; }
        }
    }
}