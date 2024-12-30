﻿using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuDev.Models
{
    public class Users : SQLParam
    {
        [DbParameter]
        public string UID { get; set; }
        public string UserName { get; set; }
        [DbParameter]
        public string Password { get; set; }
        public string AuthorityLevel { get; set; }
        public string AuthorityName { get; set; }
        public string IPAddress { get; set; }

    }
}