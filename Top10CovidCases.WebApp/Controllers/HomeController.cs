using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Top10CovidCases.WebApp.Models;
using Top10CovidCases.WebApp.Models.VM;
using Top10CovidCases.WebApp.Services;

namespace Top10CovidCases.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICovidStatisticsService _srv;
        public HomeController(ILogger<HomeController> logger, ICovidStatisticsService srv)
        {
            _logger = logger;
            _srv = srv;
        }

        public async Task<IActionResult> Index(string iso="")
        {
            HomeVM vm = new HomeVM();
            try
            {
                var t1 =  _srv.GetAllRegionsAsync();
                var t2 = _srv.GetReportDataAsync(iso);
                Task.WaitAll(t1, t2);
                
                vm.RegionList = t1.Result;
                if (string.IsNullOrEmpty(iso))
                    vm.ReportList = (from x in t2.Result
                                     group x by x.Region.Iso into regionCases
                                     orderby regionCases.Sum(c => c.Confirmed) descending
                                     select new
                                     ReportListModel
                                     {
                                         Confirmed = regionCases.Sum(c => c.Confirmed),
                                         Deaths = regionCases.Sum(c => c.Deaths),
                                         RegionOrProvince = vm.RegionList.SingleOrDefault(c => c.Iso == regionCases.Key).Name
                                     }
                                     ).Take(10).ToList();
                else
                    vm.ReportList = (from x in t2.Result
                                     group x by  new  { x.Region.Iso, x.Region.Province} into provinceCases
                                     orderby provinceCases.Sum(c => c.Confirmed) descending
                                     where provinceCases.Key.Iso == iso
                                     select new
                                     ReportListModel
                                     {
                                         Confirmed = provinceCases.Sum(c => c.Confirmed),
                                         Deaths = provinceCases.Sum(c => c.Deaths),
                                         RegionOrProvince = provinceCases.Key.Province
                                     }
                       ).Take(10).ToList();
            }
            catch (Exception ex)
            {

              
            }
            return View(vm);
        }
        [HttpGet]
        
        public async Task<IActionResult> Export(string type, string iso) 
        {
            List<ReportListModel> data = new List<ReportListModel>();
            try
            {
                var t1 = _srv.GetAllRegionsAsync();
                var t2 = _srv.GetReportDataAsync(iso);
                Task.WaitAll(t1, t2);
              
                var regions = t1.Result;
                if (string.IsNullOrEmpty(iso))
                    data = (from x in t2.Result
                                     group x by x.Region.Iso into regionCases
                                     orderby regionCases.Sum(c => c.Confirmed) descending
                                     select new
                                     ReportListModel
                                     {
                                         Confirmed = regionCases.Sum(c => c.Confirmed),
                                         Deaths = regionCases.Sum(c => c.Deaths),
                                         RegionOrProvince = regions.SingleOrDefault(c => c.Iso == regionCases.Key).Name
                                     }
                                     ).Take(10).ToList();
                else
                    data = (from x in t2.Result
                                     group x by new { x.Region.Iso, x.Region.Province } into provinceCases
                                     orderby provinceCases.Sum(c => c.Confirmed) descending
                                     where provinceCases.Key.Iso == iso
                                     select new
                                     ReportListModel
                                     {
                                         Confirmed = provinceCases.Sum(c => c.Confirmed),
                                         Deaths = provinceCases.Sum(c => c.Deaths),
                                         RegionOrProvince = provinceCases.Key.Province
                                     }
                       ).Take(10).ToList();
                switch (type)
                {
                    case "xml":
                        using (var stream = new MemoryStream())
                        {
                            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(List<ReportListModel>));
                            s.Serialize(stream, data);
                            var content = stream.ToArray();
                            return File(content, "application/xml", "data.xml");

                        }
                        break;
                    case "csv":
                        StringBuilder sb = new StringBuilder();
                        foreach (var item in data)
                        {
                            sb.AppendLine($"{item.RegionOrProvince},{item.Confirmed},{item.Deaths}");
                        }
                        return File(new System.Text.UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "data.csv");
                        break;
                    case "json":
                        return File(new System.Text.UTF8Encoding().GetBytes(JsonConvert.SerializeObject(data)), "application/json", "data.json");
                        break;
                    default:
                        return Ok();
                        break;
                }

                
              
            }
            catch (Exception ex)
            {


            }
            return Ok();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
