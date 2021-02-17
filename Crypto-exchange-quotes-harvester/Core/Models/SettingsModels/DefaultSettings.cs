using System.Collections.Generic;

namespace Core.Models.SettingsModels
{
    public class DefaultSettings
    {
        public long FlushPeriod => 10000;
        public List<Instrument> Instruments => new List<Instrument>
        {
            new Instrument
            {
                Symbol = "BTC/USDT"
            },
            new Instrument
            {
                Symbol = "ETH/USDT"
            },
            new Instrument
            {
                Symbol = "BTC/USDT",
                Depends = new Depend
                {
                    Synthetic1 = "ETH/BTC",
                    Synthetic2 = "ETH/USDT"
                }
            }
        };
    }
}
