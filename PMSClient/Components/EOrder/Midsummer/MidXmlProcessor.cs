using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Diagnostics;

namespace PMSClient.Components.EOrder.Midsummer
{
    /// <summary>
    /// 用来处理Midsummer的Xml订单文件
    /// </summary>
    public class MidXmlProcessor
    {
        /// <summary>
        /// 解析xml为基本模型，存储所有信息
        /// xml有不规范的情形
        /// </summary>
        /// <param name="xml_filename"></param>
        /// <returns></returns>
        public MidModel Anlysis(string xml_filename)
        {
            if (!File.Exists(xml_filename))
            {
                return null;
            }
            MidModel model = new MidModel();

            //使用XmlDocument DOM的方式随机访问
            XmlDocument doc = new XmlDocument();
            doc.Load(xml_filename);

            //读取客户信息
            var root = doc.GetElementsByTagName("ORDERS420").Item(0);
            model.SoftwareManufacturer = root.Attributes["SoftwareManufacturer"].Value;
            model.OrderNumber = root.Attributes["OrderNumber"].Value;

            var supplier = doc.GetElementsByTagName("Supplier").Item(0);
            model.SupplierCodeEdi = supplier.Attributes["SupplierCodeEdi"].Value;
            if (supplier.ChildNodes.Count >= 6)
            {
                model.Supplier_Name = supplier.ChildNodes.Item(0).InnerText;
                model.Supplier_StreetBox1 = supplier.ChildNodes.Item(1).InnerText;
                model.Supplier_StreatBox2 = supplier.ChildNodes.Item(2).InnerText;
                model.Supplier_ZipCity1 = supplier.ChildNodes.Item(3).InnerText;
                model.Supplier_ZipCity2 = supplier.ChildNodes.Item(4).InnerText;
                model.Supplier_Country = supplier.ChildNodes.Item(5).InnerText;
            }

            var buyer = doc.GetElementsByTagName("Buyer").Item(0);
            if (buyer.ChildNodes.Count == 6)
            {
                model.Buyer_Name = buyer.ChildNodes.Item(0).InnerText;
                model.Buyer_StreetBox1 = buyer.ChildNodes.Item(1).InnerText;
                model.Buyer_StreatBox2 = buyer.ChildNodes.Item(2).InnerText;
                model.Buyer_ZipCity1 = buyer.ChildNodes.Item(3).InnerText;
                model.Buyer_ZipCity2 = buyer.ChildNodes.Item(4).InnerText;
                model.Buyer_Country = buyer.ChildNodes.Item(5).InnerText;
            }

            model.OrderDate = DateTime.Parse(doc.GetElementsByTagName("OrderDate").Item(0).InnerText);
            model.TermsOfPaymentDays = int.Parse(doc.GetElementsByTagName("TermsOfPaymentDays").Item(0).InnerText);
            model.Currency = doc.GetElementsByTagName("Currency").Item(0).InnerText;

            //读取Row
            var rows = doc.GetElementsByTagName("Row");
            foreach (XmlNode row in rows)
            {
                MidModelItem item = new MidModelItem();
                item.RowNumber = int.Parse(row.Attributes["RowNumber"].Value);
                item.RowType = row.Attributes["RowType"].Value;

                if (row.ChildNodes.Count == 10)
                {
                    item.PartNumber = row.ChildNodes[0].Attributes["PartNumber"].Value;
                    item.SupplierPartNumber = row.ChildNodes[0].Attributes["SupplierPartNumber"].Value;

                    item.Text = row.ChildNodes[1].InnerText;
                    item.ReferenceNumber = row.ChildNodes[2].InnerText;
                    item.Quantity = int.Parse(row.ChildNodes[3].InnerText);
                    item.Unit = row.ChildNodes[4].InnerText;
                    item.DeliveryPeriod = DateTime.Parse(row.ChildNodes[5].InnerText);
                    item.Each = row.ChildNodes[6].InnerText;
                    item.Discount = row.ChildNodes[7].InnerText;
                    item.Setup = row.ChildNodes[8].InnerText;
                    item.Alloy = row.ChildNodes[9].InnerText;
                }
                model.Items.Add(item);
            }


            return model;
        }

        /// <summary>
        /// 返回可读字符串
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string ModelToString(MidModel model)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{model.SoftwareManufacturer}");

            foreach (var item in model.Items)
            {
                sb.Append($"{item.RowNumber} ");
                sb.AppendLine($"{item.RowType}");
            }

            var result = model.Items.GroupBy(i => i.RowNumber);
            foreach (var group in result)
            {
                sb.AppendLine(group.Key.ToString());
            }
            return sb.ToString();
        }

        /// <summary>
        /// 返回能用于PMS的有用模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public List<MidOrder> GetGoodOrder(MidModel model)
        {
            if (model == null) return null;
            List<MidOrder> orders = new List<MidOrder>();


            var result = model.Items.GroupBy(i => i.RowNumber);
            foreach (var group in result)
            {
                MidOrder order = new MidOrder();
                order.OrderDate = model.OrderDate;
                order.DeliveryDate = model.OrderDate.AddDays(model.TermsOfPaymentDays);
                order.CustomerName = "Midsummer";
                order.PO = model.OrderNumber;


                order.Composition = group.ElementAt(0).Text;
                order.Quantity = group.ElementAt(0).Quantity;
                order.Remark = group.ElementAt(0).SupplierPartNumber;
                order.Dimension = group.ElementAt(1).Text;


                orders.Add(order);
            }

            return orders;
        }


    }
}
