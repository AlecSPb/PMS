using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace XSHelper.Helpers
{
    public class AutoSaveHelper
    {
        /// <summary>
        /// 保存当前ECOA model
        /// </summary>
        /// <param name="model"></param>
        public void SaveCurrentJson(Object model)
        {
            if (model == null) return;
            string json = JsonConvert.SerializeObject(model);
            string filename = "temp_current_model.json";
            File.WriteAllText(filename, json);
        }

        /// <summary>
        /// 对比当前ECOA model和之前保存的json
        /// </summary>
        /// <param name="model"></param>
        /// <param name="temp_current_model"></param>
        public bool CompareJsonHash(Object model, string temp_current_model = "temp_current_model.json")
        {
            //文件不存在或者当前模型对象不存在的时候，返回true
            if (model == null) return true;
            if (!File.Exists(temp_current_model)) return true;

            var hash = new HashHelper();
            string str_current, str_saved;
            str_current = JsonConvert.SerializeObject(model);
            str_saved = File.ReadAllText(temp_current_model);
            string hash_current = hash.GetMD5String(str_current);
            string hash_saved = hash.GetMD5String(str_saved);
            return hash_current == hash_saved;
        }

        /// <summary>
        /// 清理临时文件
        /// </summary>
        /// <param name="path"></param>
        public void CleanTempFile(string path = "temp_current_model.json")
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
