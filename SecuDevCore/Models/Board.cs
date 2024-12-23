using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SecuDev.Models
{
    public class Board
    {
        private Guid _guid;
        public Guid Guid
        {
            get { 
                if(_guid == default(Guid))
                {
                    _guid = Guid.NewGuid();
                }
                return _guid;
            }
            set
            {
                _guid = value;
            }
        }
        public int BID { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UUID { get; set; }
        public string FileName { get; set; }
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