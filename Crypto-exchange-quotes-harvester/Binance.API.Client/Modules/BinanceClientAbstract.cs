using Core.Interfaces;

namespace Binance.API.Client
{
    public abstract class BinanceClientAbstract
    {
        public readonly IApiClient _apiClient;
        public BinanceClientAbstract(IApiClient apiClient)
        {
            _apiClient = apiClient;
        }
    }
}
