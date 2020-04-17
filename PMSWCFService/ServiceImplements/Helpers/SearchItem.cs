using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PMSWCFService.ServiceImplements.Helpers
{
    public class SearchItem
    {
        public SearchItem()
        {
            Item1 = Item2 = Item3 = Item4 = "";
        }
        public string Item1 { get; set; }
        public string Item2 { get; set; }
        public string Item3 { get; set; }
        public string Item4 { get; set; }
    }
}