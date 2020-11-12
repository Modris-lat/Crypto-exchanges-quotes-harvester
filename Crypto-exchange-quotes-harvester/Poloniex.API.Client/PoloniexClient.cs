using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Core.Utils;
using Newtonsoft.Json.Linq;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client
{
    public class PoloniexClient: IPoloniexClient
    {
        public async Task<IEnumerable<PoloniexMarketData>> GetOrderBookTicker()
        {
            var result = await CallAsync();
            JObject jobject = JObject.Parse(result);
            var list = new List<PoloniexMarketData>() { };
            foreach (var o in jobject)
            {
                var marketData = new PoloniexMarketData();
                marketData.Symbol = o.Key;
                if (o.Key.Contains("_"))
                {
                    var str = o.Key.Split('_');
                    marketData.Symbol = str[0] + str[1];
                }
                var asksString = o.Value["asks"].ToString().Split('"')[1].Replace(".", ",");
                marketData.AskPrice = decimal.Parse(asksString);
                var bidsString = o.Value["bids"].ToString().Split('"')[1].Replace(".", ","); ;
                marketData.BidPrice = decimal.Parse(bidsString);
                marketData.IsFrozen = int.Parse(o.Value["isFrozen"].ToString());
                list.Add(marketData);
            }
            return list;
        }
        private async Task<string> CallAsync()
        {
            HttpClient poloniexClient = new HttpClient();
            var response = await poloniexClient.GetAsync("https://poloniex.com/public?command=returnOrderBook&currencyPair=all&depth=1");
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return result;
            }
            if (response.StatusCode == HttpStatusCode.GatewayTimeout)
            {
                throw new Exception("Api Request Timeout.");
            }
            var e = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var eCode = 0;
            string eMsg = "";
            if (e.IsValidJson())
            {
                try
                {
                    var i = JObject.Parse(e);

                    eCode = i["code"]?.Value<int>() ?? 0;
                    eMsg = i["msg"]?.Value<string>();
                }
                catch { }
            }

            throw new Exception(string.Format("Api Error Code: {0} Message: {1}", eCode, eMsg));
        }
    }
}
