using System.Collections.Generic;

namespace Core.Models
{
    public class DataBaseSettings
    {
        public DataBaseSettings()
        {
            Instruments = new List<Instrument>(){};
        }
        public string FlushPeriod { get; set; }
        public List<Instrument> Instruments { get; set; }
    }
}
