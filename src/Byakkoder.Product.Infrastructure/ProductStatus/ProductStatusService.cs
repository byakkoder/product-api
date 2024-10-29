using Byakkoder.Product.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;

namespace Byakkoder.Product.Infrastructure.ProductStatus
{
    internal class ProductStatusService : IProductStatusService
    {
        #region Fields

        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<ProductStatusService> _logger;

        #endregion

        #region Constants

        private const string _cacheKey = "ProductStatusDict";

        #endregion

        #region Constructor

        public ProductStatusService(
            IMemoryCache memoryCache,
            ILogger<ProductStatusService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        #endregion

        #region Public Methods

        public Dictionary<int, string> GetProductStatusDict()
        {
            try
            {
                if (!_memoryCache.TryGetValue(_cacheKey, out Dictionary<int, string> productStatusDict))
                {
                    productStatusDict = LoadProductStatus();

                    MemoryCacheEntryOptions cacheEntryOptions = new MemoryCacheEntryOptions()
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                    };

                    _memoryCache.Set(_cacheKey, productStatusDict, cacheEntryOptions);
                }

                return productStatusDict ?? [];
            }
            catch (Exception ex)
            {
                _logger.LogError("An error has ocurred getting the product status dictionary: {errorMessage}", ex.Message);
            }

            return [];
        }

        #endregion

        #region Private Methods

        private Dictionary<int, string> LoadProductStatus()
        {
            // DB Query Simulation.
            return new Dictionary<int, string>()
            {
                { 0, "Inactive" },
                { 1, "Active" },
            };
        }

        #endregion
    }
}
