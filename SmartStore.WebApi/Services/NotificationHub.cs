using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SmartStore.WebApi.Services
{
    public sealed class NotificationHub : Hub
    {
        public const string GROUP_NAME = "notificacao";
                
        public override Task OnConnectedAsync()
        {
            return Groups.AddToGroupAsync(Context.ConnectionId, "notificacao");
        }
    }
}
