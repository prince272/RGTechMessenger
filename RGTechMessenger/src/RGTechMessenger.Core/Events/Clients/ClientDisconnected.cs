using MediatR;
using RGTechMessenger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Events.Clients
{
    public class ClientDisconnected : INotification
    {
        public ClientDisconnected(Client client)
        {
            Client = client;
        }

        public Client Client { get; }
    }
}
