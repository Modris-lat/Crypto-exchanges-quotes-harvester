﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Models;

namespace Core.Interfaces
{
    public interface ICalculateSyntheticQuotes
    {
        Quote Calculate(List<Synthetic> syntheticList, DataBaseSettings settings);
    }
}
