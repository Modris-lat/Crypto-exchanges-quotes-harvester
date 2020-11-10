using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Poloniex.API.Client.Interfaces;
using Poloniex.API.Client.Modules;

namespace Poloniex.API.Client.API
{
    public class PoloniexApiClient: IPoloniexApiClient
    {
        public async Task<string> CallAsync()
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
