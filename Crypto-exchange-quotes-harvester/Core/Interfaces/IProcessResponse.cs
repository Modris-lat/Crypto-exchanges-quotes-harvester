using System.Net.Http;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProcessResponse
    {
        Task<T> Response<T>(HttpResponseMessage response);
    }
}
