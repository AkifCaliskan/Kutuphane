﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KutuphanApi.Models
{
    public class UserListModel
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TC { get; set; }
        public string PhoneNumber { get; set; }
        public string UserEmail { get; set; }
    

    }
}
