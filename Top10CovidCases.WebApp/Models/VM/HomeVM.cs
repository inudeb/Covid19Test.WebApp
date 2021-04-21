using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10CovidCases.WebApp.Models.VM
{
    public class HomeVM
    {
        public List<Region> RegionList { get; set; }
        public List<ReportListModel> ReportList { get; set; }
    }
}
