using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.EditLockService;

namespace PMSClient.Helpers
{
    public static class EditLockHelper
    {
        /// <summary>
        /// 获取锁定指纹
        /// </summary>
        /// <param name="id"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        public static string GetLockFingerPrint(Guid id)
        {
            return $"{id.ToString()}";
        }

        public static DcEditLock GetCurrentLock(Guid id, string itemname = "")
        {
            if (PMSHelper.CurrentSession.CurrentUser != null)
            {
                DcEditLock model = new DcEditLock();
                model.ID = Guid.NewGuid();
                model.Operator = PMSHelper.CurrentSession.CurrentUser.UserName;
                model.ComputerInfo = System.Net.Dns.GetHostName();
                model.LockTime = DateTime.Now.ToString();
                model.ItemName = itemname;
                model.FingerPrint = GetLockFingerPrint(id);
                return model;
            }
            return null;
        }


        public static string CheckLock(Guid id)
        {
            try
            {
                using (var s = new EditLockService.EditLockServiceClient())
                {

                    //检查编辑锁定
                    string fingerprint = Helpers.EditLockHelper.GetLockFingerPrint(id);
                    var islock = s.CheckLock(fingerprint);
                    if (islock != null)
                    {
                        StringBuilder lockinfo = new StringBuilder();
                        lockinfo.AppendLine("该条记录已经被锁定，无法编辑，锁定信息如下：");
                        lockinfo.AppendLine($"锁定人:{islock.Operator}");
                        lockinfo.AppendLine($"锁定时间:{islock.LockTime}");
                        lockinfo.AppendLine($"锁定电脑:{islock.ComputerInfo}");
                        lockinfo.AppendLine($"锁定类别:{islock.ItemName}");
                        return lockinfo.ToString();
                    }
                }
            }
            catch (Exception)
            {

            }
            return null;

        }

        public static void Lock(Guid id, string itemname)
        {

            try
            {
                using (var s = new EditLockService.EditLockServiceClient())
                {
                    //锁定
                    EditLockService.DcEditLock locker = Helpers.EditLockHelper
                                                .GetCurrentLock(id, itemname);
                    s.Lock(locker);
                }
            }
            catch (Exception)
            {

            }
        }

        public static void UnLock(Guid id)
        {
            try
            {
                using (var s = new EditLockService.EditLockServiceClient())
                {
                    string fingerprint = Helpers.EditLockHelper.GetLockFingerPrint(id);
                    s.UnLock(fingerprint);
                }
            }
            catch (Exception)
            {

            }
        }

        public static void UnLockByCurrentUser()
        {
            try
            {
                using (var s = new EditLockService.EditLockServiceClient())
                {
                    s.UnLockByLocker(PMSHelper.CurrentSession.CurrentUser.UserName);
                }
            }
            catch (Exception)
            {

            }

        }
        public static void UnLockAll()
        {
            try
            {
                using (var s = new EditLockService.EditLockServiceClient())
                {
                    s.UnLockAll();
                }
            }
            catch (Exception)
            {

            }

        }
    }
}
