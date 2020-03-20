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
    public class RawMaterialSheetService : IRawMaterialSheetService
    {
        public void AddRawMaterialSheet(DcRawMaterialSheet model)
        {
            try
            {
                XS.RunLog();
                Mapper.Initialize(config => config.CreateMap<DcRawMaterialSheet, RawMaterialSheet>());

                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<RawMaterialSheet>(model);
                    db.RawMaterialSheets.Add(entity);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }

        public List<DcRawMaterialSheet> GetRawMaterialSheet(int s, int t, string lot, string composition)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.RawMaterialSheets
                                where m.State == PMSCommon.RawMaterialSheetState.在库.ToString()
                                && m.Composition.Contains(composition)
                                && m.Lot.Contains(lot)
                                orderby m.StoreTime descending 
                                select m;
                    Mapper.Initialize(config => config.CreateMap<RawMaterialSheet, DcRawMaterialSheet>());
                    return Mapper.Map<List<RawMaterialSheet>, List<DcRawMaterialSheet>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return new List<DcRawMaterialSheet>();
            }
        }

        public List<DcRawMaterialSheet> GetRawMaterialSheetAll(int s, int t, string lot, string composition)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.RawMaterialSheets
                                where m.State != PMSCommon.RawMaterialSheetState.作废.ToString()
                                && m.Composition.Contains(composition)
                                && m.Lot.Contains(lot)
                                orderby m.StoreTime descending
                                select m;
                    Mapper.Initialize(config => config.CreateMap<RawMaterialSheet, DcRawMaterialSheet>());
                    return Mapper.Map<List<RawMaterialSheet>, List<DcRawMaterialSheet>>(query.Skip(s).Take(t).ToList());
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return new List<DcRawMaterialSheet>();
            }

        }

        public int GetRawMaterialSheetAllCount(string lot, string composition)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.RawMaterialSheets
                                where m.State != PMSCommon.RawMaterialSheetState.作废.ToString()
                                && m.Composition.Contains(composition)
                                && m.Lot.Contains(lot)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;

            }
        }

        public int GetRawMaterialSheetCount(string lot, string composition)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from m in db.RawMaterialSheets
                                where m.State == PMSCommon.RawMaterialSheetState.在库.ToString()
                                && m.Composition.Contains(composition)
                                && m.Lot.Contains(lot)
                                select m;
                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                return 0;

            }
        }

        public void UpdateRawMaterialSheet(DcRawMaterialSheet model)
        {
            try
            {
                XS.RunLog();
                Mapper.Initialize(config => config.CreateMap<DcRawMaterialSheet, RawMaterialSheet>());

                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<RawMaterialSheet>(model);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
            }
        }
    }
}