using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuDev.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public int CustomerTypeID { get; set; }
        public int MasterLocationID { get; set; }
        public string LocationName { get; set; }
        public int ParentLocationID { get; set; }
        public string InsertDate {  get; set; }

    }
}