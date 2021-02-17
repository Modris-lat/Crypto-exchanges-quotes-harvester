using Binance.API.Client;
using Binance.API.Client.Interfaces;
using Binance.API.Client.Service;
using Core;
using Core.Interfaces;
using Core.Services;
using Harvested.Quotes.Data;
using Harvested.Quotes.Data.Interfaces;
using Harvested.Quotes.Data.Service;
using Poloniex.API.Client;
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Service;

namespace Quotes.Harvester.Console
{
    public static class DependencyFactory
    {
        public static ILogMessageService CreateLogMessageService()
        {
            return new LogMessageService(CreateDbContext());
        }
        public static IQuoteService CreateQuoteService()
        {
            return new QuotesService(CreateDbContext());
        }
        static IQuotesDBContext CreateDbContext()
        {
            return new QuotesDBContext();
        }
        public static IPoloniexSearch CreatePoloniexSearch()
        {
            return new PoloniexSearch(CreateCalculateSyntheticQuotes());
        }
        public static IGetSearchInstruments CreateSearchInstruments()
        {
            return new GetSearchInstruments();
        }
        public static IBinanceSearch CreateBinanceSearch()
        {
            return new BinanceSearch(CreateCalculateSyntheticQuotes());
        }
        static ICalculateSyntheticQuotes CreateCalculateSyntheticQuotes()
        {
            return new CalculateSyntheticQuotes();
        }
        public static ISettingsConfig CreateSettingsConfig()
        {
            return new SettingsConfig();
        }
        public static IPoloniexClient CreatePoloniexClient()
        {
            return new PoloniexClient();
        }
        public static IBinanceClient CreateBinanceClient()
        {
            return new BinanceClient(CreateApiClient());
        }
        static IApiClient CreateApiClient()
        {
            return new ApiClient(AccessKeys.ApiKeyBinance, AccessKeys.ApiSecretKeyBinance);
        }
    }
}
