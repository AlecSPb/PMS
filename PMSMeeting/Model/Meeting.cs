using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMSMeeting.Model
{
    public class Meeting
    {
        public Guid ID { get; set; }
        public string Creator { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime MeetingTime { get; set; }
        public string MeetingPlace { get; set; }
        public string Holder { get; set; }
        public string Theme { get; set; }
        public string MainContent{ get; set; }
        public string Duration { get; set; }
        public string Persons { get; set; }

        public string Records{ get; set; }
        public bool State { get; set; }//On Cancel


    }
}
