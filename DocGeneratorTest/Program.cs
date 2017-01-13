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
            ////TestProdct(mainGenerator);
            //TestCOA(mainGenerator);
            TestMaterialOrder(mainGenerator);
            Console.WriteLine("文档在:" + mainGenerator.TargetFolder);
            Console.Read();
        }

        private static void TestCOA(GeneralGenerator mainGenerator)
        {
            IDoc<gn.Product> generator = new GeneratorCOA();
            var model = GetProductModel();
            string source = nameof(DocTemplateEnum.COA);
            string target = "COA-"+model.CompositionAbbr + "-" + model.ProductID;
            mainGenerator.Generate<gn.Product>(generator, model, source, target);

        }

        private static void TestMaterialOrder(GeneralGenerator mainGenerator)
        {
            IDoc<gn.MaterialOrder> generator = new GeneratorMaterialOrder();
            gn.MaterialOrder model = GetMaterialOrderModel();
            string source = nameof(DocTemplateEnum.MaterialOrder);
            string target = model.OrderPO;
            for (int i = 0; i < 2; i++)
            {
                mainGenerator.Generate<gn.MaterialOrder>(generator, model, source, target + "_" + i.ToString());
            }

        }

        private static gn.MaterialOrder GetMaterialOrderModel()
        {
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
                    DeliveryDate = DateTime.Now.AddDays(10 + i),
                    UnitPrice = 2300,
                    Weight = 1.6 * (i + 1)
                };
                //modelItem.Description = $"Processing fee to cast {modelItem.Purity} {modelItem.Composition} (atomic%;PMI to provide{modelItem.ProvideRawMaterial};please deliver by {modelItem.DeliveryDate.ToShortDateString()}";
                model.MaterialOrderItems.Add(modelItem);
            }

            return model;
        }

        private static void TestProdct(GeneralGenerator mainGenerator)
        {
            IDoc<gn.Product> generator = new GeneratorProduct();
            gn.Product model = GetProductModel();

            string source = nameof(DocTemplateEnum.Product);
            string target ="Product-"+ model.CompositionAbbr + "-" + model.ProductID;
            mainGenerator.Generate<gn.Product>(generator, model, source, target);

        }

        private static gn.Product GetProductModel()
        {
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
            
            if (true)
            {
                model.CompositionXRF = @"No.,Cu atm%,In atm%,Ga atm%,Se atm%
1,23.21,20.19,7.16,49.44
2,23.08,20.01,7.27,49.64
3,22.41,19.86,6.92,50.82
4,22.49,20.00,7.08,50.43
5,22.29,19.97,7.30,50.45
Average,22.69,20.01,7.14,50.15";
            }
            else
            {
                model.CompositionXRF = "含S，无法测试";
            }
            model.CreateTime = DateTime.Now;
            model.Creator = "xs.zhou";
            return model;
        }
    }
}
