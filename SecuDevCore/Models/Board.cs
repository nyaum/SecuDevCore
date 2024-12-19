using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuDev.Models
{
    public class Board
    {
        public int BID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string InsertDate { get; set; }
        public string InsertIP { get; set; }
        public Users Users { get; set; }
        public Category Category { get; set; }
        public Board()
        {
            Category = new Category();
            Users = new Users();
        }
    }
}