﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.UserService;

namespace PMSClient
{
    public static class PMSUserHelper
    {
        /// <summary>
        ///MainDesktop版本
        /// </summary>
        public static LogInformation CurrentLogInformation
        {
            get { return (App.Current as App).CurrentLogInformation; }
        }
    }
}
