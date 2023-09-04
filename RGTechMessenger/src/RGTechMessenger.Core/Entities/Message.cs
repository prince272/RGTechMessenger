using RGTechMessenger.Core.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Entities
{
    public class Message : IEntity
    {
        public long Id { get; set; }

        public virtual User Sender { get; set; } = default!;

        public long SenderId { get; set; }

        public string Content { get; set; } = default!;
    }
}
