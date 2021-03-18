using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSClient.MainService;
using Newtonsoft.Json;
using PMSClient.Components.PMSSettingHelper;

namespace PMSClient.Express
{
    public class Operation
    {


        public Operation()
        {
            Sender = PMSSettingService.ReadKeyFromCache("sf_sender");
            SenderPhone = PMSSettingService.ReadKeyFromCache("sf_sender_phone");
        }
        public string Sender { get; set; } = "秦雪梅";
        public string SenderPhone { get; set; } = "13808071935";
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
                        if (!PMSDialogService.ShowYesNo("请问", "确定要查看[快递追踪]信息吗？Y=查看，N=跳过"))
                        {
                            return;
                        }
                        Trace(result);
                    }
                }
            }
            catch (Exception ex)
            {
                PMSDialogService.ShowWarning(ex.Message);
            }
        }

        public async void Trace(DcDelivery[] models)
        {
            var express_track = new ToolDialog.ExpressTrack();
            express_track.Tip = "需要自动查询的物流记录请设置状态为【未完成】 绿色，不需要则设置为其他状态；目前只支持UPS和SF";
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



        public void TraceNumber(string express, string[] numbers, StringBuilder sb)
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
                            //通过顺丰丰桥来查询
                            //string result = sf.SFOrder(number);
                            //sb.AppendLine($"查询的SF单号为{number}");
                            //sb.AppendLine($"此单按照发件人为{sf.Sender}-{sf.SenderPhone}来查询，如有变化，联系管理员");
                            //sb.AppendLine("--------------------------------------------------------");
                            //sb.AppendLine(checker.ConcatErrorMessage(checker.CheckSF(number)));
                            //sb.AppendLine(result);


                            //通过快递鸟来查询SF
                            sb.Append("查询单号");
                            sb.Append(express);
                            sb.Append(":");
                            sb.AppendLine(number);
                            sb.AppendLine("--------------------------------------------------------");
                            sb.AppendLine($"此单按照发件人为{Sender}-{SenderPhone}来查询，如有变化，联系管理员");

                            sb.AppendLine(checker.ConcatErrorMessage(checker.CheckSF(number)));

                            string last4Digital = SenderPhone.Substring(SenderPhone.Length - 4, 4);

                            string json = api.GetOrderTracesByJson(new Request("", last4Digital, Shipper.SF, number));
                            string ok_str = ProcessResponce(json);
                            sb.AppendLine(ok_str);



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
                    sb.AppendLine("快递接口没有返回任何追踪信息");
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
