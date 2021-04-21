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
    public class CovidStatisticsServicesImp : ICovidStatisticsService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string _remoteServiceBaseUrl;
        public CovidStatisticsServicesImp(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _config = config;
            _remoteServiceBaseUrl = _config["rapidapiURL"];
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {
            var uri = API.Covid19API.Regions(_remoteServiceBaseUrl);
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var x = JsonConvert.DeserializeObject<BaseApiResponse<List<Region>>>(responseString);
            return x.Data;
        }

        public async Task<List<ReportModel>> GetReportDataAsync(string regionISO="")
        {
            var uri = API.Covid19API.Reports(_remoteServiceBaseUrl) + (!string.IsNullOrEmpty(regionISO) ? $"?iso={regionISO}" : "");
            var response = await _httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();
            var x = JsonConvert.DeserializeObject<BaseApiResponse<List<ReportModel>>>(responseString);
            return x.Data; 
        }
    }
}
