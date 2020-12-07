﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    class Password
    {
        public int BusinessEntityID { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
