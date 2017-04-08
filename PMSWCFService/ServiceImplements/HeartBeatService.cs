using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.ServiceContracts;

namespace PMSWCFService
{
    public class HeartBeatService : IHeartBeatSerive
    {
        public string Beat()
        {
            return "ok";
        }
    }
}