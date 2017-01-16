using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotificationDAL
{
    public class NoticeDataContext : DbContext
    {
        public NoticeDataContext():base("name=Default")
        {

        }


        public virtual DbSet<Notice> Notices { get; set; }
    }
}
