﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class UserAddress
    {
        
        public string Address1 {get; set;}
        public string Address2 {get; set;}
        public string City {get; set;}
        public StateDomainModel State {get; set;}
        public string Zip {get; set;}
        public Guid UId {get; set;}
    }
}
