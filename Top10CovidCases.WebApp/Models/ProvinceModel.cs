using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10CovidCases.WebApp.Models
{
    public class ProvinceModel:Region
    {
        public string Province { get; set; }
        public decimal Lat { get; set; }
        public decimal Long { get; set; }

    }
}
