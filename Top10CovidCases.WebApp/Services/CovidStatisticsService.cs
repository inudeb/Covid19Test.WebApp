using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Top10CovidCases.WebApp.Infrastructure;
using Top10CovidCases.WebApp.Models;

namespace Top10CovidCases.WebApp.Services
{
    public class CovidStatisticsService : ICovidStatisticsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _remoteServiceBaseUrl;
        public CovidStatisticsService(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _remoteServiceBaseUrl = _config["rapidapiURL"];
        }
        public async Task<Region> GetAllRegionsAsync()
        {
            var uri = API.Covid19API.Regions(_remoteServiceBaseUrl);
            var response = await _httpClient.GetAsync(uri);
            var responseString = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<CambioDePrecio>(responseString);
        }

        public Task<ProvinceModel> GetProvincesByRegionAsync(string iso)
        {
            throw new NotImplementedException();
        }

        public Task<ReportModel> GetTop10CovidReportAsync(string iso)
        {
            throw new NotImplementedException();
        }
    }
}
