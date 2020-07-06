using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSShipment.TCB;
using Newtonsoft.Json;
namespace PMSShipment.Express
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
                using (var service = new TCBServiceClient())
                {
                    var result = service.GetDeliveryUnFinished();

                    if (result.Length > 0)
                    {
                        if (!XSHelper.XS.MessageBox.ShowYesNo("Are you sure to see tracking info？Y=View，N=Skip", "Ask"))
                        {
                            return;
                        }
                        Trace(result);
                    }
                }
            }
            catch (Exception ex)
            {
                XSHelper.XS.MessageBox.ShowWarning(ex.Message);
            }
        }

        private async void Trace(DcDelivery[] models)
        {
            var express_track = new ToolDialog.ExpressTrack();
            express_track.Tip = "Will Track Green Ones";
            express_track.TrackInfo = await GetTraceInformationAsync(models);
            express_track.Show();
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
                    sb.AppendLine($"【{item.DeliveryName} Tracking Results】");
                    sb.AppendLine($"Shipment Denstination:{item.Country}");

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
            if (numbers.Length == 0)
            {
                sb.AppendLine("No Tracking Number");
                return;
            }
            foreach (var number in numbers)
            {
                switch (express)
                {
                    case Shipper.UPS:
                        {
                            sb.Append("Tracking Number");
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
                        sb.AppendLine($"No Impelement【{express}】Tracking Function");
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
                    sb.AppendLine("No Tracking Results");
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
