using Microsoft.Extensions.Caching.Memory;
using TlvMunicipalityApi.Models;

namespace TlvMunicipalityApi.Services
{
    public class RequestManagerService
    {
        private IMemoryCache _cache;
        public RequestManagerService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public (bool IsRateLimited, ApiResponse Data) ProcessRequest(string email)
        {

            string cacheKey = $"request_{email}";

            if (_cache.TryGetValue(cacheKey, out ApiResponse? existingResponse) && existingResponse is not null)
            {
                return (true, existingResponse);
            }

            var newResponse = new ApiResponse
            {
                Email = email,
                ReceivedAt = DateTime.UtcNow.ToString("O")
            };

            _cache.Set(cacheKey, newResponse, TimeSpan.FromSeconds(3));
            return (false, newResponse);
        }
    }
}
