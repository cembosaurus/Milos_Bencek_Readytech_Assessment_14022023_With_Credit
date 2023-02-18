using Business.Coffee.DTOs;
using Business.ServiceResult.Interfaces;

namespace Coffee.HttpServices.Interfaces
{
    public interface IHttpWeatherService
    {
        Task<IServiceResult<WeatherReadDTO>> GetWeather(string city);
    }
}
