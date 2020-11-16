using System;
using System.Linq;
using Core.Interfaces;
using Core.Models;

namespace Core.Services
{
    public class SettingsConfig: ISettingsConfig
    {
        private DataBaseSettings settings;

        public SettingsConfig()
        {
            settings = new DataBaseSettings();
        }
        public DataBaseSettings ChooseSettings()
        {
            Console.WriteLine("Press 'a' to configure database settings or any button to continue with default settings:");
            var input = Console.ReadKey().KeyChar;
            if (input == 'a')
            {
                SetSettings();
            }
            else
            {
                settings.FlushPeriod = 10000;
                var instrument1 = new Instrument { Symbol = "ETH/USDT" };
                settings.Instruments.Add(instrument1);
                var instrument2 = new Instrument { Symbol = "USDT/BTC" };
                instrument2.Depends = new Depend {Synthetic1 = "BTC/XRP", Synthetic2 = "USDT/XRP" };
                settings.Instruments.Add(instrument2);
            }

            return settings;
        }
        private void SetSettings()
        {
            Console.WriteLine("Enter flush period in milliseconds:");
            settings.FlushPeriod = long.Parse(Console.ReadLine());
            while (true)
            {
                Console.WriteLine("Enter instrument, example ETH/USDT or 'q' to quit settings:");
                var symbol = Console.ReadLine();
                if (symbol == "q")
                {
                    break;
                }
                var instrument = new Instrument();
                instrument.Symbol = symbol;
                Console.WriteLine(
                    "(optional)Enter depends for synthetics, example 'XRP/BTC XRP/USDT' or 'Enter' to continue:");
                var input = Console.ReadLine();
                if (input.Length > 2 && input.Contains(' '))
                {
                    var list = input.Split(' ');
                    instrument.Depends = new Depend {Synthetic1 = list[0], Synthetic2 = list[1]};
                }
                settings.Instruments.Add(instrument);
                Console.WriteLine($"Instruments count: {settings.Instruments.Count}");
            }
        }
    }
}
