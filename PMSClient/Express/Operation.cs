using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Newtonsoft.Json;
namespace PMSClient.Express
{
    public class Operation
    {
        /// <summary>
        /// 追踪所有未完成的发货计划
        /// </summary>
        public void TraceUnCompleted()
        {
            try
            {
                using (var service = new DeliveryServiceClient())
                {
                    var result = service.GetDeliveryUnFinished();

                    if (result.Length > 0)
                    {
                        Trace(result);
                    }
                }
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        private async void Trace(DcDelivery[] models)
        {
            var express_track = new ToolDialog.ExpressTrack();
            express_track.Tip = "需要自动查询的物流记录请设置状态为【未完成】 绿色，不需要则设置为其他状态；目前只支持UPS和SF";
            express_track.TrackInfo = await GetTraceInformationAsync(models);
            express_track.ShowDialog();
        }

        /// <summary>
        /// 使用异步处理最费时间的地方
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        private Task<string> GetTraceInformationAsync(DcDelivery[] models)
        {
            var rs = Task<string>.Run(() =>
            {
                StringBuilder sb = new StringBuilder();
                sb.Clear();

                //分别实现不同快递公司的服务接口
                foreach (var item in models)
                {
                    sb.AppendLine($"【{item.DeliveryName}查询结果如下】");
                    sb.AppendLine($"发货目的地:{item.Country}");

                    string express = item.DeliveryExpress;
                    string[] numbers = item.DeliveryNumber.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);

                    TraceNumber(express, numbers, sb);

                }
                return sb.ToString();
            });

            return rs;
        }



        private void TraceNumber(string express, string[] numbers, StringBuilder sb)
        {
            var checker = new CheckHelper.ExpressHelper();
            var api = new KDBird();
            var sf = new SF();
            if (numbers.Length == 0)
            {
                sb.AppendLine("还没有填写单号");
                return;
            }
            foreach (var number in numbers)
            {
                switch (express)
                {
                    case Shipper.SF:
                        {
                            string result = sf.SFOrder(number);
                            sb.AppendLine($"查询的SF单号为{number}");
                            sb.AppendLine($"此单按照发件人为{sf.Sender}-{sf.SenderPhone}来查询，如有变化，联系管理员");
                            sb.AppendLine("--------------------------------------------------------");
                            sb.AppendLine(checker.ConcatErrorMessage(checker.CheckSF(number)));
                            sb.AppendLine(result);
                        }
                        break;
                    case Shipper.UPS:
                        {
                            sb.Append("查询单号");
                            sb.Append(express);
                            sb.Append(":");
                            sb.AppendLine(number);
                            sb.AppendLine("--------------------------------------------------------");
                            sb.AppendLine(checker.ConcatErrorMessage(checker.CheckUPS(number)));
                            string json = api.GetOrderTracesByJson(new Request("", Shipper.UPS, number));
                            string ok_str = ProcessResponce(json);
                            sb.AppendLine(ok_str);
                        }
                        break;
                    default:
                        sb.AppendLine($"暂未实现【{express}】快递追踪功能");
                        break;
                }
            }
        }

        private string ProcessResponce(string json)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            Response response = JsonConvert.DeserializeObject<Response>(json);
            if (!response.Success)
            {
                sb.AppendLine(response.Reason);
            }
            else
            {
                if (response.Traces.Length == 0)
                {
                    sb.AppendLine("没找到追踪记录");
                }
                else
                {
                    foreach (var item in response.Traces)
                    {
                        sb.AppendLine($"{item.AcceptTime}-{ item.AcceptStation} { item.Remark}");
                    }
                }
            }


            return sb.ToString();
        }
    }
}
