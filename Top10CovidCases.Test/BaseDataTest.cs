using NSubstitute;
using System;
using System.Collections.Generic;
using System.Text;
using Top10CovidCases.WebApp.Models;
using Top10CovidCases.WebApp.Services;
using FizzWare.NBuilder;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;
using Top10CovidCases.WebApp.Controllers;
using Microsoft.Extensions.Logging;

using Microsoft.AspNetCore.Mvc;
using Top10CovidCases.WebApp.Models.VM;
namespace Top10CovidCases.Test
{
    public class BaseDataTest
    {
        protected readonly ICovidStatisticsService srv;
        protected readonly List<Region> regions;
        protected readonly IList<ReportModel> data;
        protected readonly IList<ProvinceModel> provinces;
        public BaseDataTest()
        {
            provinces = Builder<ProvinceModel>.CreateListOfSize(130)
           .TheFirst(10)
           .With(c => c.Iso = "US")
           .With(c => c.Name = "US")
           .TheNext(10)
           .With(c => c.Iso = "CHN")
           .With(c => c.Name = "China")
           .TheNext(10)
           .With(c => c.Iso = "JPN")
           .With(c => c.Name = "Japan")
            .TheNext(10)
           .With(c => c.Iso = "THA")
           .With(c => c.Name = "Thailand")
            .TheNext(10)
           .With(c => c.Iso = "MEX")
           .With(c => c.Name = "Mexico")
            .TheNext(10)
           .With(c => c.Iso = "BRA")
            .With(c => c.Name = "Brazil")
            .TheNext(10)
           .With(c => c.Iso = "RUS")
           .With(c => c.Name = "Russia")
            .TheNext(10)
           .With(c => c.Iso = "IND")
            .With(c => c.Name = "India")
            .TheNext(10)
           .With(c => c.Iso = "ITA")
           .With(c => c.Name = "Italy")
             .TheNext(10)
           .With(c => c.Iso = "TUR")
           .With(c => c.Name = "Turkey")
            .TheNext(10)
           .With(c => c.Iso = "GBR")
            .With(c => c.Name = "United Kingdom")
            .TheNext(10)
           .With(c => c.Iso = "SWE")
            .With(c => c.Name = "Sweden")
            .TheNext(10)
           .With(c => c.Iso = "ESP")
             .With(c => c.Name = "Spain")
           .Build();
            srv = Substitute.For<ICovidStatisticsService>();
            var numbergen = new RandomGenerator();
            regions = new List<Region> {
                    new Region{ Iso = "US", Name="US" },
                    new Region{ Iso = "CHN", Name="China" },
                    new Region{ Iso = "JPN", Name="Japan" },
                    new Region{ Iso = "THA", Name="Thailand" },
                    new Region{ Iso = "MEX", Name="Mexico" },
                    new Region{ Iso = "BRA", Name="Brazil" },
                    new Region{ Iso = "RUS", Name="Russia" },
                    new Region{ Iso = "IND", Name="India" },
                    new Region{ Iso = "ITA", Name="Italy" },
                    new Region{ Iso = "TUR", Name="Turkey" },
                    new Region{ Iso = "GBR", Name="United Kingdom" },
                    new Region{ Iso = "SWE", Name="Sweden" },
                    new Region{ Iso = "ESP", Name="Spain" }
                };
            int i = 0;
            data = Builder<ReportModel>.CreateListOfSize(130)
                .TheFirst(10)
                    .With(c => c.Confirmed = 1300 - i)
                    .With(c => c.Deaths = 1300 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 1200 - i)
                    .With(c => c.Deaths = 1200 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 1100 - i)
                    .With(c => c.Deaths = 1100 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 1000 - i)
                    .With(c => c.Deaths = 1000 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 900 - i)
                    .With(c => c.Deaths = 900 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 800 - i)
                    .With(c => c.Deaths = 800 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 700 - i)
                    .With(c => c.Deaths = 700 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 600 - i)
                    .With(c => c.Deaths = 600 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 500 - i)
                    .With(c => c.Deaths = 500 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 400 - i)
                    .With(c => c.Deaths = 400 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 300 - i)
                    .With(c => c.Deaths = 300 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 200 - i)
                    .With(c => c.Deaths = 200 - i)
                    .With(c => c.Region = provinces[i++])
                .TheNext(10)
                    .With(c => c.Confirmed = 100 - i)
                    .With(c => c.Deaths = 100 - i)
                    .With(c => c.Region = provinces[i++])
                .Build();
            srv.GetAllRegionsAsync()
                .Returns(regions);
            srv.GetReportDataAsync(Arg.Any<string>())
                .Returns(data.ToList());
        }
    }
}
