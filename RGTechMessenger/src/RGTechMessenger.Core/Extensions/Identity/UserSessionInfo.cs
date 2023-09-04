﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Extensions.Identity
{
    public class UserSessionInfo
    {
        public string AccessToken { get; set; } = default!;

        public DateTimeOffset AccessTokenExpiresAt { get; set; }

        public string RefreshToken { get; set; } = default!;

        public DateTimeOffset RefreshTokenExpiresAt { get; set; }

        public string TokenType { get; set; } = default!;

        public ClaimsPrincipal User { get; set; } = default!;
    }
}
