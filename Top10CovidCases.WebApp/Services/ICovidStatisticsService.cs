using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Top10CovidCases.WebApp.Models;

namespace Top10CovidCases.WebApp.Services
{
    public interface ICovidStatisticsService
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<List<ReportModel>> GetReportDataAsync(string regionISO);
    }
}
