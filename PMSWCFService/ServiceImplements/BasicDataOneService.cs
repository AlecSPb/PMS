using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using PMSDAL;
using AutoMapper;



namespace PMSWCFService
{
    public partial class PMSService : ICustomerService, IDeliveryAddressService,
        IVHPDeviceService, IVHPMoldService, IVHPProcessService,
        ICompoundService

    {
        public int AddCompound(DcBDCompound model)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDCompound, BDCompound>());
                var newModel = Mapper.Map<BDCompound>(model);

                dc.Comounds.Add(newModel);
                result = dc.SaveChanges();
            }

            return result;
        }

        public int AddCustomer(DcBDCustomer model)
        {
            throw new NotImplementedException();
        }

        public int AddDeliveryAddress(DcBDDeliveryAddress model)
        {
            throw new NotImplementedException();
        }

        public int AddVHPDevice(DcBDVHPDevice model)
        {
            throw new NotImplementedException();
        }

        public int AddVHPMold(DcBDVHPProcess model)
        {
            throw new NotImplementedException();
        }

        public int AddVHPMold(DcBDVHPMold model)
        {
            throw new NotImplementedException();
        }

        public int DeleteCompound(DcBDCompound model)
        {
            throw new NotImplementedException();
        }

        public int DeleteCustomer(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteDeliveryAddress(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteVHPDevice(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteVHPMold(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteVHPProcess(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<DcBDCompound> GetAllCompounds()
        {
            throw new NotImplementedException();
        }

        public List<DcBDCustomer> GetCustomer()
        {
            throw new NotImplementedException();
        }

        public List<DcBDDeliveryAddress> GetDeliveryAddress()
        {
            throw new NotImplementedException();
        }

        public List<DcBDVHPDevice> GetVHPDevice()
        {
            throw new NotImplementedException();
        }

        public List<DcBDVHPMold> GetVHPMold()
        {
            throw new NotImplementedException();
        }

        public List<DcBDVHPProcess> GetVHPProcess()
        {
            throw new NotImplementedException();
        }

        public int UpdateCompound(DcBDCompound model)
        {
            throw new NotImplementedException();
        }

        public int UpdateCustomer(DcBDCustomer model)
        {
            throw new NotImplementedException();
        }

        public int UpdateDeliveryAddress(DcBDDeliveryAddress model)
        {
            throw new NotImplementedException();
        }

        public int UpdateVHPDevice(DcBDVHPDevice model)
        {
            throw new NotImplementedException();
        }

        public int UpdateVHPMold(DcBDVHPMold model)
        {
            throw new NotImplementedException();
        }

        public int UpdateVHPProcess(DcBDVHPProcess model)
        {
            throw new NotImplementedException();
        }
    }
}