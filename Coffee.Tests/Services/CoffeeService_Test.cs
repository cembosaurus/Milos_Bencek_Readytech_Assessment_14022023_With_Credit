using Business.Coffee.DTOs;
using Business.ServiceResult;
using Business.ServiceResult.Interfaces;
using Coffee.HttpServices.Interfaces;
using Coffee.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;

namespace Coffee.Tests.Services
{

    [TestFixture]
    internal class CoffeeService_Test
    {

        private IConfiguration _config;
        private const string _hotCoffeeConfigKey = "message200HotCoffee";
        private const string _hotCoffeeConfigValue = "Your piping hot coffee is ready";
        private const string _icedCoffeeConfigKey = "message200IcedCoffee";
        private const string _icedCoffeeConfigValue = "Your refreshing iced coffee is ready";
        private const string _temperatureThresholdConfigKey = "CurrentTemperatureThreshold";
        private const string _temperatureThresholdConfigValue = "30";
        private const float _currentTemperature = 31f;

        private IServiceResultFactory _resultFact;
        private Mock<IServiceResult<WeatherReadDTO>> _weatherServiceResult;
        private Mock<IHttpWeatherService> _httpWeatherService;
        private WeatherReadDTO _weatherReadDTO;

        private const string _city = "Sydney";
        private const float _zeroCelsiusInKelvins = 273.15f;
        private const float _31CelsiusInKelvins = 273.15f + _currentTemperature;


        [SetUp]
        public void Setup()
        {
            _config = new ConfigurationBuilder()
                .AddInMemoryCollection(new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string> ( _hotCoffeeConfigKey, _hotCoffeeConfigValue ),
                    new KeyValuePair<string, string> ( _icedCoffeeConfigKey, _icedCoffeeConfigValue ),
                    new KeyValuePair<string, string> (_temperatureThresholdConfigKey, _temperatureThresholdConfigValue)
                })
                .Build();

            _resultFact = new ServiceResultFactory();
            _weatherServiceResult = new Mock<IServiceResult<WeatherReadDTO>>();
            _httpWeatherService = new Mock<IHttpWeatherService>();
            _weatherReadDTO = new WeatherReadDTO();
            _weatherReadDTO.main = new Main();
            _weatherReadDTO.name = _city;
        }




        [Test]
        public void GetCoffee_WhenTemperatureUnderThreshold_ReturnsHotCoffee()
        {
            _weatherReadDTO.main.temp = _zeroCelsiusInKelvins;

            _weatherServiceResult.Setup(sr => sr.Data).Returns(_weatherReadDTO);
            _weatherServiceResult.Setup(sr => sr.Status).Returns(true);
            _weatherServiceResult.Setup(sr => sr.Message).Returns("");
            _httpWeatherService.Setup(ws => ws.GetWeather(_city))
                .Returns(Task.FromResult(_weatherServiceResult.Object));


            var coffeeService = new CoffeeService(_config, _resultFact, _httpWeatherService.Object);

            var serviceResult = coffeeService.GetCoffee().Result;

            var result = JsonConvert.DeserializeObject<CoffeeReadDTO>(serviceResult.Data);


            Assert.That(result?.Message, Is.EqualTo(_config.GetSection(_hotCoffeeConfigKey).Value));
        }


        [Test]
        public void GetCoffee_WhenTemperatureEqualOrAboveThreshold_ReturnsIcedCoffee()
        {
            _weatherReadDTO.main.temp = _31CelsiusInKelvins;

            _weatherServiceResult.Setup(sr => sr.Data).Returns(_weatherReadDTO);
            _weatherServiceResult.Setup(sr => sr.Status).Returns(true);
            _weatherServiceResult.Setup(sr => sr.Message).Returns("");
            _httpWeatherService.Setup(ws => ws.GetWeather(_city))
                .Returns(Task.FromResult(_weatherServiceResult.Object));


            var coffeeService = new CoffeeService(_config, _resultFact, _httpWeatherService.Object);

            var serviceResult = coffeeService.GetCoffee().Result;

            var result = JsonConvert.DeserializeObject<CoffeeReadDTO>(serviceResult.Data);


            Assert.That(result?.Message, Is.EqualTo(_config.GetSection(_icedCoffeeConfigKey).Value));
        }



        [Test]
        public void GetCoffee_WhenWeatherServiceUnavailable_ReturnsFailureResult()
        {
            _weatherReadDTO.main.temp = _31CelsiusInKelvins;

            _weatherServiceResult.Setup(sr => sr.Data).Returns(_weatherReadDTO);
            _weatherServiceResult.Setup(sr => sr.Status).Returns(false);
            _weatherServiceResult.Setup(sr => sr.Message).Returns("");
            _httpWeatherService.Setup(ws => ws.GetWeather(_city))
                .Returns(Task.FromResult(_weatherServiceResult.Object));


            var coffeeService = new CoffeeService(_config, _resultFact, _httpWeatherService.Object);

            var result = coffeeService.GetCoffee().Result;


            Assert.That(result?.Status, Is.False);
            Assert.That(result?.Message, Does.StartWith("Weather request was not processed !").IgnoreCase);
        }

    }
}
