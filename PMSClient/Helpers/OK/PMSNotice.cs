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
                //var remoteCount = 0;
                var localCount = 0;
                //using (var service = new DeliveryServiceClient())
                //{
                //    //remoteCount = service.GetDeliveryCount();
                //    remoteCount = 4;
                //}
                localCount = Properties.Settings.Default.DeliveryCount;
                System.Diagnostics.Debug.Print($"local={localCount.ToString()},remote={remoteCount.ToString()}");

                if (remoteCount > localCount)
                {
                    if (_newMessage==null)
                    {
                        _newMessage = new PMSMessageBox();

                    }
                    //_newMessage.Message = $"有新的发货单{remoteCount}记录了，准备发货了";
                    //_newMessage.ShowDialog();
                    //保存远程数量到设置
                    Properties.Settings.Default.DeliveryCount = remoteCount;
                    Properties.Settings.Default.Save();
                }
                remoteCount++;
            }
            catch (Exception ex)
            {
                PMSHelper.CurrentLog.Error(ex);
            }
        }
    }
}
