using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Instrument
    {
        public Instrument()
        {
            Depends = new List<string>(){};
        }
        public string Symbol { get; set; }
        public List<string> Depends { get; set; }
    }
}
