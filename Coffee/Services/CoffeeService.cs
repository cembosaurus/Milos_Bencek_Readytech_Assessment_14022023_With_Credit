using Business.Coffee.DTOs;
using Business.ServiceResult.Interfaces;
using Coffee.HttpServices.Interfaces;
using Coffee.Services.Interfaces;
using Newtonsoft.Json;

namespace Coffee.Services
{
    public class CoffeeService : ICoffeeService
    {

        private readonly string _messageHotCoffee;
        private readonly string _messageIcedCoffee;
        private readonly string _city;
        private readonly float _ctt;
        private readonly IServiceResultFactory _resultFact;
        private readonly IHttpWeatherService _httpWeatherService;


        public CoffeeService(IConfiguration config, IServiceResultFactory resultFact, IHttpWeatherService httpWeatherService)
        {
            _messageHotCoffee = config.GetSection("message200HotCoffee").Value ?? "";
            _messageIcedCoffee = config.GetSection("message200IcedCoffee").Value ?? "";
            _city = config.GetSection("RemoteServices:City").Value ?? "Sydney";
            _ctt = config.GetValue<float>("CurrentTemperatureThreshold");
            _resultFact = resultFact;
            _httpWeatherService = httpWeatherService;
        }



        public async Task<IServiceResult<string>> GetCoffee()
        {
            var weatherResult = await _httpWeatherService.GetWeather(_city);

            if (weatherResult == null || !weatherResult.Status)
                return _resultFact.Result("", false, Environment.NewLine + $"Weather request was not processed ! Reason: '{weatherResult?.Message ?? "N/A"}'");

            var result = _resultFact.Result(JsonConvert.SerializeObject(new CoffeeReadDTO { Message = weatherResult.Data?.main.temp <= _ctt ? _messageHotCoffee : _messageIcedCoffee }), true, "");

            return result;
        }

    }
}
