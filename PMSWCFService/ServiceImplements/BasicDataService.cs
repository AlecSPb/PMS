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
    public class BasicDataService : ICustomerService, IDeliveryAddressService,
        IVHPDeviceService, IVHPMoldService, IVHPProcessService,
        ICompoundService, ISupplierService, IElementService

    {
        public int AddCompound(DcBDCompound model)
        {
            try
            {
                int result = 0;
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<DcBDCompound, BDCompound>());
                    var newModel = Mapper.Map<BDCompound>(model);

                    dc.Compounds.Add(newModel);
                    result = dc.SaveChanges();
                }

                return result;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddCustomer(DcBDCustomer model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
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
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDDeliveryAddress, BDDeliveryAddress>());
                var newModel = Mapper.Map<BDDeliveryAddress>(model);

                dc.DeliveryAddresses.Add(newModel);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int AddElement(DcBDElement model)
        {
            int result = 0;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDElement, BDElement>());
                var item = Mapper.Map<BDElement>(model);
                using (var dc = new PMSDbContext())
                {
                    dc.Elements.Add(item);
                    result = dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public int AddElementGroupAndItems(string groupName, List<DcBDElementGroupItem> elements)
        {
            int result = 0;
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var group = new BDElementGroup()
                    {
                        ID = Guid.NewGuid(),
                        GroupName = groupName,
                        CreateTime = DateTime.Now
                    };
                    dc.ElementGroups.Add(group);

                    Mapper.Initialize(cfg => cfg.CreateMap<DcBDElementGroupItem, BDElementGroupItem>());
                    var items = Mapper.Map<List<DcBDElementGroupItem>, List<BDElementGroupItem>>(elements);
                    dc.ElementGroupItems.AddRange(items);

                    result = dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public int AddSupplier(DcBDSupplier model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDSupplier, BDSupplier>());
                var supplier = Mapper.Map<BDSupplier>(model);
                dc.Suppliers.Add(supplier);
                result = dc.SaveChanges();
            }

            return result;
        }

        public int AddVHPDevice(DcBDVHPDevice model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPDevice, BDVHPDevice>());
                var newModel = Mapper.Map<BDVHPDevice>(model);
                dc.VHPDevices.Add(newModel);
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

        public int AddVHPProcess(DcBDVHPProcess model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDVHPProcess, BDVHPProcess>());
                var newModel = Mapper.Map<BDVHPProcess>(model);
                dc.VHPProcesses.Add(newModel);
                result = dc.SaveChanges();
            }

            return result;
        }

        public int DeleteCompound(Guid id)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
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
            using (var dc = new PMSDbContext())
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
            using (var dc = new PMSDbContext())
            {
                var model = dc.DeliveryAddresses.Find(id);
                dc.DeliveryAddresses.Remove(model);
                result = dc.SaveChanges();
            }
            return result;
        }

        public int DeleteElement(Guid id)
        {
            throw new NotImplementedException();
        }

        public int DeleteSupplier(Guid id)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                var model = dc.Suppliers.Find(id);
                dc.Suppliers.Remove(model);
                result = dc.SaveChanges();
            }

            return result;
        }

        public int DeleteVHPDevice(Guid id)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
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
            using (var dc = new PMSDbContext())
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

        public List<DcBDElementGroup> GetElementGroup()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<BDElementGroup, DcBDElementGroup>());
                    var result = dc.ElementGroups.OrderByDescending(i => i.CreateTime).ToList();
                    return Mapper.Map<List<BDElementGroup>, List<DcBDElementGroup>>(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DcBDElementGroupItem> GetElementGroupItem(Guid id)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<BDElementGroupItem, DcBDElementGroupItem>());
                    var result = dc.ElementGroupItems.Where(i => i.GroupElementID == id).OrderBy(i => i.OrderNumber).ToList();
                    return Mapper.Map<List<BDElementGroupItem>, List<DcBDElementGroupItem>>(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DcBDElement> GetElements()
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    Mapper.Initialize(cfg => cfg.CreateMap<BDElement, DcBDElement>());
                    var result = dc.Elements.OrderBy(i => i.AtomicNumber).ToList();

                    return Mapper.Map<List<BDElement>, List<DcBDElement>>(result);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<DcBDSupplier> GetSuppliers()
        {
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<BDSupplier, DcBDSupplier>());
                var model = dc.Suppliers.ToList();
                return Mapper.Map<List<BDSupplier>, List<DcBDSupplier>>(model);
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
            using (var dc = new PMSDbContext())
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

        public int UpdateElement(DcBDElement model)
        {
            int result = 0;
            try
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDElement, BDElement>());
                var item = Mapper.Map<BDElement>(model);
                using (var dc = new PMSDbContext())
                {
                    dc.Entry(item).State = System.Data.Entity.EntityState.Modified;
                    result = dc.SaveChanges();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return result;
        }

        public int UpdateSupplier(DcBDSupplier model)
        {
            int result = 0;
            using (var dc = new PMSDbContext())
            {
                Mapper.Initialize(cfg => cfg.CreateMap<DcBDSupplier, BDSupplier>());
                var supplier = Mapper.Map<BDSupplier>(model);
                dc.Entry(supplier).State = System.Data.Entity.EntityState.Modified;
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