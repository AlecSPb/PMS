using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSDAL;

namespace PMSWCFService
{
    public class EditLockService : IEditLockService
    {
        public DcEditLock CheckLock(string fingerprint)
        {
            using (var db = new PMSDbContext())
            {
                try
                {
                    Mapper.Initialize(config =>
                    {
                        config.CreateMap<EditLock, DcEditLock>();
                    });
                    var query = from l in db.EditLocker
                                where l.FingerPrint == fingerprint
                                select l;
                    var entity = query.FirstOrDefault();
                    if (entity != null)
                    {
                        return Mapper.Map<EditLock, DcEditLock>(entity);
                    }
                }
                catch (Exception)
                {

                }
                return null;
            }
        }

        public void Lock(DcEditLock model)
        {
            try
            {
                Mapper.Initialize(config =>
                {
                    config.CreateMap<DcEditLock, EditLock>();
                });
                var entity = Mapper.Map<DcEditLock, EditLock>(model);
                using (var db = new PMSDbContext())
                {
                    db.EditLocker.Add(entity);
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

        }

        public void UnLock(string fingerprint)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    var entity = db.EditLocker.Where(i => i.FingerPrint == fingerprint).FirstOrDefault();
                    if (entity != null)
                    {
                        db.EditLocker.Remove(entity);
                        db.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

            }

        }

        public void UnLockAll()
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    db.EditLocker.RemoveRange(db.EditLocker.Where(i => i.ID != null));
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

        }

        public void UnLockByLocker(string locker)
        {
            try
            {
                using (var db = new PMSDbContext())
                {
                    db.EditLocker.RemoveRange(db.EditLocker.Where(i => i.Operator == locker));
                    db.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }
    }
}