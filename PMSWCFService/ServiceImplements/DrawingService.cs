using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PMSWCFService.DataContracts;
using PMSWCFService.ServiceContracts;
using AutoMapper;
using PMSCommon;
using PMSDAL;


namespace PMSWCFService
{
    public class DrawingService : IDrawingService
    {
        public void AddDrawing(DcDrawing model)
        {
            try
            {
                XS.RunLog();
                Mapper.Initialize(cfg => cfg.CreateMap<DcDrawing, Drawing>());
                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<Drawing>(model);
                    db.Drawings.Add(entity);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public List<DcDrawing> GetDrawing(int s, int t, string drawingName, string customer, string mainDimension)
        {
            try
            {
                XS.RunLog();

                Mapper.Initialize(cfg => cfg.CreateMap<Drawing, DcDrawing>());
                using (var db = new PMSDbContext())
                {
                    var query = from d in db.Drawings
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.DrawingName.Contains(drawingName)
                                && d.Customer.Contains(customer)
                                && d.MainDimension.Contains(mainDimension)
                                orderby d.CreateTime descending
                                select d;

                    return Mapper.Map<List<Drawing>, List<DcDrawing>>(query.Skip(s).Take(t).ToList());
                }

            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public int GetDrawingCount(string drawingName, string customer, string mainDimension)
        {
            try
            {
                XS.RunLog();
                using (var db = new PMSDbContext())
                {
                    var query = from d in db.Drawings
                                where d.State != PMSCommon.SimpleState.作废.ToString()
                                && d.DrawingName.Contains(drawingName)
                                && d.Customer.Contains(customer)
                                && d.MainDimension.Contains(mainDimension)
                                select d;

                    return query.Count();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }

        public void UpdateDrawing(DcDrawing model)
        {
            try
            {
                XS.RunLog();
                Mapper.Initialize(cfg => cfg.CreateMap<DcDrawing, Drawing>());
                using (var db = new PMSDbContext())
                {
                    var entity = Mapper.Map<Drawing>(model);
                    db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                XS.Current.Error(ex);
                throw ex;
            }
        }
    }
}