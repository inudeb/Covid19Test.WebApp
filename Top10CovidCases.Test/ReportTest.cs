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
    public class ReportTest:BaseDataTest
    {


        public ReportTest()
            :base()
        {
        }
        [Test]
        public async Task HomeShouldReturnViewModelTest()
        {

            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Index("");
            Assert.IsAssignableFrom<ViewResult>(result);
            var viewResult = (ViewResult)result;
            Assert.IsAssignableFrom<HomeVM>(viewResult.Model);
        }
        [Test]
        public async Task ReportShouldReturn10ElementsNoFilterViewModelTest()
        {

            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Index("");
            var viewResult = (ViewResult)result;
            Assert.IsAssignableFrom<HomeVM>(viewResult.Model);
            var vm = (HomeVM)viewResult.Model;
            Assert.IsNotNull(vm);
            Assert.AreEqual(vm.ReportList.Count, 10);
        }
        [Test]
        public async Task ReportShoulFilterByISOTest()
        {

            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Index("US");
            var viewResult = (ViewResult)result;
            Assert.IsAssignableFrom<HomeVM>(viewResult.Model);
            var vm = (HomeVM)viewResult.Model;
            Assert.IsNotNull(vm);
            Assert.AreEqual(vm.ReportList.Count, 10);
            Assert.IsTrue(vm.ReportList.Any(c => c.RegionOrProvince == "Province1"));
        }
        [Test]
        public async Task ReportNoResultTest()
        {

            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Index("USSA");
            var viewResult = (ViewResult)result;
            Assert.IsAssignableFrom<HomeVM>(viewResult.Model);
            var vm = (HomeVM)viewResult.Model;
            Assert.IsNotNull(vm);
            Assert.IsTrue(vm.ReportList.Count==0);
         
        }
        [Test]
        public async Task ReportShoulGetTop10RegionCasesTest()
        {
          List<string> top10Region = new List<string> { "US", "China", "Japan", "Thailand", "Mexico", "Brazil", "Russia", "India", "Italy", "Turkey" };
            HomeController c = new HomeController(Substitute.For<ILogger<HomeController>>(), srv);
            var result = await c.Index();
            var viewResult = (ViewResult)result;
            Assert.IsAssignableFrom<HomeVM>(viewResult.Model);
            var vm = (HomeVM)viewResult.Model;
            Assert.IsNotNull(vm);
            Assert.AreEqual(vm.ReportList.Count, 10);
            //the data should contains the top 10 regions and 
            CollectionAssert.AreEqual(vm.ReportList.Select(c => c.RegionOrProvince).ToList(), top10Region);

        }

    }
}
