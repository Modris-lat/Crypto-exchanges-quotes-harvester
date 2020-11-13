using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Models
{
    public class Instrument
    {
        public string Symbol { get; set; }
        public Depend Depends { get; set; }
    }
}
