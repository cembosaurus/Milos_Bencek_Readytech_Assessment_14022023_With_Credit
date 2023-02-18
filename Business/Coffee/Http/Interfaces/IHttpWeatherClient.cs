using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Coffee.Http.Interfaces
{
    public interface IHttpWeatherClient
    {
        Task<HttpResponseMessage> GetWeather(string city);
    }
}
