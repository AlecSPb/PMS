using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSXMLCreator.XMLGenerator;

namespace PMSXMLCreator
{
    public class ECOA
    {
        public ECOA()
        {
            ResponsiblePartyEmail = "xs.zhou@cdpmi.net";

            ManufacturerNumber = "######";
            ManufacturerName = "Pioneer Materials,Inc.";
            ManufacturerPlantCode = "######";
            IncomingFaxNumber = "+86-028-66515927";

            ThisDocumentGenerationDateTime = DateTime.Now;
            ReleaseType = "00";

            DeliverTo = "TCB";
            ScheduledShipDate = DateTime.Today.AddDays(14);
            ActualShipDate = DateTime.Today.AddDays(14);
            Containers = "wood box#ups";

            Comment = "Quality Certificate";


            ProductName = "";
            ManufacturerPartNumber = "";
            ManufacturerOrderNumber = "";
            PartNumber = "########";
            PartRevisionNumber = "######";
            LotCreatedDate = DateTime.Now;
            LotNumber = "";

            Density = "";
            Weight = "";

            TargetDimension = "";
            PlateSpec = Helper.Plate;

            XRF = Helper.XRF;
            GDMS = Helper.GDMS;
            VPI = Helper.VPI;
        }


        #region FileInfo
        public string ResponsiblePartyEmail { get; set; }
        #endregion

        #region SupplierInfo
        public string ManufacturerNumber { get; set; }
        public string ManufacturerName { get; set; }
        public string ManufacturerPlantCode { get; set; }
        public string IncomingFaxNumber { get; set; }
        #endregion

        #region GenerationInfo
        public DateTime ThisDocumentGenerationDateTime { get; set; }
        public string ReleaseType { get; set; }
        #endregion

        #region DeliveryInfo
        public string DeliverTo { get; set; }
        public DateTime ScheduledShipDate { get; set; }
        public DateTime ActualShipDate { get; set; }
        public string Containers { get; set; }

        #endregion

        #region Comment
        public string Comment { get; set; }

        #endregion

        #region TargetInfo
        public string ProductName { get; set; }
        public string ManufacturerPartNumber { get; set; }
        public string ManufacturerOrderNumber { get; set; }
        public string PartNumber { get; set; }
        public string PartRevisionNumber { get; set; }
        public DateTime LotCreatedDate { get; set; }
        public string LotNumber { get; set; }

        #endregion




        #region MaterialParameterInfo
        //Material Parameters
        public string Density { get; set; }
        public string Weight { get; set; }

        public string TargetDimension { get; set; }
        public string PlateSpec { get; set; }

        public string XRF { get; set; }

        //GDMS
        public string GDMS { get; set; }

        //Vapor Phase Impurities
        public string VPI { get; set; }
        #endregion




    }
}
