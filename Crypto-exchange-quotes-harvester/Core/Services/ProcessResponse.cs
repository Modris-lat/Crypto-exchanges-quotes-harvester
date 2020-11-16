using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;
using Core.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Services
{
    public class ProcessResponse: IProcessResponse
    {
        public async Task<T> Response<T>(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                return JsonConvert.DeserializeObject<T>(result);
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
