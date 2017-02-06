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
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDCustomer, BDCustomer>());
                var newModel = Mapper.Map<BDCustomer>(model);

                dc.Customers.Add(newModel);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int AddDeliveryAddress(DcBDDeliveryAddress model)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDDeliveryAddress, BDDeliveryAddress>());
                var newModel = Mapper.Map<BDDeliveryAddress>(model);

                dc.DeliveryAddresses.Add(newModel);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int AddVHPDevice(DcBDVHPDevice model)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPDevice, BDVHPDevice>());
                var newModel = Mapper.Map<BDVHPDevice>(model);
                dc.VHPDevices.Add(newModel);
                result = dc.SaveChanges();
            }

            return result;
        }

        public int AddVHPMold(DcBDVHPProcess model)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPProcess, BDVHPProcess>());
                var newModel = Mapper.Map<BDVHPProcess>(model);
                dc.VHPProcesses.Add(newModel);
                result = dc.SaveChanges();
            }

            return result;
        }

        public int AddVHPMold(DcBDVHPMold model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPMold, BDVHPMold>());
                var newModel = Mapper.Map<BDVHPMold>(model);
                dc.VHPMolds.Add(newModel);
                result = dc.SaveChanges();
            }

            return result;
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