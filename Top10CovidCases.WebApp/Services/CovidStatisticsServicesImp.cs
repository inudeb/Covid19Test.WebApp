using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Top10CovidCases.WebApp.Models;

namespace Top10CovidCases.WebApp.Services
{
    public class CovidStatisticsServicesImp : ICovidStatisticsService
    {
        private readonly HttpClient _client;
        public CovidStatisticsServicesImp(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<Region>> GetAllRegionsAsync()
        {


            //        var request = new HttpRequestMessage
            //        {
            //            //define el metodo
            //            Method = HttpMethod.Get,
            //            //define la url y recurso
            //            RequestUri = new Uri("https://covid-19-statistics.p.rapidapi.com/regions"),
            //            //cabeceraz
            //            Headers =
            //{
            //    { "x-rapidapi-key", "7dd565a419msh715a376555e1d53p123188jsn86b4ea0fc047" },
            //    { "x-rapidapi-host", "covid-19-statistics.p.rapidapi.com" },
            //},
            //        };
            //        //llamada del api
            //        using (var response = await client.SendAsync(request))
            //        {
            //            response.EnsureSuccessStatusCode();
            //            var body = await response.Content.ReadAsStringAsync();
            //            Console.WriteLine(body);
            //        }

            throw new NotImplementedException();
        }

        public Task<List<ReportModel>> GetReportDataAsync(string regionISO)
        {
            throw new NotImplementedException();
        }
    }
}
