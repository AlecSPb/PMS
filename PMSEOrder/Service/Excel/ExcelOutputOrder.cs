using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSEOrder.Service;
using XSHelper;

namespace PMSEOrder.Service.Excel
{
    public class ExcelOutputOrder : ExcelOutputBaseSimple
    {
        public override void Output()
        {
            var service = new DataService();
            var allorders = service.GetAllOrder();
            NPOIHelper helper = new NPOIHelper();
            helper.CreateNew(sheetName);

            string[] titles = {
                                    "GUID",
                                    "CustomerName",
                                    "PO",
                                    "Composition",
                                    "CompositionDetail",
                                    "ProductType",
                                    "Purity",
                                    "Quantity",
                                    "QuantityUnit",
                                    "Dimension",
                                    "DimensionDetails",
                                    "Drawing",
                                    "SampleNeed",
                                    "SampleNeedRemark",
                                    "SampleForAnlysis",
                                    "SampleForAnlysisRemark",
                                    "DeadLine",
                                    "MinimumAcceptDefect",
                                    "ShipTo",
                                    "WithBackingPlate",
                                    "PlateDrawing",
                                    "SpecialRequirement",
                                    "BondingRequirement",
                                    "PartNumber",
                                    "Remark",
                                    "Creator",
                                    "CreateTime",
                                    "OrderState"
                };

            helper.AddRowTitle(titles);

            int rowIndex = 1;

            foreach (var item in allorders)
            {
                int column_index = 0;
                helper.CreateRow(rowIndex);
                helper.CreateAndSetCell(column_index++, item.GUIDID.ToString());
                helper.CreateAndSetCell(column_index++, item.CustomerName ?? "");
                helper.CreateAndSetCell(column_index++, item.PO ?? "");
                helper.CreateAndSetCell(column_index++, item.Composition ?? "");
                helper.CreateAndSetCell(column_index++, item.CompositionDetail ?? "");
                helper.CreateAndSetCell(column_index++, item.ProductType ?? "");
                helper.CreateAndSetCell(column_index++, item.Purity ?? "");
                helper.CreateAndSetCell(column_index++, item.Quantity.ToString());
                helper.CreateAndSetCell(column_index++, item.QuantityUnit ?? "");
                helper.CreateAndSetCell(column_index++, item.Dimension ?? "");
                helper.CreateAndSetCell(column_index++, item.DimensionDetails ?? "");
                helper.CreateAndSetCell(column_index++, item.Drawing ?? "");
                helper.CreateAndSetCell(column_index++, item.SampleNeed ?? "");
                helper.CreateAndSetCell(column_index++, item.SampleNeedRemark ?? "");
                helper.CreateAndSetCell(column_index++, item.SampleForAnlysis ?? "");
                helper.CreateAndSetCell(column_index++, item.SampleForAnlysisRemark ?? "");
                helper.CreateAndSetCell(column_index++, item.DeadLine.ToString());
                helper.CreateAndSetCell(column_index++, item.MinimumAcceptDefect ?? "");
                helper.CreateAndSetCell(column_index++, item.ShipTo ?? "");
                helper.CreateAndSetCell(column_index++, item.WithBackingPlate ?? "");
                helper.CreateAndSetCell(column_index++, item.PlateDrawing?? "");
                helper.CreateAndSetCell(column_index++, item.SpecialRequirement ?? "");
                helper.CreateAndSetCell(column_index++, item.BondingRequirement ?? "");
                helper.CreateAndSetCell(column_index++, item.PartNumber ?? "");
                helper.CreateAndSetCell(column_index++, item.Remark ?? "");
                helper.CreateAndSetCell(column_index++, item.Creator ?? "");
                helper.CreateAndSetCell(column_index++, item.CreateTime.ToString());
                helper.CreateAndSetCell(column_index++, item.OrderState ?? "");


                rowIndex++;
            }

            helper.Save(excelFileName);
            XS.MessageBox.ShowInfo($"{excelFileName} has been created","info");

            CheckOpenAfterCreate();

        }
    }
}
