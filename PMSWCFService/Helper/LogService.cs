using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PMSDAL;

namespace PMSWCFService
{
    public class Log
    {

        public Log()
        {

        }

        public void Debug(string information)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = new DebugInformation()
                    {
                        ID = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        Information = information
                    };
                    dc.DebugInformations.Add(entity);
                    dc.SaveChanges();
                }
            }
            catch (Exception)
            {

            }
        }

        public void Information(string information)
        {
            try
            {
                using (var dc = new PMSDbContext())
                {
                    var entity = new LogInformation()
                    {
                        ID = Guid.NewGuid(),
                        CreateTime = DateTime.Now,
                        Log = information
                    };
                    dc.LogInformations.Add(entity);
                    dc.SaveChanges();
                }
            }
            catch (Exception)
            {

            }

        }

        public void Error(Exception ex)
        {
            try
            {
                if (ex != null)
                {
                    using (var dc = new PMSDbContext())
                    {
                        var entity = new LogError()
                        {
                            ID = Guid.NewGuid(),
                            CreateTime = DateTime.Now,
                            Error = ex.Message
                        };

                        dc.LogErrors.Add(entity);
                        dc.SaveChanges();
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
