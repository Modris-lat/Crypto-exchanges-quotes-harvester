using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

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
