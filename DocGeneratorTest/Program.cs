using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocGenerator;
using gn = DocGenerator.DocModels;

namespace DocGeneratorTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainGenerator = new GeneralGenerator();
            //TestProdct(mainGenerator);
            TestMaterialOrder(mainGenerator);
            Console.WriteLine("文档在:" + mainGenerator.TargetFolder);
            Console.Read();
        }

        private static void TestMaterialOrder(GeneralGenerator mainGenerator)
        {
            IDoc<gn.MaterialOrder> generator = new GeneratorMaterialOrder();
            var model = new gn.MaterialOrder();
            model.ID = Guid.NewGuid();
            model.CreateTime = DateTime.Now;
            model.Creator = "leon.chiu";
            model.State = 1;
            model.Supplier = "PMS-Chengdu Casting Division";
            model.SupplierAbbr = "SJ";
            model.SupplierReceiver = "Mr. Wang";
            model.SupplierEmail = "sj_materials@163.com";
            model.SupplierAddress = "Chengdu,Sichuan CHINA";
            model.OrderPO = DateTime.Now.ToString("yyMMdd") + "_" + model.SupplierAbbr;
            model.Remark = "PMI to provide:lalalalalalalalala";
            model.ShipFee = 100;
            for (int i = 0; i < 4; i++)
            {
                var modelItem = new gn.MaterialOrderItem()
                {
                    ID = Guid.NewGuid(),
                    CreateTime = DateTime.Now,
                    Creator = "leon.chiu",
                    State = 1,
                    Composition = "Ge20As35Se45",
                    Purity = "99.995%",
                    Description = "",
                    PMIWorkNumber = DateTime.Now.AddDays(i).ToString("yyMMdd") + "-A",
                    ProvideRawMaterial = "1.6kg Cu,2.48kg In,0.47kg Ga,4.754kg Se",
                    DeliveryDate = DateTime.Now.AddDays(10+i),
                    UnitPrice = 2300,
                    Weight = 1.6 * (i + 1)
                };
                modelItem.Description = $"Processing fee to cast {modelItem.Purity} {modelItem.Composition} (atomic%;PMI to provide{modelItem.ProvideRawMaterial};please deliver by {modelItem.DeliveryDate.ToShortDateString()}";
                model.MaterialOrderItems.Add(modelItem);
            }
            string source = nameof(DocTemplateEnum.MaterialOrder);
            string target = model.OrderPO;
            for (int i = 0; i < 2; i++)
            {

                mainGenerator.Generate<gn.MaterialOrder>(generator, model, source, target + "_" + i.ToString());
            }

        }

        private static void TestProdct(GeneralGenerator mainGenerator)
        {
            IDoc<gn.Product> generator = new GeneratorProduct();
            var model = new gn.Product();
            model.ID = Guid.NewGuid();
            model.ProductID = "170110-MA-1";
            model.Composition = "Cu22.8In20Ga7Se50.2";
            model.CompositionAbbr = "CuInGaSe";
            model.PO = "123123132123";
            model.Customer = "Midsummer";
            model.Dimension = "230 OD x 4mm";
            model.DimensionActual = "230.02 OD x 4.04mm";
            model.Weight = "954.0";
            model.Density = "5.75";
            model.Resistance = "50000";
            model.CompositionXRF = "";
            model.CreateTime = DateTime.Now;
            model.Creator = "xs.zhou";

            string source = nameof(DocTemplateEnum.Product);
            string target = model.CompositionAbbr + "-" + model.ProductID;
            mainGenerator.Generate<gn.Product>(generator, model, source, target);

        }
    }
}
