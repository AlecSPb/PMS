using AuthorizationChecker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;

namespace PMSWCFService
{
    /// <summary>
    /// 审核记录类
    /// </summary>
    public static class XS
    {
        static XS()
        {
            log = new Log();
        }
        private static Log log;

        public static Log Current
        {
            get { return log; }
        }

        /// <summary>
        /// 核心函数
        /// 执行授权检查和日志记录
        /// </summary>
        /// <param name="caller">调用函数</param>
        public static void RunLog([CallerMemberName] string caller = "")
        {
            try
            {
                //检查授权
                Checker.CheckIfCanRun();
                //记录日志
                log.Information(caller);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}