﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sabio.Web.Domain
{
    public class StateDomainModel
    {
        public int StateProvinceId { get; set; }

        public string StateProvinceCode { get; set; }

        public string CountryRegionCode { get; set; }

        public string Name { get; set; }


    }
}