using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
