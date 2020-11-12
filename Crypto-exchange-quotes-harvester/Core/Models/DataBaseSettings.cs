﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Models
{
    public class DataBaseSettings: ISettingsConfig
    {
        public DataBaseSettings()
        {
            Instruments = new List<Instrument>(){};
        }
        public string DbUrl { get; set; }
        public string DbUser { get; set; }
        public string DbPassword { get; set; }
        public string FlushPeriod { get; set; }
        public List<Instrument> Instruments { get; set; }

        public void ChooseSettings()
        {
            Console.WriteLine("Press a to configure data base settings or any button to continue with default settings:");
            var input = Console.ReadKey().KeyChar;
            if (input == 'a')
            {
                SetSettings();
            }
            else
            {
                DbUrl = "url";
                DbUser = "user";
                DbPassword = "123";
                FlushPeriod = "period";
                var instrument1 = new Instrument {Symbol = "BTCUSDT"};
                Instruments.Add(instrument1);
                var instrument2 = new Instrument {Symbol = "ETHUSDT"};
                Instruments.Add(instrument2);
                var instrument3 = new Instrument {Symbol = "BTCUSDT"};
                instrument3.Depends.Add("ETHBTC");
                instrument3.Depends.Add("ETHUSDT");
                Instruments.Add(instrument3);
                var instrument4 = new Instrument{Symbol = "BTCUSDT"};
                instrument4.Depends.Add("XRPBTC");
                instrument4.Depends.Add("XRPUSDT");
                Instruments.Add(instrument4);
            }
        }
        private void SetSettings()
        {
            Console.WriteLine("Enter data base url:");
            DbUrl = Console.ReadLine();
            Console.WriteLine("Enter user:");
            DbUser = Console.ReadLine();
            Console.WriteLine("Enter password:");
            DbPassword = Console.ReadLine();
            Console.WriteLine("Enter flush period:");
            FlushPeriod = Console.ReadLine();
            bool loop = true;
            while (loop)
            {
                Console.WriteLine("Enter instrument or press q to quit settings:");
                var symbol = Console.ReadLine();
                Console.WriteLine("Enter dependencies instruments(optional) or press q to quit");
                var input = Console.ReadLine();
                var instrument = new Instrument();
                instrument.Symbol = symbol;
                instrument.Depends.Add(input);
                Instruments.Add(instrument);
                if (input == "q")
                {
                    loop = false;
                }
            }
        }
    }
}
