using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.PMSSettings;

namespace PMSClient.Components.PMSSettingHelper
{

    public class PMSSettingService
    {
		/// <summary>
		/// 直接从数据库PMSSetting表中读取配置
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
        public static string ReadKey(string key)
        {
			try
			{
				using (var s=new PMSSettingServiceClient())
				{
					return s.GetValueByKey(key);
				}
			}
			catch (Exception ex)
			{
				PMSHelper.CurrentLog.Error(ex);
				return "";
			}
        }
		/// <summary>
		/// 从本地缓存读取配置
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		private static Dictionary<string, string> settingCache = new Dictionary<string, string>();
		public static string ReadKeyFromCache(string key)
		{
			try
			{
				if (settingCache.ContainsKey(key))
				{
					return settingCache[key];
				}
				else
				{
					return "";
				}
			}
			catch (Exception ex)
			{
				PMSHelper.CurrentLog.Error(ex);
				return "";
			}
		}

		/// <summary>
		/// 缓存配置到内存
		/// PMS启动的时候执行
		/// </summary>
		/// <returns></returns>
		public static void CacheSettings()
		{
			try
			{
				settingCache.Clear();
				string key = "";
				using (var s = new PMSSettingServiceClient())
				{
					key = "sf_sender";
					settingCache.Add(key, s.GetValueByKey(key));
					key = "sf_sender_phone";
					settingCache.Add(key, s.GetValueByKey(key));
					key = "bonding_ok_rate";
					settingCache.Add(key, s.GetValueByKey(key));
					key = "history_log_count";
					settingCache.Add(key, s.GetValueByKey(key));
				}
			}
			catch (Exception ex)
			{
				PMSHelper.CurrentLog.Error(ex);
			}
		}
    }
}
