using Business.Coffee.DTOs;
using Business.Coffee.Http.Interfaces;
using Business.ServiceResult.Interfaces;
using Coffee.HttpServices.Interfaces;

namespace Coffee.HttpServices
{
    public class HttpWeatherService : IHttpWeatherService
    {

        private readonly IHttpWeatherClient _httpWeatherClient;
        private readonly IServiceResultFactory _resutlFact;


        public HttpWeatherService(IHttpWeatherClient httpWeatherClient, IServiceResultFactory resutlFact)
        {
            _httpWeatherClient = httpWeatherClient;
            _resutlFact = resutlFact;
        }




        public async Task<IServiceResult<WeatherReadDTO>> GetWeather(string city)
        {
            var response = await _httpWeatherClient.GetWeather(city);

            if (!response.IsSuccessStatusCode)
                return _resutlFact.Result<WeatherReadDTO>(null, false, response.StatusCode.ToString());

            var content = response.Content.ReadAsStringAsync().Result;

            var result = _resutlFact.Result(Newtonsoft.Json.JsonConvert.DeserializeObject<WeatherReadDTO>(content), true, "");

            return result;
        }








    }
}
