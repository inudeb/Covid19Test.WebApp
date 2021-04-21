using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top10CovidCases.WebApp.Models
{
    public class BaseApiResponse<T>
    {
        public T Data { get; set; }
    }
}
