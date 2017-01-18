using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSDAL
{
    public class SeedInitializer: CreateDatabaseIfNotExists<PMSDbContext>
    {
        protected override void Seed(PMSDbContext context)
        {
            context.Users.Add(
                new User() { ID=Guid.NewGuid(),UserName="xs.zhou",Password="",RealName="周新生",CreateTime=DateTime.Now,State=0,Email="xs.zhou@cdpmi.net",Phone="13540781789"});
            context.SaveChanges();
            base.Seed(context);
        }
    }
}
