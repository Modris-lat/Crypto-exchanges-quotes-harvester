using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProcessResponse
    {
        Task<T> Response<T>(HttpResponseMessage response);
    }
}
