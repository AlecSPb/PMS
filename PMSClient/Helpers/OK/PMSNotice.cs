using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using System.Timers;

namespace PMSClient
{
    /// <summary>
    /// 用到的各类通知
    /// </summary>
    public static class PMSNotice
    {
       
        private static int remoteCount = 4;
        //private static int localCount = 0;
        private static PMSMessageBox _newMessage;
        static PMSNotice()
        {
            _newMessage = new PMSMessageBox();
        }
        public static void HasNewDelivery()
        {
            try
            {

            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
