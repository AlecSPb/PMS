using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSQuotation.Models;
using XSHelper;
using System.IO;
using Xceed.Words;
using Xceed.Words.NET;
using Xceed.Document;

using PMSQuotation.Services;

namespace PMSQuotation.Docs
{
    public class DocService
    {
        public DocService(Quotation model, DocOptions options)
        {
            this.model = model;
            this.options = options;



            string source_folder = XS.File.GetCurrentFolderPath("Docs");
            sourceFile_en = Path.Combine(source_folder, "quotation_template_en.docx");
            sourceFile_zh = Path.Combine(source_folder, "quotation_template_zh.docx");
            tempFile = Path.Combine(source_folder, "temp.docx");
            string target_folder = XS.File.GetDesktopPath();
            targetFile = Path.Combine(target_folder, $"{model.Lot}.docx");


            db_service = new QuotationDbService();
            calc_service = new CalculationService();

        }

        private Quotation model;
        private DocOptions options;

        private string targetFile;
        private string sourceFile_en;
        private string sourceFile_zh;
        private string tempFile;

        private QuotationDbService db_service;
        private CalculationService calc_service;

        private string currencySymbol;
        /// <summary>
        /// 创建文档
        /// </summary>
        public void CreateDocument()
        {
            if (model.CurrencyType == "RMB")
            {
                currencySymbol = "￥";
            }
            else
            {
                currencySymbol = "$";
            }


            if (options.DocType == "English")
            {
                CreateEn();
            }
            else
            {
                CreateZh();
            }
            XS.MessageBox.ShowInfo("Success,Check On Desktop");

            System.Diagnostics.Process.Start(targetFile);
        }

        private void CreateEn()
        {
            File.Copy(sourceFile_en, tempFile, true);

            using (var doc = DocX.Load(tempFile))
            {
                doc.ReplaceText("[CreateTime]", model.CreateTime.ToString("yyyy-MM-dd"));
                doc.ReplaceText("[ExpirationTime]", model.ExpirationTime.ToString("yyyy-MM-dd"));
                doc.ReplaceText("[Lot]", model.Lot ?? "");

                doc.ReplaceText("[Remark]", model.Remark ?? "");

                var contact_customer = GetContactBySplit(model.ContactInfo_Customer);
                var contact_self = GetContactBySplit(model.ContactInfo_Self);

                doc.ReplaceText("[CustomerCompanyName]", contact_customer.CompanyName ?? "");
                doc.ReplaceText("[CustomerContactPerson]", contact_customer.ContactPerson ?? "");
                doc.ReplaceText("[CustomerPhone]", contact_customer.Phone ?? "");
                doc.ReplaceText("[CustomerEmail]", contact_customer.Email ?? "");
                doc.ReplaceText("[CustomerAddress]", contact_customer.Address ?? "");

                doc.ReplaceText("[SelfCompanyName]", contact_self.CompanyName ?? "");
                doc.ReplaceText("[SelfContactPerson]", contact_self.ContactPerson ?? "");
                doc.ReplaceText("[SelfPhone]", contact_self.Phone ?? "");
                doc.ReplaceText("[SelfEmail]", contact_self.Email ?? "");
                doc.ReplaceText("[SelfAddress]", contact_self.Address ?? "");

                var items = db_service.GetQuotationItems(model.ID);

                var table = doc.Tables[1];

                if (items.Count > 0)
                {
                    int row_number = 1;
                    foreach (var item in items)
                    {
                        var specification = GetSpecificationBySplit(item.Specification);

                        var currentrow = table.InsertRow(row_number);
                        currentrow.Cells[0].Paragraphs[0].Append(row_number.ToString())
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[1].Paragraphs[0].Append(item.Composition ?? "");
                        currentrow.Cells[2].Paragraphs[0].Append(specification.Purity ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center; ;
                        currentrow.Cells[3].Paragraphs[0].Append(specification.Dimension ?? "");
                        currentrow.Cells[4].Paragraphs[0].Append(specification.Plate ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[5].Paragraphs[0].Append(specification.Bonding ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[6].Paragraphs[0].Append(item.Quantity.ToString() ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[7].Paragraphs[0].Append($"{currencySymbol}{item.UnitPrice.ToString("F2") ?? ""}")
                            .Alignment = Xceed.Document.NET.Alignment.right;
                        currentrow.Cells[8].Paragraphs[0].Append($"{currencySymbol}{item.TotalPrice.ToString("F2") ?? ""}")
                            .Alignment = Xceed.Document.NET.Alignment.right;
                        currentrow.Cells[9].Paragraphs[0].Append(item.DeliveryTime ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        row_number++;
                    }
                }

                var calculation_result = calc_service.Calculate(model);

                double target_fee = calculation_result.TargetFee;
                double extra_fee = calculation_result.ExtraFee;
                double tax_fee = calculation_result.TaxFee;
                double total_fee = calculation_result.TargetFee + calculation_result.ExtraFee + calculation_result.TaxFee;


                doc.ReplaceText("[TargetFee]", $"{currencySymbol}{target_fee.ToString("F2")}" ?? "");
                doc.ReplaceText("[ExtraFee]", $"{currencySymbol}{extra_fee.ToString("F2")}" ?? "");
                doc.ReplaceText("[TaxFee]", $"{currencySymbol}{tax_fee.ToString("F2")}" ?? "");
                doc.ReplaceText("[TotalFee]", $"{currencySymbol}{total_fee.ToString("F2")}" ?? "");

                string money_capital = GetRMBCaptical(total_fee);

                doc.ReplaceText("[RMBCapital]", $"{money_capital}" ?? "");

                doc.Save();
            }
            File.Copy(tempFile, targetFile, true);
        }

        private void CreateZh()
        {
            File.Copy(sourceFile_zh, tempFile, true);

            using (var doc = DocX.Load(tempFile))
            {
                doc.ReplaceText("[CreateTime]", model.CreateTime.ToString("yyyy-MM-dd"));
                doc.ReplaceText("[ExpirationTime]", model.ExpirationTime.ToString("yyyy-MM-dd"));
                doc.ReplaceText("[Lot]", model.Lot ?? "");

                doc.ReplaceText("[Remark]", model.Remark ?? "");

                var contact_customer = GetContactBySplit(model.ContactInfo_Customer);
                var contact_self = GetContactBySplit(model.ContactInfo_Self);

                doc.ReplaceText("[CustomerCompanyName]", contact_customer.CompanyName ?? "");
                doc.ReplaceText("[CustomerContactPerson]", contact_customer.ContactPerson ?? "");
                doc.ReplaceText("[CustomerPhone]", contact_customer.Phone ?? "");
                doc.ReplaceText("[CustomerEmail]", contact_customer.Email ?? "");
                doc.ReplaceText("[CustomerAddress]", contact_customer.Address ?? "");

                doc.ReplaceText("[SelfCompanyName]", contact_self.CompanyName ?? "");
                doc.ReplaceText("[SelfContactPerson]", contact_self.ContactPerson ?? "");
                doc.ReplaceText("[SelfPhone]", contact_self.Phone ?? "");
                doc.ReplaceText("[SelfEmail]", contact_self.Email ?? "");
                doc.ReplaceText("[SelfAddress]", contact_self.Address ?? "");

                var items = db_service.GetQuotationItems(model.ID);

                var table = doc.Tables[1];

                if (items.Count > 0)
                {
                    int row_number = 1;
                    foreach (var item in items)
                    {
                        var specification = GetSpecificationBySplit(item.Specification);

                        var currentrow = table.InsertRow(row_number);
                        currentrow.Cells[0].Paragraphs[0].Append(row_number.ToString())
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[1].Paragraphs[0].Append(item.Composition ?? "");
                        currentrow.Cells[2].Paragraphs[0].Append(specification.Purity ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center; ;
                        currentrow.Cells[3].Paragraphs[0].Append(specification.Dimension ?? "");
                        currentrow.Cells[4].Paragraphs[0].Append(specification.Plate ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[5].Paragraphs[0].Append(specification.Bonding ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[6].Paragraphs[0].Append(item.Quantity.ToString() ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        currentrow.Cells[7].Paragraphs[0].Append($"{currencySymbol}{item.UnitPrice.ToString("F2") ?? ""}")
                            .Alignment = Xceed.Document.NET.Alignment.right;
                        currentrow.Cells[8].Paragraphs[0].Append($"{currencySymbol}{item.TotalPrice.ToString("F2") ?? ""}")
                            .Alignment = Xceed.Document.NET.Alignment.right;
                        currentrow.Cells[9].Paragraphs[0].Append(item.DeliveryTime ?? "")
                            .Alignment = Xceed.Document.NET.Alignment.center;
                        row_number++;
                    }
                }

                var calculation_result = calc_service.Calculate(model);

                double target_fee = calculation_result.TargetFee;
                double extra_fee = calculation_result.ExtraFee;
                double tax_fee = calculation_result.TaxFee;
                double total_fee = calculation_result.TargetFee + calculation_result.ExtraFee + calculation_result.TaxFee;


                doc.ReplaceText("[TargetFee]", $"{currencySymbol}{target_fee.ToString("F2")}" ?? "");
                doc.ReplaceText("[ExtraFee]", $"{currencySymbol}{extra_fee.ToString("F2")}" ?? "");
                doc.ReplaceText("[TaxFee]", $"{currencySymbol}{tax_fee.ToString("F2")}" ?? "");
                doc.ReplaceText("[TotalFee]", $"{currencySymbol}{total_fee.ToString("F2")}" ?? "");

                string money_capital = GetRMBCaptical(total_fee);


                doc.ReplaceText("[RMBCapital]", $"{money_capital}" ?? "");


                doc.Save();
            }
            File.Copy(tempFile, targetFile, true);
        }


        private Contact GetContactBySplit(string str)
        {
            string[] strs = str.Split(new string[] { "+" }, StringSplitOptions.None);
            Contact model = new Contact();
            if (strs.Length >= 5)
            {
                model.CompanyName = strs[0];
                model.ContactPerson = strs[1];
                model.Phone = strs[2];
                model.Email = strs[3];
                model.Address = strs[4];
            }
            return model;
        }
        private Specification GetSpecificationBySplit(string str)
        {
            string[] strs = str.Split(new string[] { "+" }, StringSplitOptions.None);
            Specification model = new Specification();
            if (strs.Length >= 5)
            {
                model.Dimension = strs[0];
                model.Purity = strs[1];
                model.Plate = strs[2];
                model.Bonding = strs[3];
                model.Other = strs[4];
            }
            return model;
        }


        private string GetRMBCaptical(double total)
        {
            if (model.CurrencyType == "RMB")
            {
                decimal money = (decimal)total;
                string s = Services.RMB.RmbTools.ConvertToChinese(money);
                return s;
            }
            return "";
        }
    }
}
