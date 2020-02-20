using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using System.Security.Cryptography;
using CommonHelper;

namespace PMSXMLCreator.Service
{
    /// <summary>
    /// 这个类的目的是为了实现对当前编辑对象进行自动保存
    /// </summary>
    public static class AutoSave
    {
        /// <summary>
        /// 保存当前ECOA model
        /// </summary>
        /// <param name="currentCOA"></param>
        public static void SaveCurrentJson(ECOA currentCOA)
        {
            if (currentCOA == null) return;
            string json = JsonConvert.SerializeObject(currentCOA);
            string filename = "temp_current_model.json";
            File.WriteAllText(filename, json);
        }

        /// <summary>
        /// 对比当前ECOA model和之前保存的json
        /// </summary>
        /// <param name="currentCOA"></param>
        /// <param name="temp_current_model"></param>
        public static bool CompareJsonHash(ECOA currentCOA, string temp_current_model = "temp_current_model.json")
        {
            if (currentCOA == null) return false;

            string str_current, str_saved;
            str_current = JsonConvert.SerializeObject(currentCOA);
            str_saved = File.ReadAllText(temp_current_model);
            string hash_current = HashHelper.GetMD5String(str_current);
            string hash_saved = HashHelper.GetMD5String(str_saved);
            return hash_current == hash_saved;
        }


    }
}
