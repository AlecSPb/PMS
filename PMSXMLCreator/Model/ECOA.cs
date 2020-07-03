using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSXMLCreator.Service;

namespace PMSXMLCreator
{
    public class ECOA
    {
        public ECOA()
        {
            CurrentSpec = new ULVACSpecs();
            IsNew = true;
            ResponsiblePartyEmail = "xs.zhou@cdpmi.net";

            ManufacturerNumber = "109575";
            ManufacturerName = "Pioneer Materials,Inc.";
            ManufacturerPlantCode = "00";
            IncomingFaxNumber = "+86-028-66515927";

            ThisDocumentGenerationDateTime = DateTime.Now;
            ReleaseType = "00";

            DeliverTo = "TCB";
            ScheduledShipDate = DateTime.Today.AddDays(3);
            ActualShipDate = DateTime.Today.AddDays(3);
            Containers = "wood box#ups";
            ContainerNumber = "unknown";
            ShipmentNumber = "unknown";
            Comment = "This is a Quality Certificate";


            ProductName = "";
            ManufacturerPartNumber = "";
            ManufacturerOrderNumber = "";
            PartNumber = "#";
            PartRevisionNumber = "#";
            PartNumberDesc = "#";
            LotCreatedDate = DateTime.Now;
            LotNumber = "";

            Density = "0";
            Weight = "0";

            TargetDimension = "440mm OD x 4.0mm";
            PlateSpec = Helper.Plate;

            XRF = Helper.XRF;
            GDMS = Helper.GDMS;
            VPI = Helper.VPI;

            AMLStatus = "ENGINEERING";
            AMLNotes = "none";
            EHSNumber = "none";
            ContainerSize = "0";
            ContainerWeight = "0";
            BackPlateNumber = "none";
            SelfLife = "12 months";
        }
        public ISpecs CurrentSpec { get; set; }
        public bool IsNew { get; set; }

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
        public string ContainerNumber { get; set; }
        public string ShipmentNumber { get; set; }

        public string AMLStatus { get; set; }
        public string AMLNotes { get; set; }
        public string EHSNumber { get; set; }
        public string ContainerSize { get; set; }
        public string ContainerWeight { get; set; }
        public string SelfLife { get; set; }

        #endregion

        #region Comment
        public string Comment { get; set; }

        #endregion

        #region TargetInfo
        public string ProductName { get; set; }
        public string ManufacturerPartNumber { get; set; }//Part Nbr
        public string ManufacturerOrderNumber { get; set; }//NBr
        public string PartNumber { get; set; }//Nbr
        public string PartRevisionNumber { get; set; }//Rev
        public string PartNumberDesc { get; set; }//Desc


        public DateTime LotCreatedDate { get; set; }
        public string LotNumber { get; set; }
        public string BackPlateNumber { get; set; }

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
