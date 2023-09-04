using MediatR;
using Microsoft.AspNetCore.SignalR;
using RGTechMessenger.Core.Events.Clients;
using RGTechMessenger.Core.Models;
using RGTechMessenger.Core.Repositories;
using RGTechMessenger.Infrastructure.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Infrastructure.RealTime.Handlers
{
    public class UserDisconnectedHandler : INotificationHandler<UserDisconnected>
    {
        private readonly IHubContext<ChatHub> _hubContext;

        public UserDisconnectedHandler(IHubContext<ChatHub> hubContext)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
        }

        public async Task Handle(UserDisconnected notification, CancellationToken cancellationToken)
        {
            var message = new
            {
                UserId = notification.User.Id,
                Connections = notification.Connections,
                ClientId = notification.Client.Id,
            };

            await _hubContext.Clients.All.SendAsync(notification.GetType().Name, message, cancellationToken);
        }
    }
}