using System.Threading.Tasks;
using Binance.API.Client.Modules.Enums;
using Core.Modules.WebSockets;

namespace Core.Interfaces
{
    public interface IApiClient
    {
        Task<T> CallAsync<T>(ApiMethod method, string endpoint, bool isSigned = false, string parameters = null);
        void ConnectToWebSocket<T>(string parameters, ApiClientAbstract.MessageHandler<T> messageDelegate, bool useCustomParser = false);
        void ConnectToUserDataWebSocket(
            string parameters, ApiClientAbstract.MessageHandler<AccountUpdatedMessage> accountHandler, ApiClientAbstract.MessageHandler<OrderOrTradeUpdatedMessage> tradeHandler, ApiClientAbstract.MessageHandler<OrderOrTradeUpdatedMessage> orderHandler);
    }
}
