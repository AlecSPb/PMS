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

        private void Trace(DcDelivery[] models)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();

            //分别实现不同快递公司的服务接口
            foreach (var item in models)
            {
                sb.AppendLine($"【{item.DeliveryName}】查询结果:");

                string express = item.DeliveryExpress;
                string[] numbers = item.DeliveryNumber.Split(new string[] { "+" }, StringSplitOptions.RemoveEmptyEntries);

                TraceNumber(express, numbers, sb);

            }

            var msgbox = new ToolDialog.ExpressTrack();
            msgbox.TrackInfo = sb.ToString();
            msgbox.ShowDialog();
        }

        private void TraceNumber(string express, string[] numbers, StringBuilder sb)
        {
            var api = new ApiVisit();
            foreach (var number in numbers)
            {
                switch (express)
                {
                    case Shipper.SF:
                        {
                            //不支持其他途径下单的顺丰单号查询
                            //string json = api.GetOrderTracesByJson(new Request("", Shipper.SF, number));
                            //string ok_str = ProcessResponce(json);
                            //sb.AppendLine(ok_str);
                            new SF().SFOrder();
                            //sb.AppendLine($"暂时搞不定顺丰查询，请使用手工查询{number}");
                        }
                        break;
                    case Shipper.UPS:
                        {
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
            sb.Append(response.ShipperCode);
            sb.Append(":");
            sb.AppendLine(response.LogisticCode);
            sb.AppendLine("----------");
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
