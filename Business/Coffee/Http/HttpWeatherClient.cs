using Business.Coffee.Http.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;

namespace Business.Coffee.Http
{
    public class HttpWeatherClient : IHttpWeatherClient
    {
        private readonly HttpClient _httpClient;
        private readonly HttpRequestMessage _request;
        private readonly string _baseUri;
        private readonly string _appId;
        private readonly string _mediaType = "application/json";


        public HttpWeatherClient(HttpClient httpClient, IConfiguration config)
        {
            _httpClient = httpClient;
            _baseUri = config.GetSection("RemoteServices:WeatherBaseUri").Value ?? "";
            _appId = config.GetSection("RemoteServices:AppId").Value ?? "";
            _request = new HttpRequestMessage { RequestUri = new Uri(_baseUri) };
            _request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(_mediaType));
        }





        public async Task<HttpResponseMessage> GetWeather(string city)
        {
            _request.Method = HttpMethod.Get;
            _request.RequestUri = new Uri(_request.RequestUri + $"?q={city}&appid={_appId}");


            Console.WriteLine($"---> GETTING carts ....");

            return await _httpClient.SendAsync(_request);
        }

    }
}
