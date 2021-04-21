﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10CovidCases.WebApp.Models
{
    public class ReportModel
    {
        public int Confirmed { get; set; }
        public int Deaths { get; set; }
        public ProvinceModel Region { get; set; }
    }
}
