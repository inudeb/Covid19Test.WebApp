using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Top10CovidCases.WebApp.Controllers;

namespace Top10CovidCases.Test
{
    public class ExportDataTest:BaseDataTest
    {
        public ExportDataTest():base()
        {

        }
        [Test]
        public async Task ExportToCSVTest() 
        {
            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Export("csv", "");
            Assert.IsAssignableFrom<FileContentResult>(result);
            var fileResult = (FileContentResult)result;
            Assert.IsTrue(fileResult.ContentType == "text/csv");
        }
        [Test]
        public async Task ExportToJsonTest()
        {
            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Export("json", "");
            Assert.IsAssignableFrom<FileContentResult>(result);
            var fileResult = (FileContentResult)result;
            Assert.IsTrue(fileResult.ContentType == "application/json");
        }
        [Test]
        public async Task ExportToXmlTest()
        {
            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Export("xml", "");
            Assert.IsAssignableFrom<FileContentResult>(result);
            var fileResult = (FileContentResult)result;
            Assert.IsTrue(fileResult.ContentType == "application/xml");
        }
        [Test]
        public async Task WrongTypeExportTest()
        {
            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Export("hlm", "");
            Assert.IsNotAssignableFrom<FileContentResult>(result);
        }
    }
}
