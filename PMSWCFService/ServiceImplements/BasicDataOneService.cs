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

                dc.Compounds.Add(newModel);
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

        public int DeleteCompound(Guid id)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                var model = dc.Compounds.Find(id);
                dc.Compounds.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int DeleteCustomer(Guid id)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                var model = dc.Customers.Find(id);
                dc.Customers.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int DeleteDeliveryAddress(Guid id)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                var model = dc.DeliveryAddresses.Find(id);
                dc.DeliveryAddresses.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int DeleteVHPDevice(Guid id)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                var model = dc.VHPDevices.Find(id);
                dc.VHPDevices.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int DeleteVHPMold(Guid id)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                var model = dc.VHPMolds.Find(id);
                dc.VHPMolds.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int DeleteVHPProcess(Guid id)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                var model = dc.VHPProcesses.Find(id);
                dc.VHPProcesses.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public List<DcBDCompound> GetAllCompounds()
        {
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDCompound, DcBDCompound>());
                var model = dc.Compounds.OrderBy(o => o.CreateTime).ToList();
                return Mapper.Map<List<BDCompound>, List<DcBDCompound>>(model);
            }
        }

        public List<DcBDCustomer> GetCustomer()
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDCustomer, DcBDCustomer>());
                var model = dc.Customers.ToList();
                return Mapper.Map<List<BDCustomer>, List<DcBDCustomer>>(model);
            }
        }

        public List<DcBDDeliveryAddress> GetDeliveryAddress()
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDDeliveryAddress, DcBDDeliveryAddress>());
                var model = dc.DeliveryAddresses.ToList();
                return Mapper.Map<List<BDDeliveryAddress>, List<DcBDDeliveryAddress>>(model);
            }
        }

        public List<DcBDVHPDevice> GetVHPDevice()
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDVHPDevice, DcBDVHPDevice>());
                var model = dc.VHPDevices.ToList();
                return Mapper.Map<List<BDVHPDevice>, List<DcBDVHPDevice>>(model);
            }
        }

        public List<DcBDVHPMold> GetVHPMold()
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDVHPMold, DcBDVHPMold>());
                var model = dc.VHPMolds.ToList();
                return Mapper.Map<List<BDVHPMold>, List<DcBDVHPMold>>(model);
            }
        }

        public List<DcBDVHPProcess> GetVHPProcess()
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDVHPProcess, DcBDVHPProcess>());
                var model = dc.VHPProcesses.ToList();
                return Mapper.Map<List<BDVHPProcess>, List<DcBDVHPProcess>>(model);
            }
        }

        public int UpdateCompound(DcBDCompound model)
        {
            int result = 0;
            using (var dc=new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDCompound, BDCompound>());
                var newModel = Mapper.Map<BDCompound>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;

                result = dc.SaveChanges();
            }
            return result;
        }

        public int UpdateCustomer(DcBDCustomer model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDCustomer, BDCustomer>());
                var newModel = Mapper.Map<BDCustomer>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
            }
            return result;
        }

        public int UpdateDeliveryAddress(DcBDDeliveryAddress model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDDeliveryAddress, BDDeliveryAddress>());
                var newModel = Mapper.Map<BDDeliveryAddress>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
            }
            return result;
        }

        public int UpdateVHPDevice(DcBDVHPDevice model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPDevice, BDVHPDevice>());
                var newModel = Mapper.Map<BDVHPDevice>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
            }
            return result;
        }

        public int UpdateVHPMold(DcBDVHPMold model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPMold, BDVHPMold>());
                var newModel = Mapper.Map<BDVHPMold>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
            }
            return result;
        }

        public int UpdateVHPProcess(DcBDVHPProcess model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPProcess, BDVHPProcess>());
                var newModel = Mapper.Map<BDVHPProcess>(model);
                dc.Entry(newModel).State = System.Data.Entity.EntityState.Modified;
                result = dc.SaveChanges();
            }
            return result;
        }
    }
}