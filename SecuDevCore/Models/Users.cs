using CoreDAL.ORM;
using CoreDAL.ORM.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SecuDev.Models
{
    public class Users : SQLParam
    {
        public string UID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string AuthorityLevel { get; set; }
        public string IPAddress { get; set; }
        public string InsertDate { get; set; }
        public string LastLogin { get; set; }
        public int Status { get; set; }
        public Authority Authority { get; set; }
        public Users()
        {
            Authority = new Authority();
        }
    }
}