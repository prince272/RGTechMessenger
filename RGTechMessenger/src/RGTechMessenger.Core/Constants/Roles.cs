﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGTechMessenger.Core.Constants
{
    public static class Roles
    {
        public const string Admin = nameof(Admin);

        public const string Member = nameof(Member);

        public static IEnumerable<string> All => new[] { Admin, Member };
    }
}
