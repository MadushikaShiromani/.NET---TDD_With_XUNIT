using FormulaApp.Api.Configuration;
using FormulaApp.Api.Models;
using FormulaApp.Api.Services.Interfaces;
using Microsoft.Extensions.Options;
using System.Net;

namespace FormulaApp.Api.Services
{
    public class FanService : IFanService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiServiceConfig _config;

        public FanService(
            HttpClient httpClient,
            IOptions<ApiServiceConfig> config)
        {
            _httpClient = httpClient;
            _config = config.Value;
        }

        public async Task<List<Fan>?> GetAllFans()
        {
            var response = await _httpClient.GetAsync(_config.Url);
            switch (response.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new List<Fan>();
                case HttpStatusCode.Unauthorized:
                    return null;
                default:
                {
                    var fans = await response.Content.ReadFromJsonAsync<List<Fan>>();
                    return fans;
                }
            }
        }
    }
}
