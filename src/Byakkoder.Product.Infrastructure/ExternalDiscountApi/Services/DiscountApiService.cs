using Byakkoder.Product.Application.Interfaces;
using Byakkoder.Product.Application.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Http.Json;
using System.Text.Json;

namespace Byakkoder.Product.Infrastructure.ExternalDiscountApi.Services
{
    internal class DiscountApiService : IDiscountApiService
    {
        #region Fields

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly ILogger<DiscountApiService> _logger;

        #endregion

        #region Constructor

        public DiscountApiService(
            IHttpClientFactory httpClientFactory,
            IConfiguration configuration,
            ILogger<DiscountApiService> logger)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public async Task<DiscountDto> GetDiscountByProductId(string productId)
        {
            DiscountDto? discountDto = new DiscountDto();
            using HttpClient httpClient = _httpClientFactory.CreateClient();

            try
            {
                discountDto = await httpClient.GetFromJsonAsync<DiscountDto>($"{_configuration["DiscountsApiUrl"]}/{productId}", new JsonSerializerOptions(JsonSerializerDefaults.Web));
            }
            catch (Exception ex)
            {
                _logger.LogError("Discount API Error: An error has ocurred getting discount for ProductId {productId}: {errorMessage}", productId, ex.Message);
            }

            return discountDto ?? new DiscountDto();
        }

        #endregion
    }
}
