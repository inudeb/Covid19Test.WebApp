using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10CovidCases.WebApp.Infrastructure
{
    public static class API
    {
        public static class Covid19API {
            public static string Regions(string baseUri) => $"{baseUri}/regions";
            public static string Provinces(string baseUri) => $"{baseUri}/provinces";
            public static string Reports(string baseUri) => $"{baseUri}/reports";
        }
    }
}
