using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PMSClient.DataProcess.ScanInput
{
    /// <summary>
    /// 扫描结果处理类基类
    /// </summary>
    public class ProcessBase
    {
        public ProcessBase()
        {
            uid = PMSHelper.CurrentSession.CurrentUser.UserName;
            Lots = new List<LotModel>();

        }

        protected string uid;

        public List<LotModel> Lots;

        /// <summary>
        /// 拆分整体多行批号到数组
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        protected string[] SplitLots(string text)
        {
            return text.Split(Environment.NewLine.ToCharArray(),
                                    StringSplitOptions.RemoveEmptyEntries);
        }
        /// <summary>
        /// 判断lot是否符合要求
        /// </summary>
        /// <param name="lot"></param>
        /// <returns></returns>
        protected bool CheckValid(string lot)
        {
            string pattern = @"\d{6}-\w{1,2}-\w+";
            return Regex.IsMatch(lot, pattern, RegexOptions.IgnoreCase);
        }

        protected void CheckIsStandard(LotModel item)
        {
            if (item.IsValid)
            {
                if (!CheckValid(item.Lot.Trim()))
                {
                    item.IsValid = false;
                    item.AppendMessage("格式无效");
                }
            }
        }
        /// <summary>
        /// 返回数据源类型
        /// </summary>
        /// <param name="lot"></param>
        /// <returns></returns>
        protected TableSource GetDSType(string lot)
        {

            if (lot.Length >= 9)
            {
                //180116-AB-1  180616-A-1  180616-A-1A
                string pattern = @"-\w{1,2}-";
                var match = Regex.Match(lot, pattern);
                if (match.Success)
                {
                    string code = match.Value.Replace("-", "");
                    switch (code.ToUpper())
                    {
                        case "BP":
                            return TableSource.Plate;
                        default:
                            return TableSource.Test;
                    }
                }
                else
                {
                    return TableSource.Unknown;
                }
            }
            else
            {
                return TableSource.Unknown;
            }
        }

        /// <summary>
        /// 解析
        /// </summary>
        /// <param name="lot_string"></param>
        public void Intialize(string lot_string)
        {
            Lots.Clear();
            SplitLots(lot_string).ToList().ForEach(i =>
            {
                LotModel lot = new LotModel()
                {
                    Lot = i.Trim(),
                    IsValid = true,
                    HasProcessed = false
                };
                Lots.Add(lot);
            });
        }

        public string FromLotsToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var line in Lots)
            {
                sb.Append(line.IsValid);
                sb.AppendLine(" " + line.Lot);
            }
            return sb.ToString();
        }

        public virtual void Check(Action<double> DoSomething)
        {

        }

        public virtual void Process(Action<double> DoSomething)
        {

        }

    }
}
