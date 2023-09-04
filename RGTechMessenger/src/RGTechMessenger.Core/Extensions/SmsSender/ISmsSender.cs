﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Extensions.SmsSender
{
    public interface ISmsSender
    {
        Task SendAsync(string phoneNumber,  string message, CancellationToken cancellationToken = default);
    }
}
